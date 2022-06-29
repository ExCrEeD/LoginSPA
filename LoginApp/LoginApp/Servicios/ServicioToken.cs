using LoginApp.DTOS;
using LoginApp.Infraestructura.Repositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp
{
    public interface IServicioToken {

        void AlmacenarToken(DTOLoginXoauth2 loginXoauth2);
        DTOLoginXoauth2 ConsultarToken(string email);
        string RefreshToken(string refreshToken, string scopes);
        ConfigApp ObtenerConfiguracionAPP();
    }
    public class ServicioToken : IServicioToken
    {

        private readonly IRepositorioToken repositorioToken;
        private readonly string scopes;
        private readonly ConfigApp configApp;

        public ServicioToken(IConfiguration Configuration, IRepositorioToken repositorioToken)
        {
            this.repositorioToken = repositorioToken;
            this.scopes = "https://outlook.office.com/IMAP.AccessAsUser.All%20offline_access%20email%20openid";
            this.configApp = Configuration.GetSection("ConfiguracionAPP").Get<ConfigApp>();
        }

        public DTOLoginXoauth2 ConsultarToken(string email)=> repositorioToken.ConsultarToken(email);

        public void AlmacenarToken(DTOLoginXoauth2 loginXoauth2) => repositorioToken.AlmacenarToken(loginXoauth2);

        public string RefreshToken(string refreshToken, string scopes) => RefreshAccessToken(refreshToken,scopes).GetAwaiter().GetResult();

        public ConfigApp ObtenerConfiguracionAPP() => configApp;

        private async Task<string> RefreshAccessToken(string refreshToken,string scopes)
        {

            string redirectUri = string.Format("http://127.0.0.1:{0}/", GetRandomUnusedPort());
            // builds the  request
            string tokenRequestBody = string.Format("redirect_uri={0}&client_id={1}&refresh_token={2}&scope={3}&grant_type=refresh_token",
                Uri.EscapeDataString(redirectUri),
                configApp.ClientID,
                refreshToken,
                string.IsNullOrEmpty(scopes) ? this.scopes : scopes
                );


            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(configApp.TokenUri);
            //tokenRequest.Headers["Origin"] = "http://127.0.0.1";
            tokenRequest.Headers["Origin"] = System.Environment.UserDomainName;
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;

            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using StreamReader reader = new StreamReader(tokenResponse.GetResponseStream());
                // reads response body
                return await reader.ReadToEndAsync();

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

     
    }

   


}

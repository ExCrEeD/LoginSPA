using LoginApp.DTOS;
using LoginApp.Infraestructura.Repositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace LoginApp
{
    public interface IServicioToken {

        void AlmacenarToken(DTOLoginXoauth2 loginXoauth2);
        DTOLoginXoauth2 ConsultarToken(string email);
        DTOLoginXoauth2 RefreshToken(string email, string callbackURL);
        ConfigApp ObtenerConfiguracionAPP();
        void RefrescarTokenProcesoAutomatico();
    }
    public class ServicioToken : IServicioToken
    {
        private readonly IRepositorioToken repositorioToken;
        private readonly ConfigApp configApp;

        public ServicioToken(IConfiguration Configuration, IRepositorioToken repositorioToken)
        {
            this.repositorioToken = repositorioToken;
            this.configApp = Configuration.GetSection("ConfiguracionAPP").Get<ConfigApp>();
        }

        public DTOLoginXoauth2 ConsultarToken(string email) {
            var token = repositorioToken.ConsultarToken(email);
            if (token is null)
                throw new Exception($"No se encontro un token de acceso para el email {email}");
            
            return token;
        }

        public void AlmacenarToken(DTOLoginXoauth2 loginXoauth2) => repositorioToken.AlmacenarToken(loginXoauth2);

        public DTOLoginXoauth2 RefreshToken(string email, string callbackURL) => RefreshAccessToken(email, callbackURL).GetAwaiter().GetResult();

        public ConfigApp ObtenerConfiguracionAPP() => configApp;

        private async Task<DTOLoginXoauth2> RefreshAccessToken(string email, string callbackURL)
        {
            var login = ConsultarToken(email);
            string redirectUri = string.Format(callbackURL, GetRandomUnusedPort());
            string tokenRequestBody = string.Format("redirect_uri={0}&client_id={1}&refresh_token={2}&scope={3}&grant_type=refresh_token",
                Uri.EscapeDataString(redirectUri),
                configApp.ClientID,
                login.RefreshToken,
                login.Scopes
                );

            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(configApp.TokenUri);
            tokenRequest.Headers["Origin"] = callbackURL;
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
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using StreamReader reader = new StreamReader(tokenResponse.GetResponseStream());
                string response =  await reader.ReadToEndAsync();
                var refreshToken =  JsonSerializer.Deserialize<DTORefreshToken>(response);
                var loginRefresh =  new DTOLoginXoauth2
                {
                    Email = email,
                    Scopes = refreshToken.scope,
                    AccesToken = refreshToken.access_token,
                    ExpiracionAccesToken = DateTime.Now.AddSeconds(refreshToken.expires_in),
                    RefreshToken = refreshToken.refresh_token
                };
                repositorioToken.AlmacenarToken(loginRefresh);
                return loginRefresh;
            }
            catch (WebException ex)
            {
                if(ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                    {
                        using Stream dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        throw new Exception(responseFromServer);
                    }
                }
                throw ex;
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

        public void RefrescarTokenProcesoAutomatico()
        {
            var cuentasdeCorreo = repositorioToken.ObtenerCuentasDeCorreo();
            foreach (var email in cuentasdeCorreo)
            {
                try
                {
                    RefreshAccessToken(email,string.Empty).GetAwaiter().GetResult();
                }
                catch(Exception ex) {
                    repositorioToken.ActualizarExepcionAutenticacionEmail(email,ex.Message);
                }
            };
        }
    }
}

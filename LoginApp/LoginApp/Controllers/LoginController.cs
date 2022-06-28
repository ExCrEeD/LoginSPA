using LoginApp.DTOS;
using LoginApp.Infraestructura;
using LoginApp.Infraestructura.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ContextoLogin dbContext;
        private readonly IConfiguration configuration;
        public LoginController(ContextoLogin dbContext,IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

    

        [HttpPost]
        [Route("GuardarTokenXOAUTH2")]
        public IActionResult GuardarTokenXOAUTH2(DTOLoginXoauth2 loginXoauth2)
        {
            dbContext.IniciosDeSesion.Add(
                    new IniciosDeSesion {
                        AccesToken = loginXoauth2.AccesToken,
                        Email = loginXoauth2.Email
                    }    
            );
            dbContext.SaveChanges();
            return Ok("Token almacenado correctamente");
        }

        [HttpPost]
        [Route("RefreshAccesToken")]
        public IActionResult RefreshAccesToken(DTOLoginXoauth2 loginXoauth2)
        {
            dbContext.IniciosDeSesion.Add(
                    new IniciosDeSesion
                    {
                        AccesToken = loginXoauth2.AccesToken,
                        Email = loginXoauth2.Email
                    }
            );
            dbContext.SaveChanges();
            return Ok("Token almacenado correctamente");
        }




        async Task<string> RefreshAccessToken(string redirectUri, string refreshToken)
        {

            // builds the  request
            string tokenRequestBody = string.Format("redirect_uri={0}&client_id={1}&refresh_token={2}&scope={3}&grant_type=refresh_token",
                Uri.EscapeDataString(redirectUri),
                clientID,
                refreshToken,
                scope
                );


            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenUri);
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
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    return await reader.ReadToEndAsync();
                }

            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


    }
}

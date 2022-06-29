using LoginApp.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace LoginApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IServicioToken servicioToken;

        public LoginController(IServicioToken servicioToken)
        {
            this.servicioToken = servicioToken;
        }

        [HttpGet]
        [Route("ObtenerConfiguracionAPP")]
        public IActionResult ObtenerConfiguracionAPP() => Ok(servicioToken.ObtenerConfiguracionAPP());


        [HttpGet]
        [Route("ConsultarToken")]
        public IActionResult ConsultarToken(string email) => Ok(servicioToken.ConsultarToken(email));

        [HttpGet]
        [Route("RefreshAccesToken")]
        public IActionResult RefreshAccesToken(string email) => Ok(servicioToken.RefreshToken(email, string.Format("{0}://{1}", Request.Scheme, Request.Host)));


        [HttpPost]
        [Route("AlmacenarToken")]
        public IActionResult AlmacenarToken(DTOLoginXoauth2 loginXoauth2)
        {
            servicioToken.AlmacenarToken(loginXoauth2);
            return Ok("Token almacenado correctamente");
        }
    }
}

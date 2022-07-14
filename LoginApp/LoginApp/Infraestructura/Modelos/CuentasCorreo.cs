using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Infraestructura.Modelos
{
    public class CuentasCorreo
    {
        [Key]
        public string Email { get; set; }
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public DateTime ExpiracionAccesToken { get; set; }
        public DateTime ExpiracionRefreshToken { get; set; }
        public string ExcepcionAutenticacion { get; set; }
    }
}

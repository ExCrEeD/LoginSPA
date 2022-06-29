using System;
using System.ComponentModel.DataAnnotations;

namespace LoginApp.DTOS
{
    public class DTOLoginXoauth2
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string AccesToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string Scopes { get; set; }
        [Required]
        public DateTime ExpiracionAccesToken { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.DTOS
{
    public class ConfigApp
    {
        public string ClientID { get; set; }
        public string TokenUri { get; set; }
        public string AuthUri { get; set; }
    }
}

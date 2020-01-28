using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class AuthServiceConfig
    {
        public string Secret { get; set; }

        public string AuthDBConnectionString { get; set; }

        public short TokenExpirationMinutes { get; set; }
    }
}

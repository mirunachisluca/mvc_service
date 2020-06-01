using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web
{
    public class JwtBearerTokenSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}

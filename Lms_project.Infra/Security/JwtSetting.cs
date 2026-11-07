using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Security
{
    public class JwtSetting
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audiance { get; set; } = string.Empty;
        public double ExpiresInHours { get; set; } 

    }
}

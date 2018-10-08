using System;
using System.Collections.Generic;
using System.Text;

namespace TwoFactorApp.models
{
    public class TwoFactor
    {
        public string UserName { get; set; }
        public string LoginToken { get; set; }
        public string SecretToken { get; set; }
    }
}

using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.InputTypes
{
    public class LoginViewModel
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public mUser User { get; set; }
    }
}

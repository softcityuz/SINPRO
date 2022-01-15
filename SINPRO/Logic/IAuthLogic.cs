using SINPRO.Entity.DataModels;
using SINPRO.InputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Logic
{
    public interface IAuthLogic
    {
        string Register(RegisterInputType registerInput);
        string Register(mUser mUser);
        LoginViewModel Login(LoginInputType loginInput);
        string LogOut(string accesToken);
        LoginViewModel RenewAccessToken(RenewTokenInputType renewToken);
        //string GetToken();
        UserViewModel User { get; }

    }
}

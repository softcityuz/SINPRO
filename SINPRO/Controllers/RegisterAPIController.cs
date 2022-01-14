using Microsoft.AspNetCore.Mvc;
using SINPRO.InputTypes;
using SINPRO.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SINPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAPIController : ControllerBase
    {
        private readonly IAuthLogic _authLogic;

        public RegisterAPIController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpPost]
        public string Post(RegisterInputType input)
        {
           return _authLogic.Register(input);
        }
    }
}

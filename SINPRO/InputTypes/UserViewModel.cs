using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.InputTypes
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? ManagerId { get; set; }
        public int? CounterpartyId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}

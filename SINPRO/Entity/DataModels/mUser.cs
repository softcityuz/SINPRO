using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity.DataModels
{
    [Table("mUser")]
    public class mUser
    {
        [Key]
        public int id { get; set; }
        public string fName { get; set; }
        public string sName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public DateTime statusDate { get; set; }
        public int status { get; set; }
        public string refreshToken { get; set; }
        public DateTime? refershTokenExpiration { get; set; }
        public DateTime inserted { get; set; }
        public DateTime? updated { get; set; }
        [ForeignKey("roleId")]
        public virtual mRole mRole { get; set; }
    }
}

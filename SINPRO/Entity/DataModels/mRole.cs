using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity.DataModels
{
    [Table("mRole")]
    public class mRole
    {
        [Key]
        public int id { get; set; }
        public int parentId { get; set; }
        public string resourceName { get; set; }
        public bool status { get; set; }
        public DateTime inserted { get; set; }
        public DateTime? updated { get; set; }
        public virtual ICollection<mUser> mUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity.DataModels
{
    [Table("mNew")]
    public class mNew
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string discription { get; set; }
        public string photo { get; set; }
        public DateTime inserted { get; set; }
        public DateTime? updated { get; set; }
    }
}

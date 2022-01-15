using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity.DataModels
{
    [Table("mMatch")]
    public class mMatch
    {
        [Key]
        public int id { get; set; }
        public string firstTimeName { get; set; }
        public string firstTimePhoto { get; set; }
        public string secondTimeName { get; set; }
        public string secondTimePhoto { get; set; }
        public int matchTime { get; set; }
        public DateTime matchDate { get; set; }
        public DateTime inserted { get; set; }
        public DateTime? updated { get; set; }

    }
}

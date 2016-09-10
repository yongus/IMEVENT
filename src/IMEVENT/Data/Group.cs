using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Data
{
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }
        public String Label { get; set; }
        public int IdSousZone { get; set; }
        public int IdResponsable { get; set; }
    }
}

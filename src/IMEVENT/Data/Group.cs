using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Group
    {
        public Guid IdGroup { get; set; }
        public String Label { get; set; }
        public int IdSousZone { get; set; }
        public int IdResponsable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class SousZone
    {
        public int IdZone { get; set; }
        public int IdSousZone { get; set; }
        public int IdParent { get; set; }
        public String Label { get; set; }
    }
}

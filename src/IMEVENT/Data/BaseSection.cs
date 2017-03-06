using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class BaseSection
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int EventId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Table:IObjectPersister
    {
        public int IdRefertoire { get; set; }
        public int Capacite { get; set; }
        public int ForSpecialRegime { get; set; }

        public int persist()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Responsable:IObjectPersister
    {
        public int Id { get; set; }
        public int IdUSer { get; set; }
        public int IdEntity { get; set; }
        public void persist(ApplicationDbContext context)
        {
            context.Responsables.Add(this);
            context.SaveChanges();
        }

    }
}

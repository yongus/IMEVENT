using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    interface IObjectPersister
    {
         void persist(ApplicationDbContext context);
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    interface IObjectPersister
    {
         int persist(ApplicationDbContext context);
       
    }
}

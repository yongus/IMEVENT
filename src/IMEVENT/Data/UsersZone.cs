using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class UsersZone:IObjectPersister
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ZoneId { get; set; }
        public int persist(ApplicationDbContext context)
        {
            context.UsersZones.Add(this);
            context.SaveChanges();
            return this.Id;
        }

    }
}

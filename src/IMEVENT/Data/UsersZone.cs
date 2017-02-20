using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class UsersZone:IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ZoneId { get; set; }
        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.UsersZones.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }

    }
}

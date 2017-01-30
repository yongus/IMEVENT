using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Hall:BaseSection,IObjectPersister
    {
        [Key]
        public int IdHall { get; set; }

        public void persist(ApplicationDbContext context)
        {
            context.Halls.Add(this);
            context.SaveChanges();
        }

    }
}

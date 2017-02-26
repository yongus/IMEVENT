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

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdHall = GetIdHallIdByName(Name); 
               
            if (IdHall != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                
                context.Halls.Add(this);
            }
            
            context.SaveChanges();
            return this.IdHall;
        }
        public int GetIdHallIdByName( string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var hall = context.Halls.FirstOrDefault(d => d.Name.Equals(name));
            if (hall != null)
            {
                return hall.IdHall;
            }
            else return 0;
        }

    }
}

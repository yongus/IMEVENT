using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class Hall:BaseSection,IObjectPersister
    {
        [Key]
        public int IdHall { get; set; }
        public HallSectionTypeEnum HallType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
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

        public static Dictionary<int, Hall> GetAllHalls(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Halls.Where(x => x.IdEvent == eventID).ToDictionary(x => x.IdHall, x => x);            
        }

    }
}

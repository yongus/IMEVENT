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
       
        public HallSectionTypeEnum HallType { get; set; }
        [Key]
        public int Id { get;  set; }

        public int Persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            
            Id = Convert.ToInt32(GetRecordID());
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {                
                context.Halls.Add(this);
            }
            
            context.SaveChanges();
            return this.Id;
        }

        public int GetHallIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Hall hall = context.Halls.FirstOrDefault(d => d.Name.Equals(name));            
            return (hall == null)? 0 : hall.Id;
        }

        public static Dictionary<int, Hall> GetHallSections(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Halls.Where(x => x.EventId == eventID).ToDictionary(x => x.Id, x => x);            
        }

        public object GetRecordID()
        {
            return GetHallIdByName(Name);
        }
    }
}

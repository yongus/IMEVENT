using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class SharingGroup : BaseSection, IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public SharingGroupCategoryEnum Type { get; set; }                

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            this.Id = GetSharingGroupIdByType(Type);

            if (Id == 0)
            {
                context.SharingGroups.Add(this);                
            }
            else
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
            return this.Id;
        }

        public int GetSharingGroupIdByType(SharingGroupCategoryEnum type)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            SharingGroup group = context.SharingGroups.FirstOrDefault(g => g.Type == type);
            return (group == null) ? 0 : group.Id;            
        }

        public static Dictionary<SharingGroupCategoryEnum, int> GetSharingGroups(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.SharingGroups.Where(x => x.EventId == eventID).ToDictionary(x => x.Type, x => x.Capacity);
        }
    }
}

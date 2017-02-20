using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Data
{
    public class Group:IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public String Label { get; set; }
        public int IdSousZone { get; set; }
        public int IdZone { get; set; }
        public int IdResponsable { get; set; }
        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Groups.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }
        /// <summary>
        /// returns the group associated with the given label or 0 if there is no such lable in the database.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static int GetIdGroupIdByLabel(ApplicationDbContext context, string label)
        {
            var group = context.Groups.FirstOrDefault(d => d.Label.Equals(label));
            if (group != null)
            {
                return group.Id;
            }
            else return 0;
        }
    }
}

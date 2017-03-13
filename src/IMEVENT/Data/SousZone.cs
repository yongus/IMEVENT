using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Data
{
    public class SousZone:IObjectPersister
    {
        public int ZoneId { get; set; }
        [Key]
        public int Id { get; set; }
        public int IdParent { get; set; }
        public String Label { get; set; }
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
                context.SousZones.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }
        public  int GetSousZoneIdByLabel( string label)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var zone = context.SousZones.FirstOrDefault(d => d.Label.Equals(label));
            if (zone != null)
            {
                return zone.Id;
            }
            else return 0;
        }

        public object GetRecordID()
        {
            return GetSousZoneIdByLabel(Label);
        }
    }
}

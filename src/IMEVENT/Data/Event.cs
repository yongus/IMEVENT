using IMEVENT.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Event:IObjectPersister
    {
        private  ApplicationDbContext _context;
        private IDataExtractor extractor;
        [Key]
        public int IdEvent { get; set; }
        public string Theme { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public EventTypeEnum type { get; set; }
        public int Fee { get; set; }
        public bool mingleAttendees { get; set; }
        public Event(ApplicationDbContext context)
        {
            _context = context;
        }

        public int persist()
        {
            _context = ApplicationDbContext.GetDbContext();
            if (IdEvent != 0)
            {
                _context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _context.Events.Add(this);
            }
            
            _context.SaveChanges();
            return this.IdEvent;
        }

        public Event(string name, IDataExtractor extractor)
        {
            this.extractor = extractor;
            this.Theme = name;
            this.IdEvent = 1;
        }

        public void  ExtractEventDetails(String source )
        {
            extractor.ExtractDataFromSource(source,IdEvent);
        }        
    }
}

using IMEVENT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Event:IObjectPersister
    {
        private IDataExtractor extractor;
        public int IdEvent { get; set; }
        public string Theme { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public EventType type { get; set; }
        public int Fee { get; set; }

        public int persist(ApplicationDbContext context)
        {
            context.Events.Add(this);
            context.SaveChanges();
            return this.IdEvent;
        }
        public Event(string name, IDataExtractor extractor)
        {
            this.extractor = extractor;
            this.Theme = name;
        }
        public void  ExtractEventDetails(String source)
        {
            extractor.ExtractDataFromSource(source,IdEvent);
        }

    }
}

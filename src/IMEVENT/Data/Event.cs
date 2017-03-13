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
        public int Id { get; set; }
        public string Theme { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public EventTypeEnum Type { get; set; }
        public int Fee { get; set; }
        public bool MingleAttendees { get; set; }
        public Event(ApplicationDbContext context)
        {
            _context = context;
        }

        public void setContext(IDataExtractor extractor)
        {
            this.extractor = extractor;
        }

        public int Persist()
        {
            _context = ApplicationDbContext.GetDbContext();
            Id = Convert.ToInt32(GetRecordID());
            if (Id == 0)
            {
                _context.Events.Add(this);                
            }
            else
            {
                _context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            
            _context.SaveChanges();
            return this.Id;
        }

        public Event(string name, IDataExtractor extractor)
        {
            this.extractor = extractor;
            this.Theme = name;            
        }

        public void  ExtractEventDetails(String source )
        {
            this.extractor.ExtractDataFromSource(source, this.Id);
        }

        public object GetRecordID()
        {
            /*when we figure out uniqueness criteria on the event this will change */
            return Id;
        }
    }
}

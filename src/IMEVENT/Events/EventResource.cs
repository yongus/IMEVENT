using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Events
{
    public abstract class EventResource
    {
        //Are resource data loaded?
        public bool IsDataLoaded { get; set; }

        public int EventId { get; set; }

        //Load data from DB
        protected abstract void EnsureLoaded();

        //generate resource list that will be used for matching
        public abstract bool GenerateItemsForMatching(bool mingle);        
    }
}

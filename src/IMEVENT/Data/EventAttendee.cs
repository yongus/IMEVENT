using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class EventAttendee
    {
        public Guid UserId { get; set; }
        public int InvitedBy { get; set; }
        public int IdEvent { get; set; }
        public int AmountPaid { get; set; }
        public string Remarks { get; set; }
    }
}

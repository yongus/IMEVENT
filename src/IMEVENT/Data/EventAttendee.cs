using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Data
{
    public class EventAttendee
    {
        [Key]
        public string AttendeeId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string InvitedBy { get; set; }        
        public int AmountPaid { get; set; }
        public string Remarks { get; set; }
        public string Regime { get; set; }
        public string Precision { get; set; }
        public HallSectionTypeEnum sectionType { get; set; }
        public DormitoryTypeEnum DormType { get; set; }
        public RegimeEnum RefectoryType { get; set; }
        public int IdHall { get; set; }
        public int SeatNbr { get; set; }
        public int IdDormitory { get; set; }
        public int BedNbr { get; set; }
        public int IdRefectory { get; set; }
        public int TableNbr { get; set; }
        public int TableSeatNbr { get; set; }

        public override string ToString()
        {
            string ret = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}"
                , InvitedBy
                , AmountPaid
                , Remarks
                , Regime
                , Precision
                , IdHall
                , SeatNbr
                , IdDormitory
                , BedNbr
                , IdRefectory
                , TableNbr
                , TableSeatNbr
                );

            return ret;
        }

        public string ToString(Dictionary<int, Hall> Halls, Dictionary<int, Dormitory> Dorms, Dictionary<int, Refectory> Refectories)
        {
            string ret = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}"
                , InvitedBy
                , AmountPaid
                , Remarks
                , Regime
                , Precision
                , Halls[IdHall].Name
                , SeatNbr
                , Dorms[IdDormitory].Name
                , BedNbr
                , Refectories[IdRefectory].Name
                , TableNbr
                , TableSeatNbr
                );

            return ret;
        }
    }
}

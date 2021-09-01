using DOA_Sony.DataLayer.Enums;
using System;

namespace DOA_Sony.DataLayer
{
    public class Record
    {
        public Guid RecordID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public RecordType RecordType { get; set; }
        public string ReturnReason { get; set; }
        public string RepairReason { get; set; }
    }
}

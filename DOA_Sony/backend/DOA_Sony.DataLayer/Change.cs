using DOA_Sony.DataLayer.Enums;
using System;

namespace DOA_Sony.DataLayer
{
    public class Change
    {
        public Guid ChangeID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public Guid RecordID { get; set; }
        public Record Record { get; set; }
        public ChangeType ChangeType { get; set; }
    }
}

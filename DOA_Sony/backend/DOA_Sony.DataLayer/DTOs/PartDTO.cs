using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.DataLayer.DTOs
{
    public class PartDTO
    {
        public Guid PartID { get; set; }
        public Guid ChangeID { get; set; }
        public string PartZCode { get; set; }
        public string PartBarcode { get; set; }
        public string Description { get; set; }
        public string PartNo { get; set; }
        public string ReceiptNo { get; set; }
    }
}

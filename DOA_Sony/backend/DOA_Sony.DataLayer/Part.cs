using DOA_Sony.DataLayer.DTOs;
using System;
using System.Linq.Expressions;

namespace DOA_Sony.DataLayer
{
    public class Part
    {
        public Guid PartID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public Guid ChangeID { get; set; }
        public Change Change { get; set; }
        public string PartZCode { get; set; }
        public string PartBarcode { get; set; }
        public string Description { get; set; }
        public string PartNo { get; set; }
        public string ReceiptNo { get; set; }

        public static Expression<Func<Part, PartDTO>> Projection = x => new PartDTO
        {
            ChangeID = x.ChangeID,
            Description = x.Description,
            PartBarcode = x.PartBarcode,
            PartZCode = x.PartZCode,
            PartID = x.PartID,
            PartNo = x.PartNo,
            ReceiptNo = x.ReceiptNo
        };

        public PartDTO ToDTO()
        {
            return new PartDTO
            {
                ChangeID = this.ChangeID,
                Description = this.Description,
                PartBarcode = this.PartBarcode,
                PartZCode = this.PartZCode,
                PartID = this.PartID,
                PartNo = this.PartNo,
                ReceiptNo = this.ReceiptNo
            };
        }
    }
}

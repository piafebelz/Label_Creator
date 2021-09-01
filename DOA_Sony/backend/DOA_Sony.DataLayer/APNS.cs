using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Enums;
using System;
using System.Linq.Expressions;

namespace DOA_Sony.DataLayer
{
    public class APNS
    {
        public Guid APNSID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public string APNSNo { get; set; }
        public Guid ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }
        public string SerialNo { get; set; }
        public string CargoNo { get; set; }
        public string General { get; set; }
        public APNSStatus Status { get; set; }
        public Guid? RecordID { get; set; }
        public Record Record { get; set; }

        public static Expression<Func<APNS, APNSDTO>> Projection = x => new APNSDTO
        {
            APNSID = x.APNSID,
            APNSNo = x.APNSNo,
            ProductTypeID = x.ProductTypeID,
            SerialNo = x.SerialNo,
            CargoNo = x.CargoNo,
            General = x.General,
            Status = x.Status.ToString(),
            RecordID = x.RecordID
        };

        public APNSDTO ToDTO()
        {
            return new APNSDTO
            {
                APNSID = this.APNSID,
                APNSNo = this.APNSNo,
                ProductTypeID = this.ProductTypeID,
                SerialNo = this.SerialNo,
                CargoNo = this.CargoNo,
                General = this.General,
                Status = this.Status.ToString(),
                RecordID = this.RecordID
            };
        }
    }
}

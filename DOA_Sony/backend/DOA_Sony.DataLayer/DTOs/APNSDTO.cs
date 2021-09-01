using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DOA_Sony.DataLayer.DTOs
{
    public class APNSDTO
    {
        public Guid APNSID { get; set; }
        public string APNSNo { get; set; }
        public Guid ProductTypeID { get; set; }
        public string SerialNo { get; set; }
        public string CargoNo { get; set; }
        public string General { get; set; }
        public string Status { get; set; }
        public Guid? RecordID { get; set; }
    }
}

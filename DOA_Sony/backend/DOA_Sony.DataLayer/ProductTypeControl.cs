using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.DataLayer
{
    public class ProductTypeControl
    {
        public Guid ProductTypeControlID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public Guid ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }
        public Guid ControlID { get; set; }
        public Control Control { get; set; }
    }
}

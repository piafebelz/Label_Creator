using DOA_Sony.DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DOA_Sony.DataLayer
{
    public class Control
    {
        public Guid ControlID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public string ControlName { get; set; }
        public virtual List<ProductTypeControl> ProductTypeControls { get; set; }
        public static Expression<Func<ProductTypeControl, ControlDTO>> Projection = x => new ControlDTO
        {
            ControlID = x.Control.ControlID,
            ControlName = x.Control.ControlName
        };

        public ControlDTO ToDTO()
        {
            return new ControlDTO
            {
                ControlID = this.ControlID,
                ControlName = this.ControlName
            };
        }
    }
}

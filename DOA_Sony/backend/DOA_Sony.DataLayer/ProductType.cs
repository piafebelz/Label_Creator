using DOA_Sony.DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DOA_Sony.DataLayer
{
    public class ProductType
    {
        public Guid ProductTypeID { get; set; }
        public DateTime CDATE { get; set; }
        public DateTime? UDATE { get; set; }
        public string TypeName { get; set; }
        public virtual List<ProductTypeControl> ProductTypeControls { get; set; }
        public static Expression<Func<ProductType, ProductTypeDTO>> Projection = x => new ProductTypeDTO
        {
            ProductTypeID = x.ProductTypeID,
            TypeName = x.TypeName
        };

        public ProductTypeDTO ToDTO()
        {
            return new ProductTypeDTO
            {
                TypeName = this.TypeName,
                ProductTypeID = this.ProductTypeID
            };
        }
    }
}

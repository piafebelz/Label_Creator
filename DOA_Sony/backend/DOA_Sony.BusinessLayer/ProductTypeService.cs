using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;
using DOA_Sony.DataLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOA_Sony.BusinessLayer
{
    public class ProductTypeService: IProductTypeService
    {
        private SonyServiceContext _db;
        public ProductTypeService(SonyServiceContext db)
        {
            _db = db;
        }
        public ProductTypeDTO CreateProductType(string name)
        {
            var type = new ProductType
            {
                TypeName = name,
                CDATE = DateTime.UtcNow
            };
            _db.ProductType.Add(type);
            _db.SaveChanges();
            return type.ToDTO();
        }

        public List<ProductTypeDTO> GetProductTypes()
        {
            return _db.ProductType.Select(ProductType.Projection).ToList();
        }

        public bool RelateControlWithProductType(Guid productTypeID, Guid controlID)
        {
            var productType = _db.ProductType.FirstOrDefault(x => x.ProductTypeID == productTypeID);
            if (productType != null)
            {
                var control = _db.Control.FirstOrDefault(x => x.ControlID == controlID);
                if (control != null)
                {
                    var productTypeControl = new ProductTypeControl { ControlID = control.ControlID, ProductTypeID = productType.ProductTypeID, CDATE = DateTime.UtcNow };
                    _db.ProductTypeControl.Add(productTypeControl);
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}

using DOA_Sony.DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.BusinessLayer.Interfaces
{
    public interface IProductTypeService
    {
        ProductTypeDTO CreateProductType(string name);
        List<ProductTypeDTO> GetProductTypes();
        bool RelateControlWithProductType(Guid productTypeID, Guid controlID);
    }
}

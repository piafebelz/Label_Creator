using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;
using DOA_Sony.DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOA_Sony.BusinessLayer
{
    public class SeedService: ISeedService
    {
        private SonyServiceContext _db;
        private IProductTypeService _productTypeService;
        private IControlService _controlService;
        public SeedService(SonyServiceContext db, IProductTypeService productTypeService, IControlService controlService)
        {
            _db = db;
            _productTypeService = productTypeService;
            _controlService = controlService;
        }
        public void SeedProductType()
        {
            var productTypes = _db.ProductType.Select(x => x.TypeName).ToList();
            var newProductTypes = new List<string>
            {
                "TV",
                "PlayStation"
            };
            var insertProductTypes = newProductTypes.Where(x => !productTypes.Contains(x));
            foreach (var type in insertProductTypes)
            {
                _productTypeService.CreateProductType(type);
            }
        }

        public void SeedControl()
        {
            var controls = _db.Control.Select(x => x.ControlName).ToList();
            var newControls = new List<string>
            {
                "Fiş Takılı Ekran Kontrolü",
                "Harddisk Kontrolü"
            };
            var insertControls = newControls.Where(x => !controls.Contains(x));
            foreach (var control in insertControls)
            {
                _controlService.CreateControl(control);
            }
        }

        public void SeedProductTypeControl()
        {
            var relations = new Dictionary<string, string> { { "TV", "Fiş Takılı Ekran Kontrolü" }, { "PlayStation", "Harddisk Kontrolü" } };
            var controls = _db.Control.Where(x => relations.Values.Contains(x.ControlName)).ToList();
            var productTypes = _db.ProductType.Where(x => relations.Keys.Contains(x.TypeName)).ToList();
            foreach (var productType in productTypes)
            {
                var relatedControl = controls.FirstOrDefault(x => x.ControlName == relations[productType.TypeName]);
                if (relatedControl != null)
                {
                    _productTypeService.RelateControlWithProductType(productType.ProductTypeID, relatedControl.ControlID);
                }
            }
        }
    }
}

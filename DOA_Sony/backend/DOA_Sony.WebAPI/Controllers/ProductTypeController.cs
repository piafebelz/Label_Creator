using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Models;

namespace DOA_Sony.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        /// <summary>
        /// APNS Girişi Ürün Tipleri Listesi
        /// </summary>
        [HttpGet]
        public ActionResult<List<ProductTypeDTO>> GetProductTypes()
        {
            return Ok(_productTypeService.GetProductTypes());
        }
    }
}
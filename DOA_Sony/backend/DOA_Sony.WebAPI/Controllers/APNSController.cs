using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Models;
using DOA_Sony.DataLayer.Enums;

namespace DOA_Sony.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APNSController : ControllerBase
    {
        private IAPNSService _APNSService;
        public APNSController(IAPNSService APNSService)
        {
            _APNSService = APNSService;
        }

        /// <summary>
        /// APNS Bul
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> GetAPNS(string APNSNo, OperationType operationType)
        {
            var r = _APNSService.GetAPNS(APNSNo, operationType);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// APNS Girişi
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> AddAPNS(string APNSNo, Guid productTypeID)
        {
            var r = _APNSService.CreateAPNS(productTypeID, APNSNo);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// APNS e bağlı kontrol listesi
        /// </summary>
        [HttpGet]
        public ActionResult<ControlListResponse> GetControlList(Guid APNSID)
        {
            var r = _APNSService.GetControlListByAPNS(APNSID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// Ürün Kabul
        /// ----------
        /// Not: Bu endpoint direkt olarak kontrol edildi statüsüne çeker. 
        /// Dolayısyla tüm checkboxların işaretli olduğu frontend de kontrol edilmeli.
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> ControlAPNS(Guid APNSID)
        {
            var r = _APNSService.ControlAPNS(APNSID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// Veri Girişi
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> DetailAPNS(Guid APNSID, string cargoNo, string serialNo, string general)
        {
            var r = _APNSService.DetailAPNS(APNSID, cargoNo, serialNo, general);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
        /// <summary>
        /// Karar Girişi
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> CreateDecision(Guid APNSID, RecordType recordType, string description)
        {
            var r = _APNSService.CreateDecision(APNSID, recordType, description);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// Değişim Girişi
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> DetailChange(Guid APNSID, Guid recordID, ChangeType changeType)
        {
            var r = _APNSService.DetailChange(APNSID, recordID, changeType);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }

        /// <summary>
        /// Parça Listesi
        /// </summary>
        [HttpPost]
        public ActionResult<PartListResponse> GetAddedPartsList(Guid APNSID, Guid recordID)
        {
            var r = _APNSService.GetAddedPartsList(APNSID, recordID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
        /// <summary>
        /// Parça Ekle
        /// </summary>
        [HttpPost]
        public ActionResult<PartListResponse> CreatePart(Guid APNSID, Guid recordID, string partZCode, string partBarcode, string description, string partNo, string receiptNo)
        {
            var r = _APNSService.CreatePart(APNSID, recordID, partZCode, partBarcode, description, partNo, receiptNo);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
        /// <summary>
        /// Parça Sil
        /// </summary>
        [HttpPost]
        public ActionResult<PartListResponse> DeletePart(Guid APNSID, Guid recordID, Guid partID)
        {
            var r = _APNSService.DeletePart(APNSID, recordID, partID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
        /// <summary>
        /// Tüm Parçaları Sil
        /// </summary>
        [HttpPost]
        public ActionResult<PartListResponse> DeleteAllParts(Guid APNSID, Guid recordID)
        {
            var r = _APNSService.DeleteAllParts(APNSID, recordID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
        /// <summary>
        /// APNS Tamamla
        /// </summary>
        [HttpPost]
        public ActionResult<APNSResponse> CompleteAPNS(Guid APNSID)
        {
            var r = _APNSService.CompleteAPNS(APNSID);
            if (r.IsSuccessful)
            {
                return Ok(r);
            }
            return BadRequest(r);
        }
    }
}
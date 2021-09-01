using DOA_Sony.BusinessLayer.Interfaces;
using DOA_Sony.DataLayer;
using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Enums;
using DOA_Sony.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOA_Sony.BusinessLayer
{
    public class APNSService : IAPNSService
    {
        private SonyServiceContext _db;
        public APNSService(SonyServiceContext db)
        {
            _db = db;
        }

        public APNSResponse GetAPNS(string APNSNo, OperationType operationType)
        {
            if (!string.IsNullOrEmpty(APNSNo))
            {
                var apns = _db.APNS.FirstOrDefault(x => x.APNSNo == APNSNo);
                switch (operationType)
                {
                    case OperationType.Create:
                        {
                            if (apns == null)
                            {
                                return new APNSResponse { IsSuccessful = true, APNSDTO = new APNSDTO() };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = "APNS bu işlem için uygun değil" };
                        }
                    case OperationType.Control:
                        {
                            if (apns != null)
                            {
                                if (apns.Status == APNSStatus.Created)
                                {
                                    return new APNSResponse { IsSuccessful = true, APNSDTO = apns.ToDTO() };
                                }
                                return new APNSResponse { IsSuccessful = false, Error = "APNS bu işlem için uygun değil" };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = "APNS bulunamadı!" };
                        }
                    case OperationType.Detail:
                        {
                            if (apns != null)
                            {
                                if (apns.Status == APNSStatus.Controlled)
                                {
                                    return new APNSResponse { IsSuccessful = true, APNSDTO = apns.ToDTO() };
                                }
                                return new APNSResponse { IsSuccessful = false, Error = "APNS bu işlem için uygun değil" };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = "APNS bulunamadı!" };
                        }
                    case OperationType.Decision:
                        {
                            if (apns != null)
                            {
                                if (apns.Status == APNSStatus.Deatiled || apns.Status == APNSStatus.ChangeRequired || apns.Status == APNSStatus.FragmentationRequired)
                                {
                                    return new APNSResponse { IsSuccessful = true, APNSDTO = apns.ToDTO() };
                                }
                                return new APNSResponse { IsSuccessful = false, Error = "APNS bu işlem için uygun değil" };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = "APNS bulunamadı!" };
                        }
                    default:
                        {
                            return new APNSResponse { IsSuccessful = false, Error = "OperationType alanını doldurunuz!" };
                        }
                }
            }
            return new APNSResponse { IsSuccessful = false, Error = "APNS No alanını doldurunuz!" };
        }

        public APNSResponse CreateAPNS(Guid productTypeID, string APNSNo)
        {
            if (productTypeID != Guid.Empty && !string.IsNullOrEmpty(APNSNo))
            {
                var apns = _db.APNS.FirstOrDefault(x => x.APNSNo == APNSNo);
                if (apns == null)
                {
                    var productType = _db.ProductType.FirstOrDefault(x => x.ProductTypeID == productTypeID);
                    if (productType != null)
                    {
                        apns = new APNS
                        {
                            APNSNo = APNSNo,
                            CDATE = DateTime.UtcNow,
                            ProductTypeID = productTypeID,
                            Status = APNSStatus.Created
                        };
                        _db.APNS.Add(apns);
                        _db.SaveChanges();
                        return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = "Ürün Tipi Bulunamadı!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = $"{APNSNo} numaralı APNS zaten mevcut!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen tüm bilgileri eksiksiz gönderiniz!" };
        }

        public APNSResponse ControlAPNS(Guid APNSID)
        {
            if (APNSID != Guid.Empty)
            {
                var apns = _db.APNS.FirstOrDefault(x => x.APNSID == APNSID);
                if (apns != null)
                {
                    if (apns.Status == APNSStatus.Created)
                    {
                        apns.Status = APNSStatus.Controlled;
                        apns.UDATE = DateTime.UtcNow;
                        _db.Entry(apns).State = EntityState.Modified;
                        _db.SaveChanges();
                        return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = $"Sistemsel Hata APNS bulunamadı!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen tüm bilgileri eksiksiz gönderiniz!" };
        }

        public ControlListResponse GetControlListByAPNS(Guid APNSID)
        {
            var apns = _db.APNS.Include(x => x.ProductType).ThenInclude(x => x.ProductTypeControls).ThenInclude(x => x.Control).FirstOrDefault(x => x.APNSID == APNSID);
            if (apns != null)
            {
                var controls = apns.ProductType.ProductTypeControls.AsQueryable().Select(Control.Projection).ToList();
                return new ControlListResponse { IsSuccessful = true, ControlDTOs = controls };
            }
            return new ControlListResponse { IsSuccessful = false, Error = $"Sistemsel Hata APNS bulunamadı!" };
        }

        public APNSResponse DetailAPNS(Guid APNSID, string cargoNo, string serialNo, string general)
        {
            if (APNSID != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(cargoNo))
                {
                    if (!string.IsNullOrEmpty(serialNo))
                    {
                        if (!string.IsNullOrEmpty(general))
                        {
                            var apns = _db.APNS.FirstOrDefault(x => x.APNSID == APNSID);
                            if (apns != null)
                            {
                                if (apns.Status == APNSStatus.Controlled)
                                {
                                    apns.CargoNo = cargoNo;
                                    apns.SerialNo = serialNo;
                                    apns.General = general;
                                    apns.UDATE = DateTime.UtcNow;
                                    apns.Status = APNSStatus.Deatiled;
                                    _db.Entry(apns).State = EntityState.Modified;
                                    _db.SaveChanges();
                                    return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                                }
                                return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = $"Sistemsel Hata APNS bulunamadı!" };
                        }
                        return new APNSResponse { IsSuccessful = false, Error = "Lütfen Diğer Bilgiler alanını doldurunuz!" };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = "Lütfen Seri No alanını doldurunuz!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = "Lütfen Kargo No alanını doldurunuz!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public APNSResponse CreateDecision(Guid APNSID, RecordType recordType, string description)
        {
            if (APNSID != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(description) || (recordType == RecordType.Change))
                {
                    var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                    if (apns != null)
                    {
                        if (apns.Status == APNSStatus.Deatiled)
                        {
                            var record = new Record();
                            switch (recordType)
                            {
                                case RecordType.Return:
                                {
                                    record = new Record
                                    {
                                        CDATE = DateTime.UtcNow,
                                        RecordType = recordType,
                                        ReturnReason = description
                                    };
                                    _db.Record.Add(record);
                                    _db.SaveChanges();
                                    break;
                                }
                                case RecordType.Repair:
                                {
                                    record = new Record
                                    {
                                        CDATE = DateTime.UtcNow,
                                        RecordType = recordType,
                                        RepairReason = description
                                    };
                                    _db.Record.Add(record);
                                    _db.SaveChanges();
                                    break;
                                }
                            case RecordType.Change:
                                {
                                    record = new Record
                                    {
                                        CDATE = DateTime.UtcNow,
                                        RecordType = recordType
                                    };
                                    _db.Record.Add(record);
                                    _db.SaveChanges();
                                    break;
                                }
                            default:
                                {
                                    return new APNSResponse { IsSuccessful = false, Error = "Lütfen RecordType alanını doldurunuz!" };
                                }
                            }
                            if (!recordType.Equals(RecordType.Change))
                            {
                                apns.Status = APNSStatus.Completed;
                            }
                            else
                            {
                                apns.Status = APNSStatus.ChangeRequired;
                            }
                            apns.RecordID = record.RecordID;
                            apns.UDATE = DateTime.UtcNow;
                            _db.Entry(apns).State = EntityState.Modified;
                            _db.SaveChanges();
                            return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                        }
                        if (apns.Status == APNSStatus.ChangeRequired || apns.Status == APNSStatus.FragmentationRequired)
                        {
                            if (recordType == RecordType.Change)
                            {
                                return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                        }
                        return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = "Lütfen Açıklama alanını doldurunuz!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public APNSResponse DetailChange(Guid APNSID, Guid recordID, ChangeType changeType)
        {
            if (APNSID != Guid.Empty)
            {
                if (recordID != Guid.Empty)
                {
                    var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                    if (apns != null)
                    {
                        if (apns.Status == APNSStatus.ChangeRequired)
                        {
                            if (apns.RecordID.HasValue && apns.RecordID.Value == recordID)
                            {
                                var record = apns.Record;
                                var change = new Change
                                {
                                    CDATE = DateTime.UtcNow,
                                    ChangeType = changeType,
                                    RecordID = recordID
                                };
                                _db.Change.Add(change);
                                _db.SaveChanges();
                                if (changeType != ChangeType.Fragmentation)
                                {
                                    apns.Status = APNSStatus.Completed;
                                }
                                else
                                {
                                    apns.Status = APNSStatus.FragmentationRequired;
                                }
                                apns.UDATE = DateTime.UtcNow;
                                _db.Entry(apns).State = EntityState.Modified;
                                record.UDATE = DateTime.UtcNow;
                                _db.Entry(record).State = EntityState.Modified;
                                _db.SaveChanges();
                                return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = $"Karar ve APNS eşleşmiyor!" };
                        }
                        if (apns.Status == APNSStatus.FragmentationRequired)
                        {
                            if (changeType == ChangeType.Fragmentation)
                            {
                                return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                            }
                            return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                        }
                        return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = "Lütfen RecordID alanını doldurunuz!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public PartListResponse GetAddedPartsList(Guid APNSID, Guid recordID)
        {
            if (APNSID != Guid.Empty)
            {
                if (recordID != Guid.Empty)
                {
                    var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                    if (apns != null)
                    {
                        if (apns.Status == APNSStatus.FragmentationRequired)
                        {
                            if (apns.RecordID.HasValue && apns.RecordID.Value == recordID)
                            {
                                var parts = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == recordID).Select(Part.Projection).ToList();
                                return new PartListResponse { PartDTOs = parts, IsSuccessful = true };
                            }
                            return new PartListResponse { IsSuccessful = false, Error = $"Karar ve APNS eşleşmiyor!" };
                        }
                        return new PartListResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                    }
                    return new PartListResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                }
                return new PartListResponse { IsSuccessful = false, Error = "Lütfen RecordID alanını doldurunuz!" };
            }
            return new PartListResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public PartListResponse CreatePart(Guid APNSID, Guid recordID, string partZCode, string partBarcode, string description, string partNo, string receiptNo)
        {
            if (APNSID != Guid.Empty)
            {
                if (recordID != Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(partZCode))
                    {
                        if (!string.IsNullOrEmpty(partBarcode))
                        {
                            if (!string.IsNullOrEmpty(description))
                            {
                                if (!string.IsNullOrEmpty(partNo))
                                {
                                    if (!string.IsNullOrEmpty(receiptNo))
                                    {
                                        var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                                        if (apns != null)
                                        {
                                            if (apns.Status == APNSStatus.FragmentationRequired)
                                            {
                                                if (apns.RecordID.HasValue && apns.RecordID.Value == recordID)
                                                {
                                                    var change = _db.Change.Include(x => x.Record).FirstOrDefault(x => x.Record.RecordID == apns.Record.RecordID);
                                                    if (change != null)
                                                    {
                                                        var newPart = new Part
                                                        {
                                                            CDATE = DateTime.UtcNow,
                                                            ChangeID = change.ChangeID,
                                                            Description = description,
                                                            PartBarcode = partBarcode,
                                                            PartNo = partNo,
                                                            ReceiptNo = receiptNo,
                                                            PartZCode = partZCode
                                                        };
                                                        _db.Part.Add(newPart);
                                                        _db.SaveChanges();
                                                        var parts = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == recordID).Select(Part.Projection).ToList();
                                                        return new PartListResponse { PartDTOs = parts, IsSuccessful = true };
                                                    }
                                                }
                                                return new PartListResponse { IsSuccessful = false, Error = $"Karar ve APNS eşleşmiyor!" };
                                            }
                                            return new PartListResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                                        }
                                        return new PartListResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                                    }
                                    return new PartListResponse { IsSuccessful = false, Error = "Lütfen Fiş No alanını doldurunuz!" };
                                }
                                return new PartListResponse { IsSuccessful = false, Error = "Lütfen Sökülen Parça No alanını doldurunuz!" };
                            }
                            return new PartListResponse { IsSuccessful = false, Error = "Lütfen Parça açıklaması alanını doldurunuz!" };
                        }
                        return new PartListResponse { IsSuccessful = false, Error = "Lütfen Parça Barkosu alanını doldurunuz!" };
                    }
                    return new PartListResponse { IsSuccessful = false, Error = "Lütfen Parça Z Kodu alanını doldurunuz!" };
                }
                return new PartListResponse { IsSuccessful = false, Error = "Lütfen RecordID alanını doldurunuz!" };
            }
            return new PartListResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public PartListResponse DeletePart(Guid APNSID, Guid recordID, Guid partID)
        {
            if (APNSID != Guid.Empty)
            {
                if (recordID != Guid.Empty)
                {
                    if (partID != Guid.Empty)
                    {
                        
                        var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                        if (apns != null)
                        {
                            if (apns.Status == APNSStatus.FragmentationRequired)
                            {
                                if (apns.RecordID.HasValue && apns.RecordID.Value == recordID)
                                {
                                    var part = _db.Part.Include(x => x.Change).FirstOrDefault(x => x.PartID == partID && x.Change.RecordID == recordID);
                                    if (part != null)
                                    {
                                        _db.Entry(part).State = EntityState.Deleted;
                                        _db.SaveChanges();
                                        var parts = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == recordID).Select(Part.Projection).ToList();
                                        return new PartListResponse { PartDTOs = parts, IsSuccessful = true };
                                    }
                                    return new PartListResponse { IsSuccessful = false, Error = $"Parça bulunamadı!" };
                                }
                                return new PartListResponse { IsSuccessful = false, Error = $"Karar ve APNS eşleşmiyor!" };
                            }
                            return new PartListResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                        }
                        return new PartListResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                    }
                    return new PartListResponse { IsSuccessful = false, Error = "Lütfen PartID alanını doldurunuz!" };
                }
                return new PartListResponse { IsSuccessful = false, Error = "Lütfen RecordID alanını doldurunuz!" };
            }
            return new PartListResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public PartListResponse DeleteAllParts(Guid APNSID, Guid recordID)
        {
            if (APNSID != Guid.Empty)
            {
                if (recordID != Guid.Empty)
                {
                    var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                    if (apns != null)
                    {
                        if (apns.Status == APNSStatus.FragmentationRequired)
                        {
                            if (apns.RecordID.HasValue && apns.RecordID.Value == recordID)
                            {
                                var parts = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == recordID);
                                if (parts.Count() > 0)
                                {
                                    _db.RemoveRange(parts);
                                    _db.SaveChanges();
                                    var partsDTO = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == recordID).Select(Part.Projection).ToList();
                                    return new PartListResponse { PartDTOs = partsDTO, IsSuccessful = true };
                                }
                                return new PartListResponse { IsSuccessful = false, Error = $"Parça bulunamadı!" };
                            }
                            return new PartListResponse { IsSuccessful = false, Error = $"Karar ve APNS eşleşmiyor!" };
                        }
                        return new PartListResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                    }
                    return new PartListResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
                }
                return new PartListResponse { IsSuccessful = false, Error = "Lütfen RecordID alanını doldurunuz!" };
            }
            return new PartListResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }

        public APNSResponse CompleteAPNS(Guid APNSID)
        {
            if (APNSID != Guid.Empty)
            {
                var apns = _db.APNS.Include(x => x.Record).FirstOrDefault(x => x.APNSID == APNSID);
                if (apns != null)
                {
                    if (apns.Status == APNSStatus.FragmentationRequired)
                    {
                        var parts = _db.Part.Include(x => x.Change).Where(x => x.Change.RecordID == apns.Record.RecordID);
                        if (parts.Count() > 0)
                        {
                            apns.Status = APNSStatus.Completed;
                            _db.Entry(apns).State = EntityState.Modified;
                            _db.SaveChanges();
                            return new APNSResponse { APNSDTO = apns.ToDTO(), IsSuccessful = true };
                        }
                        return new APNSResponse { IsSuccessful = false, Error = "Lütfen ilk önce parça ekleyiniz." };
                    }
                    return new APNSResponse { IsSuccessful = false, Error = $"{apns.APNSNo} numaralı APNS bu işlem için uygun değil!" };
                }
                return new APNSResponse { IsSuccessful = false, Error = "Sistemsel Hata APNS bulunamadı!" };
            }
            return new APNSResponse { IsSuccessful = false, Error = "Lütfen APNS No alanını doldurunuz!" };
        }
    }
}

using DOA_Sony.DataLayer.DTOs;
using DOA_Sony.DataLayer.Enums;
using DOA_Sony.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.BusinessLayer.Interfaces
{
    public interface IAPNSService
    {
        APNSResponse GetAPNS(string APNSNo, OperationType operationType);
        APNSResponse CreateAPNS(Guid productTypeID, string APNSNo);
        APNSResponse ControlAPNS(Guid APNSID);
        ControlListResponse GetControlListByAPNS(Guid APNSID);
        APNSResponse DetailAPNS(Guid APNSID, string cargoNo, string serialNo, string general);
        APNSResponse CreateDecision(Guid APNSID, RecordType recordType, string description);
        APNSResponse DetailChange(Guid APNSID, Guid recordID, ChangeType changeType);
        PartListResponse GetAddedPartsList(Guid APNSID, Guid recordID);
        PartListResponse CreatePart(Guid APNSID, Guid recordID, string partZCode, string partBarcode, string description, string partNo, string receiptNo);
        PartListResponse DeletePart(Guid APNSID, Guid recordID, Guid partID);
        PartListResponse DeleteAllParts(Guid APNSID, Guid recordID);
        APNSResponse CompleteAPNS(Guid APNSID);
    }
}

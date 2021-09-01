part of openapi.api;

class APNSApi {
  final ApiClient apiClient;

  APNSApi([ApiClient apiClient]) : apiClient = apiClient ?? defaultApiClient;

  /// APNS Girişi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSAddAPNSPostWithHttpInfo({String aPNSNo, String productTypeID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/AddAPNS".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (aPNSNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSNo", aPNSNo));
    }
    if (productTypeID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "productTypeID", productTypeID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// APNS Girişi
  ///
  ///
  Future<APNSResponse> apiAPNSAddAPNSPost({String aPNSNo, String productTypeID}) async {
    Response response = await apiAPNSAddAPNSPostWithHttpInfo(aPNSNo: aPNSNo, productTypeID: productTypeID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// APNS Tamamla with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSCompleteAPNSPostWithHttpInfo({String APNSID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/CompleteAPNS".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// APNS Tamamla
  ///
  ///
  Future<APNSResponse> apiAPNSCompleteAPNSPost({String APNSID}) async {
    Response response = await apiAPNSCompleteAPNSPostWithHttpInfo(APNSID: APNSID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// Ürün Kabul  ----------  Not: Bu endpoint direkt olarak kontrol edildi statüsüne çeker.   Dolayısyla tüm checkboxların işaretli olduğu frontend de kontrol edilmeli. with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSControlAPNSPostWithHttpInfo({String APNSID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/ControlAPNS".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Ürün Kabul  ----------  Not: Bu endpoint direkt olarak kontrol edildi statüsüne çeker.   Dolayısyla tüm checkboxların işaretli olduğu frontend de kontrol edilmeli.
  ///
  ///
  Future<APNSResponse> apiAPNSControlAPNSPost({String APNSID}) async {
    Response response = await apiAPNSControlAPNSPostWithHttpInfo(APNSID: APNSID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// Karar Girişi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSCreateDecisionPostWithHttpInfo({String APNSID, RecordType recordType, String description}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/CreateDecision".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordType != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordType", recordType));
    }
    if (description != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "description", description));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Karar Girişi
  ///
  ///
  Future<APNSResponse> apiAPNSCreateDecisionPost({String APNSID, RecordType recordType, String description}) async {
    Response response = await apiAPNSCreateDecisionPostWithHttpInfo(APNSID: APNSID, recordType: recordType, description: description);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// Parça Ekle with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSCreatePartPostWithHttpInfo(
      {String APNSID, String recordID, String partZCode, String partBarcode, String description, String partNo, String receiptNo}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/CreatePart".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordID", recordID));
    }
    if (partZCode != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "partZCode", partZCode));
    }
    if (partBarcode != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "partBarcode", partBarcode));
    }
    if (description != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "description", description));
    }
    if (partNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "partNo", partNo));
    }
    if (receiptNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "receiptNo", receiptNo));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Parça Ekle
  ///
  ///
  Future<PartListResponse> apiAPNSCreatePartPost(
      {String APNSID, String recordID, String partZCode, String partBarcode, String description, String partNo, String receiptNo}) async {
    Response response = await apiAPNSCreatePartPostWithHttpInfo(
        APNSID: APNSID,
        recordID: recordID,
        partZCode: partZCode,
        partBarcode: partBarcode,
        description: description,
        partNo: partNo,
        receiptNo: receiptNo);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'PartListResponse') as PartListResponse;
    } else {
      return null;
    }
  }

  /// Tüm Parçaları Sil with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSDeleteAllPartsPostWithHttpInfo({String APNSID, String recordID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/DeleteAllParts".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordID", recordID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Tüm Parçaları Sil
  ///
  ///
  Future<PartListResponse> apiAPNSDeleteAllPartsPost({String APNSID, String recordID}) async {
    Response response = await apiAPNSDeleteAllPartsPostWithHttpInfo(APNSID: APNSID, recordID: recordID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'PartListResponse') as PartListResponse;
    } else {
      return null;
    }
  }

  /// Parça Sil with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSDeletePartPostWithHttpInfo({String APNSID, String recordID, String partID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/DeletePart".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordID", recordID));
    }
    if (partID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "partID", partID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Parça Sil
  ///
  ///
  Future<PartListResponse> apiAPNSDeletePartPost({String APNSID, String recordID, String partID}) async {
    Response response = await apiAPNSDeletePartPostWithHttpInfo(APNSID: APNSID, recordID: recordID, partID: partID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'PartListResponse') as PartListResponse;
    } else {
      return null;
    }
  }

  /// Veri Girişi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSDetailAPNSPostWithHttpInfo({String APNSID, String cargoNo, String serialNo, String general}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/DetailAPNS".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (cargoNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "cargoNo", cargoNo));
    }
    if (serialNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "serialNo", serialNo));
    }
    if (general != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "general", general));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Veri Girişi
  ///
  ///
  Future<APNSResponse> apiAPNSDetailAPNSPost({String APNSID, String cargoNo, String serialNo, String general}) async {
    Response response = await apiAPNSDetailAPNSPostWithHttpInfo(APNSID: APNSID, cargoNo: cargoNo, serialNo: serialNo, general: general);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// Değişim Girişi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSDetailChangePostWithHttpInfo({String APNSID, String recordID, ChangeType changeType}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/DetailChange".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordID", recordID));
    }
    if (changeType != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "changeType", changeType));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Değişim Girişi
  ///
  ///
  Future<APNSResponse> apiAPNSDetailChangePost({String APNSID, String recordID, ChangeType changeType}) async {
    Response response = await apiAPNSDetailChangePostWithHttpInfo(APNSID: APNSID, recordID: recordID, changeType: changeType);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// APNS Bul with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSGetAPNSPostWithHttpInfo({String aPNSNo, OperationType operationType}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/GetAPNS".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (aPNSNo != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSNo", aPNSNo));
    }
    if (operationType != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "operationType", operationType));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// APNS Bul
  ///
  ///
  Future<APNSResponse> apiAPNSGetAPNSPost({String aPNSNo, OperationType operationType}) async {
    Response response = await apiAPNSGetAPNSPostWithHttpInfo(aPNSNo: aPNSNo, operationType: operationType);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'APNSResponse') as APNSResponse;
    } else {
      return null;
    }
  }

  /// Parça Listesi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSGetAddedPartsListPostWithHttpInfo({String APNSID, String recordID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/GetAddedPartsList".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }
    if (recordID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "recordID", recordID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'POST', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// Parça Listesi
  ///
  ///
  Future<PartListResponse> apiAPNSGetAddedPartsListPost({String APNSID, String recordID}) async {
    Response response = await apiAPNSGetAddedPartsListPostWithHttpInfo(APNSID: APNSID, recordID: recordID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'PartListResponse') as PartListResponse;
    } else {
      return null;
    }
  }

  /// APNS e bağlı kontrol listesi with HTTP info returned
  ///
  ///
  Future<Response> apiAPNSGetControlListGetWithHttpInfo({String APNSID}) async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/APNS/GetControlList".replaceAll("{format}", "json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};
    if (APNSID != null) {
      queryParams.addAll(_convertParametersForCollectionFormat("", "APNSID", APNSID));
    }

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if (contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if (hasFields) postBody = mp;
    } else {}

    var response = await apiClient.invokeAPI(path, 'GET', queryParams, postBody, headerParams, formParams, contentType, authNames);
    return response;
  }

  /// APNS e bağlı kontrol listesi
  ///
  ///
  Future<ControlListResponse> apiAPNSGetControlListGet({String APNSID}) async {
    Response response = await apiAPNSGetControlListGetWithHttpInfo(APNSID: APNSID);
    if (response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'ControlListResponse') as ControlListResponse;
    } else {
      return null;
    }
  }
}

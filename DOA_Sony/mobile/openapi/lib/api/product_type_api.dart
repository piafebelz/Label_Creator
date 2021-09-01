part of openapi.api;



class ProductTypeApi {
  final ApiClient apiClient;

  ProductTypeApi([ApiClient apiClient]) : apiClient = apiClient ?? defaultApiClient;

  /// APNS Girişi Ürün Tipleri Listesi with HTTP info returned
  ///
  /// 
  Future<Response> apiProductTypeGetProductTypesGetWithHttpInfo() async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/api/ProductType/GetProductTypes".replaceAll("{format}","json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.isNotEmpty ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'GET',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);
    return response;
  }

  /// APNS Girişi Ürün Tipleri Listesi
  ///
  /// 
  Future<List<ProductTypeDTO>> apiProductTypeGetProductTypesGet() async {
    Response response = await apiProductTypeGetProductTypesGetWithHttpInfo();
    if(response.statusCode >= 400) {
      throw ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return (apiClient.deserialize(_decodeBodyBytes(response), 'List<ProductTypeDTO>') as List).map((item) => item as ProductTypeDTO).toList();
    } else {
      return null;
    }
  }

}

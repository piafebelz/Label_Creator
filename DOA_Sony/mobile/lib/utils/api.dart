import 'package:openapi/api.dart';

class ApiClientConfig {
  ApiClient get defaultConfig {
    defaultApiClient.basePath = "https://doa-sony.azurewebsites.net";
    return defaultApiClient;
  }
}

class OpenAPI {
  final apiClientConfig = ApiClientConfig();

  APNSApi get apnsApi => APNSApi(apiClientConfig.defaultConfig);

  ProductTypeApi get productTypeApi => ProductTypeApi(apiClientConfig.defaultConfig);
}

OpenAPI api = new OpenAPI();

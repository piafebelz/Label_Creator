part of openapi.api;

class APNSResponse {
  
  APNSDTO apnsdto = null;
  
  bool isSuccessful = null;
  
  String error = null;
  APNSResponse();

  @override
  String toString() {
    return 'APNSResponse[apnsdto=$apnsdto, isSuccessful=$isSuccessful, error=$error, ]';
  }

  APNSResponse.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    apnsdto = (json['apnsdto'] == null) ?
      null :
      APNSDTO.fromJson(json['apnsdto']);
    isSuccessful = json['isSuccessful'];
    error = json['error'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
      json['apnsdto'] = apnsdto;
    if (isSuccessful != null)
      json['isSuccessful'] = isSuccessful;
      json['error'] = error;
    return json;
  }

  static List<APNSResponse> listFromJson(List<dynamic> json) {
    return json == null ? List<APNSResponse>() : json.map((value) => APNSResponse.fromJson(value)).toList();
  }

  static Map<String, APNSResponse> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, APNSResponse>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = APNSResponse.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of APNSResponse-objects as value to a dart map
  static Map<String, List<APNSResponse>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<APNSResponse>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = APNSResponse.listFromJson(value);
       });
     }
     return map;
  }
}


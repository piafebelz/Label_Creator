part of openapi.api;

class ControlListResponse {
  
  List<ControlDTO> controlDTOs = [];
  
  bool isSuccessful = null;
  
  String error = null;
  ControlListResponse();

  @override
  String toString() {
    return 'ControlListResponse[controlDTOs=$controlDTOs, isSuccessful=$isSuccessful, error=$error, ]';
  }

  ControlListResponse.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    controlDTOs = (json['controlDTOs'] == null) ?
      null :
      ControlDTO.listFromJson(json['controlDTOs']);
    isSuccessful = json['isSuccessful'];
    error = json['error'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
      json['controlDTOs'] = controlDTOs;
    if (isSuccessful != null)
      json['isSuccessful'] = isSuccessful;
      json['error'] = error;
    return json;
  }

  static List<ControlListResponse> listFromJson(List<dynamic> json) {
    return json == null ? List<ControlListResponse>() : json.map((value) => ControlListResponse.fromJson(value)).toList();
  }

  static Map<String, ControlListResponse> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, ControlListResponse>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = ControlListResponse.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of ControlListResponse-objects as value to a dart map
  static Map<String, List<ControlListResponse>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<ControlListResponse>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = ControlListResponse.listFromJson(value);
       });
     }
     return map;
  }
}


part of openapi.api;

class PartListResponse {
  
  List<PartDTO> partDTOs = [];
  
  bool isSuccessful = null;
  
  String error = null;
  PartListResponse();

  @override
  String toString() {
    return 'PartListResponse[partDTOs=$partDTOs, isSuccessful=$isSuccessful, error=$error, ]';
  }

  PartListResponse.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    partDTOs = (json['partDTOs'] == null) ?
      null :
      PartDTO.listFromJson(json['partDTOs']);
    isSuccessful = json['isSuccessful'];
    error = json['error'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
      json['partDTOs'] = partDTOs;
    if (isSuccessful != null)
      json['isSuccessful'] = isSuccessful;
      json['error'] = error;
    return json;
  }

  static List<PartListResponse> listFromJson(List<dynamic> json) {
    return json == null ? List<PartListResponse>() : json.map((value) => PartListResponse.fromJson(value)).toList();
  }

  static Map<String, PartListResponse> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, PartListResponse>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = PartListResponse.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of PartListResponse-objects as value to a dart map
  static Map<String, List<PartListResponse>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<PartListResponse>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = PartListResponse.listFromJson(value);
       });
     }
     return map;
  }
}


part of openapi.api;

class ControlDTO {
  
  String controlID = null;
  
  String controlName = null;
  ControlDTO();

  @override
  String toString() {
    return 'ControlDTO[controlID=$controlID, controlName=$controlName, ]';
  }

  ControlDTO.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    controlID = json['controlID'];
    controlName = json['controlName'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
    if (controlID != null)
      json['controlID'] = controlID;
      json['controlName'] = controlName;
    return json;
  }

  static List<ControlDTO> listFromJson(List<dynamic> json) {
    return json == null ? List<ControlDTO>() : json.map((value) => ControlDTO.fromJson(value)).toList();
  }

  static Map<String, ControlDTO> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, ControlDTO>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = ControlDTO.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of ControlDTO-objects as value to a dart map
  static Map<String, List<ControlDTO>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<ControlDTO>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = ControlDTO.listFromJson(value);
       });
     }
     return map;
  }
}


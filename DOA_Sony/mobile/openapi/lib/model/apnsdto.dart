part of openapi.api;

class APNSDTO {
  
  String apnsid = null;
  
  String apnsNo = null;
  
  String productTypeID = null;
  
  String serialNo = null;
  
  String cargoNo = null;
  
  String general = null;
  
  String status = null;
  
  String recordID = null;
  APNSDTO();

  @override
  String toString() {
    return 'APNSDTO[apnsid=$apnsid, apnsNo=$apnsNo, productTypeID=$productTypeID, serialNo=$serialNo, cargoNo=$cargoNo, general=$general, status=$status, recordID=$recordID, ]';
  }

  APNSDTO.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    apnsid = json['apnsid'];
    apnsNo = json['apnsNo'];
    productTypeID = json['productTypeID'];
    serialNo = json['serialNo'];
    cargoNo = json['cargoNo'];
    general = json['general'];
    status = json['status'];
    recordID = json['recordID'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
    if (apnsid != null)
      json['apnsid'] = apnsid;
      json['apnsNo'] = apnsNo;
    if (productTypeID != null)
      json['productTypeID'] = productTypeID;
      json['serialNo'] = serialNo;
      json['cargoNo'] = cargoNo;
      json['general'] = general;
      json['status'] = status;
      json['recordID'] = recordID;
    return json;
  }

  static List<APNSDTO> listFromJson(List<dynamic> json) {
    return json == null ? List<APNSDTO>() : json.map((value) => APNSDTO.fromJson(value)).toList();
  }

  static Map<String, APNSDTO> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, APNSDTO>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = APNSDTO.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of APNSDTO-objects as value to a dart map
  static Map<String, List<APNSDTO>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<APNSDTO>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = APNSDTO.listFromJson(value);
       });
     }
     return map;
  }
}


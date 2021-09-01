part of openapi.api;

class PartDTO {
  
  String partID = null;
  
  String changeID = null;
  
  String partZCode = null;
  
  String partBarcode = null;
  
  String description = null;
  
  String partNo = null;
  
  String receiptNo = null;
  PartDTO();

  @override
  String toString() {
    return 'PartDTO[partID=$partID, changeID=$changeID, partZCode=$partZCode, partBarcode=$partBarcode, description=$description, partNo=$partNo, receiptNo=$receiptNo, ]';
  }

  PartDTO.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    partID = json['partID'];
    changeID = json['changeID'];
    partZCode = json['partZCode'];
    partBarcode = json['partBarcode'];
    description = json['description'];
    partNo = json['partNo'];
    receiptNo = json['receiptNo'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
    if (partID != null)
      json['partID'] = partID;
    if (changeID != null)
      json['changeID'] = changeID;
      json['partZCode'] = partZCode;
      json['partBarcode'] = partBarcode;
      json['description'] = description;
      json['partNo'] = partNo;
      json['receiptNo'] = receiptNo;
    return json;
  }

  static List<PartDTO> listFromJson(List<dynamic> json) {
    return json == null ? List<PartDTO>() : json.map((value) => PartDTO.fromJson(value)).toList();
  }

  static Map<String, PartDTO> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, PartDTO>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = PartDTO.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of PartDTO-objects as value to a dart map
  static Map<String, List<PartDTO>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<PartDTO>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = PartDTO.listFromJson(value);
       });
     }
     return map;
  }
}


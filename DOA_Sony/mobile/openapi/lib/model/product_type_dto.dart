part of openapi.api;

class ProductTypeDTO {
  
  String productTypeID = null;
  
  String typeName = null;
  ProductTypeDTO();

  @override
  String toString() {
    return 'ProductTypeDTO[productTypeID=$productTypeID, typeName=$typeName, ]';
  }

  ProductTypeDTO.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    productTypeID = json['productTypeID'];
    typeName = json['typeName'];
  }

  Map<String, dynamic> toJson() {
    Map <String, dynamic> json = {};
    if (productTypeID != null)
      json['productTypeID'] = productTypeID;
      json['typeName'] = typeName;
    return json;
  }

  static List<ProductTypeDTO> listFromJson(List<dynamic> json) {
    return json == null ? List<ProductTypeDTO>() : json.map((value) => ProductTypeDTO.fromJson(value)).toList();
  }

  static Map<String, ProductTypeDTO> mapFromJson(Map<String, dynamic> json) {
    var map = Map<String, ProductTypeDTO>();
    if (json != null && json.isNotEmpty) {
      json.forEach((String key, dynamic value) => map[key] = ProductTypeDTO.fromJson(value));
    }
    return map;
  }

  // maps a json object with a list of ProductTypeDTO-objects as value to a dart map
  static Map<String, List<ProductTypeDTO>> mapListFromJson(Map<String, dynamic> json) {
    var map = Map<String, List<ProductTypeDTO>>();
     if (json != null && json.isNotEmpty) {
       json.forEach((String key, dynamic value) {
         map[key] = ProductTypeDTO.listFromJson(value);
       });
     }
     return map;
  }
}


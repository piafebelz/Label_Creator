part of openapi.api;

class ChangeType {
  /// The underlying value of this enum member.
  final String value;

  const ChangeType._internal(this.value);

  static const ChangeType destruction_ = const ChangeType._internal("Destruction");
  static const ChangeType swap_ = const ChangeType._internal("Swap");
  static const ChangeType fragmentation_ = const ChangeType._internal("Fragmentation");
  static const ChangeType sales_ = const ChangeType._internal("Sales");

  static ChangeType fromJson(String value) {
    return new ChangeTypeTypeTransformer().decode(value);
  }
}

class ChangeTypeTypeTransformer {

  dynamic encode(ChangeType data) {
    return data.value;
  }

  ChangeType decode(dynamic data) {
    switch (data) {
      case "Destruction": return ChangeType.destruction_;
      case "Swap": return ChangeType.swap_;
      case "Fragmentation": return ChangeType.fragmentation_;
      case "Sales": return ChangeType.sales_;
      default: throw('Unknown enum value to decode: $data');
    }
  }
}


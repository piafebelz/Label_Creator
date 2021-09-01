part of openapi.api;

class OperationType {
  /// The underlying value of this enum member.
  final String value;

  const OperationType._internal(this.value);

  static const OperationType create_ = const OperationType._internal("Create");
  static const OperationType control_ = const OperationType._internal("Control");
  static const OperationType detail_ = const OperationType._internal("Detail");
  static const OperationType decision_ = const OperationType._internal("Decision");

  static OperationType fromJson(String value) {
    return new OperationTypeTypeTransformer().decode(value);
  }
}

class OperationTypeTypeTransformer {

  dynamic encode(OperationType data) {
    return data.value;
  }

  OperationType decode(dynamic data) {
    switch (data) {
      case "Create": return OperationType.create_;
      case "Control": return OperationType.control_;
      case "Detail": return OperationType.detail_;
      case "Decision": return OperationType.decision_;
      default: throw('Unknown enum value to decode: $data');
    }
  }
}


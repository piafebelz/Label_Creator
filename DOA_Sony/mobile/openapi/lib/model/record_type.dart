part of openapi.api;

class RecordType {
  /// The underlying value of this enum member.
  final String value;

  const RecordType._internal(this.value);

  static const RecordType return_ = const RecordType._internal("Return");
  static const RecordType repair_ = const RecordType._internal("Repair");
  static const RecordType change_ = const RecordType._internal("Change");

  static RecordType fromJson(String value) {
    return new RecordTypeTypeTransformer().decode(value);
  }
}

class RecordTypeTypeTransformer {

  dynamic encode(RecordType data) {
    return data.value;
  }

  RecordType decode(dynamic data) {
    switch (data) {
      case "Return": return RecordType.return_;
      case "Repair": return RecordType.repair_;
      case "Change": return RecordType.change_;
      default: throw('Unknown enum value to decode: $data');
    }
  }
}


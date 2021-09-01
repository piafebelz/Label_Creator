import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:sony/design_system/token/design_token.dart';

void OPDatePicker(
    {@required BuildContext context, Function onChanged, Function onConfirm}) {
  return DatePicker.showPicker(
    context,
    showTitleActions: true,
    theme: DatePickerTheme(
      backgroundColor: Colors.white,
      itemStyle: TextStyle(
        color: DesignToken.colors.text.shade500,
        fontWeight: FontWeight.bold,
      ),
      doneStyle: TextStyle(
        color: DesignToken.colors.text.shade500,
        fontSize: DesignToken.fontSize_16,
      ),
    ),
    onConfirm: onConfirm,
    onChanged: onChanged,
    pickerModel: CustomPicker(
      currentTime: DateTime.now(),
    ),
    locale: LocaleType.tr,
  );
}

class CustomPicker extends CommonPickerModel {
  String digits(int value, int length) {
    return '$value'.padLeft(
      length,
      "0",
    );
  }

  CustomPicker({
    DateTime currentTime,
    LocaleType locale,
  }) : super(locale: locale) {
    this.currentTime = currentTime ?? DateTime.now();
    this.setLeftIndex(
      this.currentTime.day,
    );
    this.setMiddleIndex(
      this.currentTime.month,
    );
    this.setRightIndex(
      this.currentTime.year,
    );
  }

  @override
  String leftStringAtIndex(int index) {
    if (index > 0 && index < 32) {
      return this.digits(index, 2);
    } else {
      return null;
    }
  }

  @override
  String middleStringAtIndex(int index) {
    if (index > 0 && index < 13) {
      return this.digits(index, 2);
    } else {
      return null;
    }
  }

  @override
  String rightStringAtIndex(int index) {
    if (index > 2000 && index < 2030) {
      return this.digits(index, 2);
    } else {
      return null;
    }
  }

  @override
  String leftDivider() {
    return "-";
  }

  @override
  String rightDivider() {
    return "-";
  }

  @override
  List<int> layoutProportions() {
    return [1, 2, 1];
  }

  @override
  DateTime finalTime() {
    return DateTime(
      this.currentRightIndex(),
      this.currentMiddleIndex(),
      this.currentLeftIndex(),
      currentTime.hour,
      currentTime.minute,
      currentTime.second,
    );
  }
}

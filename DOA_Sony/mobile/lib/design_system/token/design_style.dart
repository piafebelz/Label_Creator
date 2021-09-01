import 'package:flutter/material.dart';
import 'package:sony/design_system/token/design_token.dart';

class DesignStyle {
  static Widget verticalSpace(double height) {
    return Container(
      height: height,
    );
  }

  static Widget horizontalSpace(double width) {
    return Container(
      width: width,
    );
  }

  static Padding divider() {
    return Padding(
      padding: EdgeInsets.only(
          top: DesignToken.space_6, bottom: DesignToken.space_6),
      child: Divider(
        height: DesignToken.space_1,
      ),
    );
  }

  static Padding divider2() {
    return Padding(
      padding: EdgeInsets.only(
          top: DesignToken.space_12, bottom: DesignToken.space_12),
      child: Divider(
        height: DesignToken.space_1,
      ),
    );
  }

  static Padding divider3() {
    return Padding(
      padding: EdgeInsets.only(
          left: DesignToken.space_12,
          bottom: DesignToken.space_12,
          right: DesignToken.space_12),
      child: Divider(
        height: DesignToken.space_1,
      ),
    );
  }

  static TextStyle textStyle(Color color, double fontSize,
      [FontWeight fontWeight = FontWeight.w400, double height = 1]) {
    return TextStyle(
      fontFamily: DesignToken.fontFamilyBase,
      color: color,
      fontSize: fontSize,
      height: height,
      fontWeight: fontWeight,
      fontStyle: FontStyle.normal,
      letterSpacing: 0,
      decoration: TextDecoration.none,
    );
  }

  static TextStyle buttonTextStyle(Color color, double fontSize,
      [FontWeight fontWeight = FontWeight.w700]) {
    return TextStyle(
      fontFamily: DesignToken.fontFamilyBase,
      color: color,
      fontSize: fontSize,
      fontWeight: FontWeight.w700,
      fontStyle: FontStyle.normal,
    );
  }

  static BoxShadow boxShadow() => BoxShadow(
        color: Color(0x1932325d),
        offset: Offset(1, 1),
        blurRadius: 2,
        spreadRadius: 0,
      );

  static BoxShadow boxShadowCard() => BoxShadow(
        color: Color(0x268898aa),
        offset: Offset(0, 4),
        blurRadius: 10,
        spreadRadius: 0,
      );

  static List<BoxShadow> boxShadowButton() => [
        BoxShadow(
          color: Color(0x1932325d),
          offset: Offset(0, 4),
          blurRadius: 6,
          spreadRadius: 0,
        ),
        BoxShadow(
          color: Color(0x14000000),
          offset: Offset(0, 1),
          blurRadius: 3,
          spreadRadius: 0,
        ),
      ];

  static List<BoxShadow> boxShadowInput() => [
        BoxShadow(
          color: Color(0x05000000),
          offset: Offset(0, 1),
          blurRadius: 0,
          spreadRadius: 0,
        ),
        BoxShadow(
          color: Color(0x2632325d),
          offset: Offset(0, 1),
          blurRadius: 3,
          spreadRadius: 0,
        ),
      ];

  static BoxShadow boxShadowDrawer() => BoxShadow(
        color: Color(0x268898aa),
        offset: Offset(0, 0),
        blurRadius: 32,
        spreadRadius: 0,
      );
}

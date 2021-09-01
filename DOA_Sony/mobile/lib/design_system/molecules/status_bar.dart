import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget OPStatusBar({
  @required String title,
  @required BuildContext context,
  Color color = Colors.redAccent,
  double width = double.maxFinite,
  double height = 25,
}) {
  return Container(
    margin: EdgeInsets.only(
      top: MediaQuery.of(context).padding.top,
    ),
    padding: EdgeInsets.symmetric(vertical: 5),
    width: MediaQuery.of(context).size.width,
    decoration: BoxDecoration(color: Colors.red),
    child: Text(
      title,
      textAlign: TextAlign.center,
      style: TextStyle(
          fontSize: 12,
          fontFamily: DesignToken.fontFamilyBase,
          fontWeight: FontWeight.w200,
          color: Colors.white,
          decoration: TextDecoration.none),
    ),
  );
}

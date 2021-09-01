import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget WidgetTwo({
  @required BuildContext context,
  @required Color backgroudColor,
  @required Color middleColor,
  @required Color bottomColor,
  @required Widget middle,
  @required String title,
  @required Function onPressed,
  int counter = null,
  double leftMargin = DesignToken.space_6,
  double rightMargin = DesignToken.space_6,
}) {
  return Stack(
    alignment: Alignment.center,
    children: <Widget>[
      Container(
        height: 104,
        margin: EdgeInsets.only(left: leftMargin, right: rightMargin),
        decoration: BoxDecoration(
          color: backgroudColor,
          borderRadius: BorderRadius.circular(DesignToken.space_20),
        ),
        child: InkWell(
          onTap: onPressed,
          borderRadius: BorderRadius.circular(DesignToken.space_40),
          child: Column(
            children: <Widget>[
              Container(
                width: 54,
                height: 54,
                margin: EdgeInsets.all(DesignToken.space_12),
                decoration: BoxDecoration(
                  color: middleColor,
                  borderRadius: BorderRadius.all(
                    Radius.circular(DesignToken.space_40),
                  ),
                ),
                child: middle,
              ),
              Container(
                width: 104,
                height: 26,
                decoration: BoxDecoration(
                  color: bottomColor,
                  borderRadius: BorderRadius.only(
                    bottomLeft: Radius.circular(DesignToken.space_20),
                    bottomRight: Radius.circular(DesignToken.space_20),
                  ),
                ),
                child: Center(
                  child: Text(
//                    FlutterI18n.translate(
//                      context,
//                      title,
//                    ),
                    title,
                    style: DesignStyle.textStyle(
                      Colors.white,
                      DesignToken.fontSize_10,
                      FontWeight.w500,
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
      counter != null
          ? Container(
              width: 24,
              height: 24,
              decoration: BoxDecoration(
                color: DesignToken.colors.red.shade500,
                borderRadius: BorderRadius.all(
                  Radius.circular(DesignToken.space_20),
                ),
              ),
              child: Center(
                child: Text(
                  counter.toString(),
                  style: DesignStyle.textStyle(
                    Colors.white,
                    DesignToken.fontSize_14,
                    FontWeight.w500,
                  ),
                ),
              ),
            )
          : Container(),
    ],
  );
}

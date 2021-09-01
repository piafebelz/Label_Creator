import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/atoms/skeleton.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget WidgetOne({
  @required String title,
  @required Widget middle,
  @required String count,
  double flex = 1,
  double leftMargin = DesignToken.space_3,
  double rightMargin = DesignToken.space_3,
  bool loading = false,
}) {
  return Expanded(
    flex: 2,
    child: Container(
      margin: EdgeInsets.only(left: leftMargin, right: rightMargin),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(4),
        boxShadow: DesignStyle.boxShadowInput(),
      ),
      child: Column(
        children: <Widget>[
          Padding(
            padding: EdgeInsets.only(top: DesignToken.space_12),
            child: Text(
              title,
              style: DesignStyle.textStyle(
                DesignToken.colors.text.shade500,
                DesignToken.fontSize_12,
                FontWeight.w300,
              ),
            ),
          ),
          Padding(
            padding: EdgeInsets.only(top: DesignToken.space_10),
            child: Container(
              width: 32,
              height: 25,
              decoration: BoxDecoration(
                color: Colors.white,
              ),
              child: middle,
            ),
          ),
          Padding(
            padding: EdgeInsets.only(
                top: DesignToken.space_10, bottom: DesignToken.space_12),
            child: !loading
                ? Text(
                    count.toString(),
                    style: DesignStyle.textStyle(
                      DesignToken.colors.text.shade500,
                      DesignToken.fontSize_18,
                      FontWeight.w700,
                    ),
                  )
                : Skeleton(
                    22,
                    13,
                  ),
          ),
        ],
      ),
    ),
  );
}

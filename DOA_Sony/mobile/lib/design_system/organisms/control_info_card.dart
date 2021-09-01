import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget ControlInfoCard({
  @required BuildContext context,
  final String basket,
  final String problem,
}) {
  return Container(
    width: (MediaQuery.of(context).size.width),
    margin: EdgeInsets.all(DesignToken.space_12),
    height: 114,
    decoration: BoxDecoration(
      color: Colors.white,
      borderRadius: BorderRadius.circular(4),
      boxShadow: DesignStyle.boxShadowInput(),
    ),
    child: Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        Container(
          width: (MediaQuery.of(context).size.width),
          height: 44,
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(4),
            boxShadow: DesignStyle.boxShadowInput(),
          ),
          child: Padding(
            padding: EdgeInsets.only(
                left: DesignToken.space_16,
                top: DesignToken.space_12,
                bottom: DesignToken.space_12),
            child: Text(
              FlutterI18n.translate(
                context,
                "InboundOrderAcceptenceScreen.ControlInfoCard.Title",
              ),
              style: DesignStyle.textStyle(
                DesignToken.colors.text.shade800,
                DesignToken.fontSize_16,
                FontWeight.w500,
              ),
            ),
          ),
        ),
        Padding(
          padding: EdgeInsets.only(
            left: DesignToken.space_16,
            top: DesignToken.space_12,
            right: DesignToken.space_16,
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Text(
                FlutterI18n.translate(
                  context,
                  "InboundOrderAcceptenceScreen.ControlInfoCard.Basket",
                ),
                style: DesignStyle.textStyle(
                  DesignToken.colors.text.shade500,
                  DesignToken.fontSize_14,
                ),
              ),
              Text(
                basket,
                style: DesignStyle.textStyle(
                  DesignToken.colors.text.shade500,
                  DesignToken.fontSize_14,
                  FontWeight.w500,
                ),
              ),
            ],
          ),
        ),
        Padding(
          padding: EdgeInsets.only(
            left: DesignToken.space_16,
            top: DesignToken.space_6,
            bottom: DesignToken.space_12,
            right: DesignToken.space_16,
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: <Widget>[
              Text(
                FlutterI18n.translate(
                  context,
                  "InboundOrderAcceptenceScreen.ControlInfoCard.Problem",
                ),
                style: DesignStyle.textStyle(
                  DesignToken.colors.text.shade500,
                  DesignToken.fontSize_14,
                ),
              ),
              Text(
                problem,
                style: DesignStyle.textStyle(
                  Color(0xffffda1a),
                  DesignToken.fontSize_14,
                  FontWeight.w500,
                ),
              ),
            ],
          ),
        ),
      ],
    ),
  );
}

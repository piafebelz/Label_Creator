import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget MenuItem({
  @required BuildContext context,
  @required String title,
  @required Widget action,
}) {
  return Column(
    crossAxisAlignment: CrossAxisAlignment.start,
    children: <Widget>[
      Padding(
        padding: EdgeInsets.all(DesignToken.space_12),
        child: Text(
//          FlutterI18n.translate(
//            context,
//            title,
//          ),
          title,
          style: DesignStyle.textStyle(
            DesignToken.colors.text.shade400,
            DesignToken.fontSize_12,
            FontWeight.w300,
          ),
        ),
      ),
      action,
    ],
  );
}

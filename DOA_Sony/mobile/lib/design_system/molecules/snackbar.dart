import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/atoms/skeleton.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget OPSnackBar({
  @required BuildContext context,
  @required String title,
  EdgeInsetsGeometry margin,
  Color backgroundColor = Colors.white,
  Color shadowColor = const Color(0x268898aa),
  Color textColor = const Color(0xff101941),
  bool loading = false,
}) {
  return Container(
    width: MediaQuery.of(context).size.width,
    margin: margin,
    padding: EdgeInsets.symmetric(
        vertical: DesignToken.space_12, horizontal: DesignToken.space_20),
    decoration: BoxDecoration(
      color: !loading ? backgroundColor : Colors.white,
      boxShadow: [
        BoxShadow(
          color: !loading ? shadowColor : Color(0x268898aa),
          offset: Offset(0, 4),
          blurRadius: 10,
          spreadRadius: 0,
        )
      ],
    ),
    child: Row(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        Icon(
          FontAwesomeIcons.lightInfoCircle,
          size: DesignToken.fontSize_20,
          color: textColor,
        ),
        SizedBox(
          width: DesignToken.space_12,
        ),
        !loading
            ? Flexible(
                child: Text(
                  title,
                  style: DesignStyle.textStyle(
                    textColor,
                    DesignToken.fontSize_14,
                    FontWeight.w400,
                    1.4,
                  ),
                ),
              )
            : Skeleton(
                MediaQuery.of(context).size.width - 100,
                14,
              )
      ],
    ),
  );
}

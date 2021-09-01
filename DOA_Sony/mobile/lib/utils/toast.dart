import 'package:flushbar/flushbar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/token/design_token.dart';

class Toast {
  static Future success(BuildContext context, String text) async {
    Flushbar(
      message: FlutterI18n.translate(context, text),
      duration: Duration(seconds: 4),
      messageText: Text(
        FlutterI18n.translate(context, text),
        style: TextStyle(
          fontFamily: 'Rubik',
          color: Colors.white,
          fontSize: 14,
          fontWeight: FontWeight.w400,
          fontStyle: FontStyle.normal,
          letterSpacing: 0.25,
        ),
      ),
      icon: Icon(
        FontAwesomeIcons.regularCheckCircle,
        color: Color(0xff3cdd98),
      ),
    )..show(context);
  }

  static Future error(BuildContext context, String text) async {
    Flushbar(
      message: FlutterI18n.translate(context, text),
      duration: Duration(seconds: 4),
      messageText: Text(
        FlutterI18n.translate(context, text),
        style: TextStyle(
          fontFamily: 'Rubik',
          color: Colors.white,
          fontSize: 14,
          fontWeight: FontWeight.w400,
          fontStyle: FontStyle.normal,
          letterSpacing: 0.25,
        ),
      ),
      icon: Container(
        width: 20,
        height: 20,
        margin: EdgeInsets.only(right: DesignToken.space_8),
        decoration: BoxDecoration(
          color: Colors.red,
          shape: BoxShape.circle,
        ),
        child: Align(
          alignment: Alignment.center,
          child: Icon(
            FontAwesomeIcons.lightTimes,
            size: DesignToken.fontSize_14,
            color: Colors.white,
          ),
        ),
      ),
    )..show(context);
  }

  static Future info(BuildContext context, String text) async {
    Flushbar(
      message: FlutterI18n.translate(context, text),
      duration: Duration(seconds: 4),
      icon: Icon(
        FontAwesomeIcons.regularCheckCircle,
        color: DesignToken.colors.purple.shade100,
      ),
    )..show(context);
  }
}

import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

enum DialogAction { yes, abort }

class OPDialog {
  static Future<DialogAction> yesAbortDialog(
    BuildContext context,
    String title,
    String body,
  ) async {
    final action = await showDialog(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return Center(
          child: Container(
            margin: EdgeInsets.only(
                left: DesignToken.space_12, right: DesignToken.space_12),
            decoration: BoxDecoration(
              color: Colors.white,
              boxShadow: [
                DesignStyle.boxShadowCard(),
              ],
            ),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Padding(
                  padding: EdgeInsets.all(DesignToken.space_16),
                  child: Text(
                    title,
                    style: DesignStyle.textStyle(
                      Color(0xff4a5568),
                      DesignToken.fontSize_18,
                      FontWeight.w300,
                    ),
                  ),
                ),
                DesignStyle.divider3(),
                Padding(
                  padding: EdgeInsets.only(
                      left: DesignToken.space_12, right: DesignToken.space_12),
                  child: Padding(
                    padding: const EdgeInsets.symmetric(vertical: 20),
                    child: Center(
                      child: Text(
                        body,
                        textAlign: TextAlign.center,
                        style: DesignStyle.textStyle(
                          Color(0xff4a5568),
                          DesignToken.fontSize_16,
                          FontWeight.w400,
                        ),
                      ),
                    ),
                  ),
                ),
                Padding(
                  padding: EdgeInsets.only(top: DesignToken.space_16),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      Expanded(
                        child: OPButton(
                          context: context,
                          onPressed: () =>
                              Navigator.of(context).pop(DialogAction.abort),
                          label: "Hayır",
                          color: DesignToken.colors.text.shade300,
                          splashColor: DesignToken.colors.text.shade300,
                          textColor: Colors.white,
                          fluid: false,
                          card: false,
                          width: 150,
                          left: DesignToken.space_12,
                          right: DesignToken.space_6,
                        ),
                      ),
                      Expanded(
                        child: OPButton(
                          context: context,
                          onPressed: () =>
                              Navigator.of(context).pop(DialogAction.yes),
                          label: "Evet",
                          color: DesignToken.colors.purple.shade500,
                          splashColor: DesignToken.colors.purple.shade500,
                          textColor: Colors.white,
                          fluid: false,
                          card: false,
                          width: 150,
                          left: DesignToken.space_6,
                          right: DesignToken.space_12,
                        ),
                      ),
                    ],
                  ),
                )
              ],
            ),
          ),
        );
      },
    );
    return (action != null) ? action : DialogAction.abort;
  }
}

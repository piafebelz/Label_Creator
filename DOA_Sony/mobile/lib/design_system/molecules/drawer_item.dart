import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

class DrawerItem extends StatefulWidget {
  final String title;
  final IconData icon;
  final Color iconColor;
  final bool active;
  final String subTitle;
  final Function onPressed;

  DrawerItem(
    this.title,
    this.icon,
    this.iconColor,
    this.active,
    this.subTitle,
    this.onPressed,
  );

  @override
  State<StatefulWidget> createState() => _DrawerItemState();
}

class _DrawerItemState extends State<DrawerItem> {
  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        widget.title != ""
            ? Container(
                margin: EdgeInsets.only(
                    left: DesignToken.space_12, right: DesignToken.space_12),
                padding: EdgeInsets.only(
                    top: DesignToken.space_6,
                    bottom: DesignToken.space_6,
                    left: DesignToken.space_12,
                    right: DesignToken.space_12),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                      FlutterI18n.translate(
                        context,
                        widget.title,
                      ),
                      style: DesignStyle.textStyle(
                        DesignToken.colors.text.shade400,
                        DesignToken.fontSize_12,
                        FontWeight.w300,
                      ),
                    ),
                  ],
                ),
              )
            : Container(),
        InkWell(
          onTap: widget.onPressed,
          child: Container(
            height: 36,
            margin: EdgeInsets.only(
                left: DesignToken.space_12, right: DesignToken.space_12),
            padding: EdgeInsets.only(left: DesignToken.space_12),
            decoration: BoxDecoration(
              color: widget.active ? DesignToken.colors.purple.shade500 : null,
              borderRadius: BorderRadius.circular(4),
            ),
            child: Row(
              children: <Widget>[
                Icon(
                  widget.icon,
                  size: DesignToken.fontSize_16,
                  color: widget.active ? Colors.white : widget.iconColor,
                ),
                SizedBox(
                  width: DesignToken.space_12,
                ),
                Text(
                  FlutterI18n.translate(
                    context,
                    widget.subTitle,
                  ),
                  style: DesignStyle.textStyle(
                    widget.active
                        ? Colors.white
                        : DesignToken.colors.text.shade500,
                    DesignToken.fontSize_14,
                  ),
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }
}

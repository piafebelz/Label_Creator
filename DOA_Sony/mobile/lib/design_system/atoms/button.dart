import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

class OPButton extends StatelessWidget {
  final BuildContext context;
  final Function onPressed;
  final String label;
  final Color color;
  final Color splashColor;
  final Color textColor;
  final double textSize;
  final bool fluid;
  final bool card;
  final double left;
  final double right;
  final bool loading;
  final double height;
  final double width;
  final bool enable;

  const OPButton({
    Key key,
    @required this.context,
    @required this.onPressed,
    @required this.label,
    @required this.color,
    @required this.splashColor,
    @required this.textColor,
    this.textSize = DesignToken.fontSize_14,
    this.fluid = true,
    this.left = 0,
    this.right = 0,
    this.loading = false,
    this.height = 44,
    this.width,
    this.card = true,
    this.enable,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: fluid
          ? EdgeInsets.only(
              left: DesignToken.space_12, right: DesignToken.space_12)
          : EdgeInsets.only(left: left, right: right),
      margin: EdgeInsets.only(
        bottom: DesignToken.space_12,
      ),
      height: height,
      width: fluid
          ? MediaQuery.of(context).size.width
          : card ? MediaQuery.of(context).size.width / 2 : width,
      child: RaisedButton(
        color: color,
        shape: RoundedRectangleBorder(
          borderRadius: new BorderRadius.circular(10.0),
        ),
        onPressed: !loading ? onPressed : null,
        elevation: 3,
        splashColor: splashColor,
        child: Text(
          label,
          textAlign: TextAlign.center,
          style: DesignStyle.buttonTextStyle(
            textColor,
            textSize,
          ),
        ),
      ),
    );
  }
}

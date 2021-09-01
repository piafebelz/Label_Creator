import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

class OPTextInput extends StatefulWidget {
  final BuildContext context;
  final IconData suffixIcon;
  final IconData prefixIcon;
  final String label;
  final String value;
  final Function onChange;
  final FocusNode focusNode;
  final Function onTap;
  final Function(String) validator;
  final TextEditingController controller;
  final int maxLine;
  final int maxLength;
  OPTextInput(
      {Key key,
      this.context,
      this.suffixIcon,
      this.focusNode,
      @required this.controller,
      this.label,
      this.value,
      this.onChange,
      this.validator,
      this.onTap,
      this.prefixIcon, this.maxLine, this.maxLength})
      : super(key: key);

  @override
  _OPTextInputState createState() => _OPTextInputState();
}

class _OPTextInputState extends State<OPTextInput> {
  @override
  void initState() {
    widget.controller.text ="";
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        Material(
          elevation: 2.0,
          shadowColor: Colors.black,
          child: TextFormField(
            validator: widget.validator,
            keyboardType: TextInputType.text,
            controller: widget.controller,
            onChanged: (value){
              widget.onChange(widget.controller.value.text);
            },
            onTap: widget.onTap,
            maxLength: widget.maxLength,
            maxLines: widget.maxLine,
            focusNode: widget.focusNode,
            textInputAction: TextInputAction.done,
            decoration: InputDecoration(
                labelStyle: TextStyle(
                  fontWeight: FontWeight.w100,
                  fontStyle: FontStyle.normal,
                  color: DesignToken.colors.text.shade400,
                  letterSpacing: 0.15,
                ),
                filled: true,
                prefixIcon: widget.prefixIcon != null
                    ? Icon(
                        widget.prefixIcon,
                        size: DesignToken.fontSize_20,
                        color: DesignToken.colors.text.shade400,
                      )
                    : null,
                suffixIcon: widget.suffixIcon != null
                    ? Icon(
                        widget.suffixIcon,
                        size: DesignToken.fontSize_20,
                        color: DesignToken.colors.text.shade400,
                      )
                    : null,
                border: InputBorder.none,
                prefixStyle: TextStyle(color: Colors.red),
                hoverColor: Colors.red,
                focusColor: Colors.red,
                errorStyle:
                    DesignStyle.textStyle(Colors.red, DesignToken.fontSize_12),
                fillColor: Colors.white,
                contentPadding: EdgeInsets.only(
                    left: DesignToken.space_12,
                    top: DesignToken.space_6,
                    bottom: DesignToken.space_6,
                    right: DesignToken.space_12),
                labelText: widget.label),
            style: TextStyle(
              fontFamily: DesignToken.fontFamilyBase,
              color: DesignToken.colors.text.shade800,
              fontSize: 14,
              fontWeight: FontWeight.w300,
              fontStyle: FontStyle.normal,
              letterSpacing: 0,
              decorationColor: Colors.red,
            ),
          ),
        )
      ],
    );
  }
}

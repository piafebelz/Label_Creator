import 'package:flutter/material.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

import 'font_awesome_flutter.dart';

class NumberInput extends StatefulWidget {
  final TextEditingController counterController;
  final Function onChange;
  final int value;
  final Function(String) validator;
  NumberInput({
    Key key,
    @required this.counterController,
    @required this.onChange,
    this.value,
    this.validator,
  }) : super(key: key);

  @override
  _NumberInputState createState() => _NumberInputState();
}

class _NumberInputState extends State<NumberInput> {
  int counter = 0;
  @override
  initState() {
    super.initState();
    if (widget.value != null) {
      counter = widget.value;
    }
    widget.counterController.text = counter.toString() ?? "0";
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(top: DesignToken.space_12),
      child: Container(
        width: 312,
        height: 44,
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(4),
          boxShadow: DesignStyle.boxShadowInput(),
        ),
        child: Row(
          children: <Widget>[
            Container(
              width: 50,
              height: 44,
              decoration: BoxDecoration(
                color: Color(0xffedf2f7),
                borderRadius: BorderRadius.circular(4),
              ),
              child: IconButton(
                onPressed: () {
                  setState(
                    () {
                      if (counter != 0) counter--;
                      widget.counterController.text = counter.toString();
                      widget.onChange(counter);
                    },
                  );
                },
                icon: Icon(
                  FontAwesomeIcons.lightMinus,
                ),
                iconSize: DesignToken.fontSize_18,
                color: DesignToken.colors.text.shade400,
              ),
            ),
            Flexible(
              child: TextFormField(
                controller: widget.counterController,
                keyboardType: TextInputType.number,
                onFieldSubmitted: (value) {
                  widget.onChange(
                    widget.counterController.value.text,
                  );
                },
                validator: widget.validator,
                textAlign: TextAlign.center,
                decoration: InputDecoration(
                  border: InputBorder.none,
                  labelStyle: DesignStyle.textStyle(
                    DesignToken.colors.text.shade500,
                    DesignToken.fontSize_14,
                    FontWeight.w300,
                  ),
                ),
                onChanged: (data) {
                  setState(
                    () {
                      counter = int.parse(data);
                      widget.onChange(counter);
                    },
                  );
                },
              ),
            ),
            Container(
              width: 50,
              height: 44,
              decoration: BoxDecoration(
                color: Color(0xffedf2f7),
                borderRadius: BorderRadius.circular(4),
              ),
              child: IconButton(
                onPressed: () {
                  setState(
                    () {
                      counter++;
                      widget.counterController.text = counter.toString();
                      widget.onChange(counter);
                    },
                  );
                },
                icon: Icon(
                  FontAwesomeIcons.lightPlus,
                ),
                iconSize: DesignToken.fontSize_18,
                color: DesignToken.colors.text.shade400,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

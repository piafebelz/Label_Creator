import 'package:flutter/material.dart';

Widget StackIcon(
  IconData icon,
  Color color,
  double size,
  IconData secondIcon,
  Color secondColor,
  double secondSize,
) {
  return Stack(
    alignment: Alignment.center,
    fit: StackFit.expand,
    children: <Widget>[
      Icon(
        icon,
        color: color,
        size: size,
      ),
      Container(
        child: Icon(
          secondIcon,
          color: secondColor,
          size: secondSize,
        ),
      ),
    ],
  );
}

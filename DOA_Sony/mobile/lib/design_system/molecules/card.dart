import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';

Widget OPCard({
  @required Widget body,
}) {
  return Container(
    margin: EdgeInsets.all(DesignToken.space_12),
    padding: EdgeInsets.all(DesignToken.space_12),
    decoration: BoxDecoration(
      color: Colors.white,
      boxShadow: [
        DesignStyle.boxShadowCard(),
      ],
    ),
    child: body,
  );
}

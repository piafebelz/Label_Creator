import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:sony/design_system/atoms/skeleton.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';


Widget Header({
  @required String title,
  List<Widget> actions,
  bool loading = false,
}) {
  return PreferredSize(
    child: Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(4),
        boxShadow: DesignStyle.boxShadowInput(),
      ),
      child: AppBar(
        iconTheme: IconThemeData(
          color: DesignToken.colors.text.shade800,
        ),
        backgroundColor: DesignToken.colors.background,
        elevation: 0,
        centerTitle: true,
        title: !loading
            ? Text(
                title,
                style: DesignStyle.textStyle(
                  DesignToken.colors.text.shade800,
                  DesignToken.fontSize_16,
                  FontWeight.w500,
                ),
              )
            : Skeleton(100, 16),
        actions: actions,
      ),
    ),
    preferredSize: Size.fromHeight(
      kToolbarHeight,
    ),
  );
}

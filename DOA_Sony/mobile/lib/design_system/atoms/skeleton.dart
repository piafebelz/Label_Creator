import 'package:flutter/material.dart';
import 'package:skeleton_text/skeleton_text.dart';

Widget Skeleton(
  double width,
  double height, [
  EdgeInsetsGeometry margin = const EdgeInsets.all(0.0),
]) {
  return SkeletonAnimation(
    child: Container(
      margin: margin,
      width: width,
      height: height,
      decoration: BoxDecoration(
        color: Colors.grey[300],
      ),
    ),
  );
}

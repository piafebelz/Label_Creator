import 'package:flutter/material.dart';

class DesignToken {
  /// Rubik
  static const String fontFamilyBase = "Rubik";

  /* Font Sizes */
  /// 10
  static const double fontSize_10 = 10.0;

  /// 12
  static const double fontSize_12 = 12.0;

  /// 14
  static const double fontSize_14 = 14.0;

  /// 16
  static const double fontSize_16 = 16.0;

  /// 18
  static const double fontSize_18 = 18.0;

  /// 20
  static const double fontSize_20 = 20.0;

  /// 22
  static const double fontSize_22 = 22.0;

  /// 24
  static const double fontSize_24 = 24.0;

  /// 26
  static const double fontSize_26 = 26.0;

  /// 32
  static const double fontSize_32 = 32.0;

  /* Space Sizes */

  /// 1
  static const double space_1 = 1.0;

  /// 3
  static const double space_3 = 3.0;

  /// 4
  static const double space_4 = 4.0;

  /// 6
  static const double space_6 = 6.0;

  /// 8
  static const double space_8 = 8.0;

  /// 10
  static const double space_10 = 10.0;

  /// 12
  static const double space_12 = 12.0;

  /// 16
  static const double space_16 = 16.0;

  /// 20
  static const double space_20 = 20.0;

  /// 22
  static const double space_22 = 22.0;

  /// 24
  static const double space_24 = 24.0;

  /// 40
  static const double space_40 = 40.0;

  /* Colors */
  static OPColors get colors => OPColors();
}

const Map<int, Color> backgroundColorCodes = {
  500: Color(0xfff8f9fe),
};

const Map<int, Color> redColorCodes = {
  100: Color(0xfffedfd8),
  200: Color(0xfffeb8b1),
  300: Color(0xfffc8a8b),
  400: Color(0xfff96c7b),
  500: Color(0xfff53d62),
  600: Color(0xffd22c5d),
  700: Color(0xffb01e57),
  800: Color(0xff8e134f),
  900: Color(0xff750b49),
};

const Map<int, Color> greenColorCodes = {
  100: Color(0xffd8fdde),
  200: Color(0xffb2fbc6),
  300: Color(0xff89f4b1),
  400: Color(0xff6aeaa6),
  500: Color(0xff3cdd98),
  600: Color(0xff2bbe8d),
  700: Color(0xff1e9f81),
  800: Color(0xff138072),
  900: Color(0xff0b6a67),
};

const Map<int, Color> blueColorCodes = {
  100: Color(0xffd3f5fe),
  200: Color(0xffa7e6fd),
  300: Color(0xff7bd2fb),
  400: Color(0xff5abcf8),
  500: Color(0xff259af4),
  600: Color(0xff1b78d1),
  700: Color(0xff1259af),
  800: Color(0xff0b3f8d),
  900: Color(0xff072c75),
};

const Map<int, Color> yellowColorCodes = {
  100: Color(0xfffffbd6),
  200: Color(0xfffff5ad),
  300: Color(0xffffef84),
  400: Color(0xffffe866),
  500: Color(0xffffde33),
  600: Color(0xffdbba25),
  700: Color(0xffb79819),
  800: Color(0xff937710),
  900: Color(0xff7a6009),
};

const Map<int, Color> purpleColorCodes = {
  100: Color(0xffdde4fd),
  200: Color(0xffbcc8fb),
  300: Color(0xff98a8f5),
  400: Color(0xff7c8eec),
  500: Color(0xff5267e0),
  600: Color(0xff3b4dc0),
  700: Color(0xff2937a1),
  800: Color(0xff1a2481),
  900: Color(0xff0f176b),
};

const Map<int, Color> orangeColorCodes = {
  100: Color(0xfffeebd8),
  200: Color(0xfffed1b1),
  300: Color(0xfffcb18a),
  400: Color(0xfff9916c),
  500: Color(0xfff5603d),
  600: Color(0xffd23f2c),
  700: Color(0xffb0241e),
  800: Color(0xff8e1317),
  900: Color(0xff750b17),
};

const Map<int, Color> turquoiseColorCodes = {
  100: Color(0xffd8fef8),
  200: Color(0xffb3fdf7),
  300: Color(0xff8cfbfa),
  400: Color(0xff6eedf7),
  500: Color(0xff40d7f2),
  600: Color(0xff2eabd0),
  700: Color(0xff2082ae),
  800: Color(0xff145e8c),
  900: Color(0xff0c4474),
};

const Map<int, Color> textColorCodes = {
  100: Color(0xffe9f0f7),
  200: Color(0xffd4e1ef),
  300: Color(0xffa0aec0),
  400: Color(0xff8898aa),
  500: Color(0xff4a5568),
  600: Color(0xff364159),
  700: Color(0xff25304a),
  800: Color(0xff101941),
  900: Color(0xff0e1531),
};

class OPColors {
  MaterialColor get background =>
      MaterialColor(0xfff8f9fe, backgroundColorCodes);

  /// ![](https://i.ibb.co/KhTQqqw/red.png)
  MaterialColor get red => MaterialColor(0xfff53d62, redColorCodes);

  /// ![](https://i.ibb.co/hfCTLQ5/green.png)
  MaterialColor get green => MaterialColor(0xff3cdd98, greenColorCodes);

  /// ![](https://i.ibb.co/3vHKNbZ/blue.png)
  MaterialColor get blue => MaterialColor(0xff259af4, blueColorCodes);

  /// ![](https://i.ibb.co/ZfZr0yX/yellow.png)
  MaterialColor get yellow => MaterialColor(0xffffde33, yellowColorCodes);

  /// ![](https://i.ibb.co/3yFTbW9/purple.png)
  MaterialColor get purple => MaterialColor(0xff5267e0, purpleColorCodes);

  /// ![](https://i.ibb.co/9cz74Rh/orange.png)
  MaterialColor get orange => MaterialColor(0xfff5603d, orangeColorCodes);

  /// ![](https://i.ibb.co/z741HsP/turqoise.png)
  MaterialColor get turquoise => MaterialColor(0xff40d7f2, turquoiseColorCodes);

  MaterialColor get text => MaterialColor(0xff4a5568, textColorCodes);
}

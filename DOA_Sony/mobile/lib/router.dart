import 'package:flutter/material.dart';
import 'package:sony/screens/addAPNS.dart';
import 'package:sony/screens/addItem.dart';
import 'package:sony/screens/confirmation.dart';
import 'package:sony/screens/data_entrance.dart';
import 'package:sony/screens/detail_input_screen.dart';
import 'package:sony/screens/exchange.dart';
import 'package:sony/screens/fragmentation.dart';
import 'package:sony/screens/home.dart';
import 'package:sony/screens/input_screen.dart';
import 'package:sony/screens/product_accept_detail.dart';

class Routes {
  static String get home => "/";

  static String get inputScreen => "/inputScreen";

  static String get productAcceptDetail => "/productAcceptDetail";

  static String get dataEntrance => "/dataEntrance";

  static String get confirmation => "/confirmation";

  static String get detailInput => "/detailInput";

  static String get exchange => "/exchange";

  static String get fragmentation => "/fragmentation";

  static String get addItem => "/addItem";

  static String get addAPNS => "/addAPNS";
}

Map<String, Widget> routes = {
  /* Home */
  Routes.home: Home(),
  Routes.inputScreen: InputScreen(),
  Routes.productAcceptDetail: ProductAcceptDetail(),
  Routes.dataEntrance: DataEntrance(),
  Routes.confirmation: Confirmation(),
  Routes.detailInput: DetailInputScreen(),
  Routes.exchange: Exchange(),
  Routes.fragmentation: Fragmentation(),
  Routes.addItem: AddItem(),
  Routes.addAPNS: AddAPNS(),
};

Route<dynamic> generateRoute(RouteSettings settings) {
  return MaterialPageRoute(
    builder: (context) => routes[settings.name],
  );
}

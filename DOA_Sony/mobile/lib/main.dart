import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_i18n/flutter_i18n_delegate.dart';
import 'package:sony/router.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:sony/service_locator.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  setupLocator();
  SystemChrome.setPreferredOrientations(
    [DeviceOrientation.portraitUp, DeviceOrientation.portraitDown],
  ).whenComplete(
    () {
      runApp(
        MyApp(),
      );
    },
  );
}

class MyApp extends StatelessWidget {
  final FlutterI18nDelegate flutterI18nDelegate = FlutterI18nDelegate(
    useCountryCode: false,
    fallbackFile: "tr",
    path: 'assets/i18n',
    forcedLocale: Locale("tr"),
  );

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Sony',
      initialRoute: '/',
      onGenerateRoute: generateRoute,
      localizationsDelegates: [
        flutterI18nDelegate,
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate
      ],
    );
  }
}

import 'package:shared_preferences/shared_preferences.dart';

class UserPreferencesStorage {
  static final String accessToken = "accessToken";

  static Future<String> get(String name) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getString(name) ?? "";
  }

  static Future<bool> set(String name, String value) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.setString(name, value);
  }

  static Future<void> remove(String name) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.remove(name);
  }
}

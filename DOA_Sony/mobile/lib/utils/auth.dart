import 'dart:convert';

import 'package:http/http.dart' as http;

class Auth0 {
  final String _clientId = 'weKr21Fc86GDIu0xRnre511Z1r3WK7s0';
  final String _audience = 'https://maestro-web-int.azurewebsites.net/';
  final String _domain = 'https://oplog-maestro-int.eu.auth0.com/';

  Future<dynamic> login(String email, String password) async {
    http.Response response = null;
    await http
        .post(this._domain + "oauth/token", body: {
          "client_id": this._clientId,
          "grant_type": "http://auth0.com/oauth/grant-type/password-realm",
          "username": email,
          "password": password,
          "realm": "Username-Password-Authentication",
          "audience": this._audience
        })
        .then((http.Response onValue) => {response = onValue})
        .catchError((onError) {
          print(onError);
        });
    print(response);
    if (response.statusCode == 200) {
      return Future.value(json.decode(response.body));
    } else {
      return Future.error(json.decode(response.body));
    }
  }
}

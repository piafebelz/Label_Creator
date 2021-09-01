import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/molecules/text_input.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/form_validators.dart';
import 'package:sony/utils/toast.dart';
import '../enums/view_state.dart';
import 'base_screen.dart';

class InputScreen extends StatefulWidget {
  @override
  _InputScreenState createState() => _InputScreenState();
}

class _InputScreenState extends State<InputScreen> {
  String apnsNumber;
  String dropdownValue;

  final _formKey = GlobalKey<FormState>();
  TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(title: model.title),
        body: SafeArea(
          child: SingleChildScrollView(
            child: Form(
              key: _formKey,
              child: OPCard(
                body: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: <Widget>[
                    Text(
                      model.description,
                      style: TextStyle(
                        fontSize: DesignToken.fontSize_20,
                      ),
                    ),
                    SizedBox(
                      height: 50,
                    ),
                    OPTextInput(
                      context: context,
                      controller: controller,
                      label: "APNS Numarası",
                      validator: FormValidators.required(
                        context,
                      ),
                      onChange: (text) {
                        apnsNumber = text;
                      },
                    ),
                  ],
                ),
              ),
            ),
          ),
        ),
        bottomNavigationBar: OPButton(
          onPressed: () async {
            if (_formKey.currentState.validate()) {
              var response = await model.apiAPNSGetAPNSPost(
                operationType: model.operationType,
                aPNSNo: apnsNumber,
              );
              if (response.isSuccessful) {
                model.apnsdto = response.apnsdto;
                model.apnsNo = apnsNumber;
                model.onPressed();
              } else {
                apnsNumber = "";
                Toast.error(
                  context,
                  response.error,
                );
              }
            }
          },
          context: context,
          color: Colors.blue,
          loading: model.state == ViewState.Busy,
          label: "Devam",
          textColor: Colors.white,
          splashColor: Colors.blue[800],
        ),
      ),
    );
  }
}

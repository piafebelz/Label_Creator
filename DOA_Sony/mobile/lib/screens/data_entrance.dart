import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/molecules/text_input.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/form_validators.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';
import 'base_screen.dart';

class DataEntrance extends StatelessWidget {
  String serialNumber;
  String trackingNumber;
  String otherInformation;

  TextEditingController _controller = TextEditingController();
  TextEditingController _controller2 = TextEditingController();
  TextEditingController _controller3 = TextEditingController();

  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Veri Girişi",
        ),
        body: SafeArea(
          child: OPCard(
            body: Form(
              key: _formKey,
              child: Column(
                children: <Widget>[
                  Expanded(
                    child: ListView(
                      children: <Widget>[
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Seri No:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          validator: FormValidators.required(context),
                          controller: _controller,
                          onChange: (text) {
                            serialNumber = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Kargo No:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          validator: FormValidators.required(context),
                          controller: _controller2,
                          onChange: (text) {
                            trackingNumber = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Diğer Bilgiler:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          maxLine: 10,
                          maxLength: 500,
                          validator: FormValidators.required(context),
                          controller: _controller3,
                          onChange: (text) {
                            otherInformation = text;
                          },
                        )
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
        bottomNavigationBar: OPButton(
          onPressed: () async {
            if (_formKey.currentState.validate()) {
              final action = await OPDialog.yesAbortDialog(
                context,
                "Onay",
                "Onaylamak istiyor musunuz?",
              );
              if (action == DialogAction.yes) {
                var response = await model.apiAPNSDetailAPNSPost(
                  APNSID: model.apnsdto.apnsid,
                  cargoNo: trackingNumber,
                  serialNo: serialNumber,
                  general: otherInformation,
                );
                if (response.isSuccessful) {
                  Navigator.pushNamedAndRemoveUntil(
                    context,
                    Routes.home,
                    ModalRoute.withName(
                      Routes.home,
                    ),
                  ).whenComplete(() {
                    model.apnsdto = null;
                    model.title = null;
                    model.description = null;
                    model.operationType = null;
                    Toast.success(
                      context,
                      "İşlem başarı ile tamamlanmıştır.",
                    );
                  });
                } else {
                  Toast.error(
                    context,
                    response.error,
                  );
                }
              }
            }
          },
          loading: model.state == ViewState.Busy,
          context: context,
          color: Colors.blue,
          label: "Onayla",
          textColor: Colors.white,
          splashColor: Colors.blue[800],
        ),
      ),
    );
  }
}

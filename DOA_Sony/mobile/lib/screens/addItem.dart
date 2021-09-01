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

import 'base_screen.dart';

class AddItem extends StatelessWidget {
  TextEditingController _controller = TextEditingController();
  TextEditingController _controller2 = TextEditingController();
  TextEditingController _controller3 = TextEditingController();
  TextEditingController _controller4 = TextEditingController();
  TextEditingController _controller5 = TextEditingController();

  String itemBarcode;
  String itemPlugNo;
  String itemDescription;
  String strippedItemNo;
  String zCode;

  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Paça Ekle",
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
                            "Parça Barcodu:",
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
                            itemBarcode = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Fiş Numarası:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          controller: _controller2,
                          validator: FormValidators.required(context),
                          onChange: (text) {
                            itemPlugNo = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Parça zCode:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          controller: _controller5,
                          validator: FormValidators.required(context),
                          onChange: (text) {
                            zCode = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Parça Açıklaması:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          controller: _controller3,
                          validator: FormValidators.required(context),
                          onChange: (text) {
                            itemDescription = text;
                          },
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(0, 10, 0, 10),
                          child: Text(
                            "Sökülen Parça Numarası:",
                            style: TextStyle(
                              fontSize: DesignToken.fontSize_18,
                            ),
                          ),
                        ),
                        OPTextInput(
                          context: context,
                          controller: _controller4,
                          validator: FormValidators.required(context),
                          onChange: (text) {
                            strippedItemNo = text;
                          },
                        )
                      ],
                    ),
                  ),
                  OPButton(
                    onPressed: () async {
                      if (_formKey.currentState.validate()) {
                        final action = await OPDialog.yesAbortDialog(
                          context,
                          "Onay",
                          "Onaylamak istiyor musunuz?",
                        );
                        if (action == DialogAction.yes) {
                          var response = await model.apiAPNSCreatePartPost(
                              APNSID: model.apnsdto.apnsid,
                              recordID: model.apnsdto.recordID,
                              description: itemDescription,
                              receiptNo: strippedItemNo,
                              partZCode: zCode,
                              partBarcode: itemBarcode,
                              partNo: itemPlugNo);
                          if (response.isSuccessful) {
                            Navigator.pop(context);
                            Toast.success(
                              context,
                              "İşlem başarı ile tamamlanmıştır.",
                            );
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
                  )
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/molecules/text_input.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/form_validators.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';
import 'base_screen.dart';

class DetailInputScreen extends StatelessWidget {
  TextEditingController controller = TextEditingController();
  String descriptionText;
  final _formKey = new GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: model.detailInputScreenTitle,
        ),
        body: SafeArea(
          child: SingleChildScrollView(
            child: OPCard(
              body: Form(
                key: _formKey,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    Padding(
                      padding: const EdgeInsets.all(10.0),
                      child: Text(
                        "${model.detailInputScreenDesciption}:",
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
                      controller: controller,
                      onChange: (text) {
                        descriptionText = text;
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
              final action = await OPDialog.yesAbortDialog(
                context,
                "Onay",
                "Devam etmek istiyor musunuz?",
              );
              if (action == DialogAction.yes) {
                var response = await model.apiAPNSCreateDecisionPost(
                  APNSID: model.apnsdto.apnsid,
                  description: descriptionText,
                  recordType: model.detailRecordType,
                );
                if (response.isSuccessful) {
                  Navigator.pushNamedAndRemoveUntil(
                    context,
                    Routes.home,
                    ModalRoute.withName(
                      Routes.home,
                    ),
                  );
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
        ),
      ),
    );
  }
}

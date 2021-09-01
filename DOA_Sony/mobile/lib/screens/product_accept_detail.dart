import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/screens/checkbox_item.dart';
import 'package:sony/utils/toast.dart';

import '../enums/view_state.dart';
import '../router.dart';
import 'base_screen.dart';

class ProductAcceptDetail extends StatefulWidget {
  @override
  _ProductAcceptDetailState createState() => _ProductAcceptDetailState();
}

class _ProductAcceptDetailState extends State<ProductAcceptDetail> {
  final _formkey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      onModelReady: (model) async {
        return await model.apiAPNSGetControlListGet(
          APNSID: model.apnsdto.apnsid,
        );
      },
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Ürün Kabul",
        ),
        body: SafeArea(
          child: OPCard(
            body: Form(
              key: _formkey,
              child: Column(
                children: <Widget>[
                  OPCard(
                    body: Text(
                      "APNS Numarası: ${model.apnsdto.apnsNo}",
                      style: TextStyle(
                        fontSize: DesignToken.fontSize_24,
                      ),
                    ),
                  ),
                  Expanded(
                    child: model.state == ViewState.Busy
                        ? Center(
                            child: CircularProgressIndicator(),
                          )
                        : ListView.builder(
                            itemCount: model.productParts.length,
                            itemBuilder: (BuildContext ctxt, int index) =>
                                CheckboxFormField(
                              context: context,
                              title: Text(
                                model.productParts.keys.toList()[index],
                              ),
                              initialValue: model.productParts[
                                  model.productParts.keys.toList()[index]],
                              validator: (value) =>
                                  value ? null : "Bu alan zorunludur.",
                            ),
                          ),
                  ),
                ],
              ),
            ),
          ),
        ),
        bottomNavigationBar: OPButton(
          onPressed: () async {
            if (_formkey.currentState.validate()) {
              final action = await OPDialog.yesAbortDialog(
                context,
                "Onay",
                "Onaylamak istiyor musunuz?",
              );
              if (action == DialogAction.yes) {
                var response = await model.apiAPNSControlAPNSPost(
                  APNSID: model.apnsdto.apnsid,
                );
                if (response.isSuccessful) {
                  Navigator.pushNamedAndRemoveUntil(
                    context,
                    Routes.home,
                    ModalRoute.withName(
                      Routes.home,
                    ),
                  ).whenComplete(
                    () {
                      Toast.success(
                        context,
                        "İşlem başarı ile tamamlanmıştır.",
                      );
                      model.apnsdto = null;
                      model.productAddStatus = false;
                      model.title = null;
                      model.description = null;
                      model.operationType = null;
                    },
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

import 'package:flutter/material.dart';

import 'package:openapi/api.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';
import 'base_screen.dart';

class AddAPNS extends StatefulWidget {
  @override
  _AddAPNSState createState() => _AddAPNSState();
}

class _AddAPNSState extends State<AddAPNS> {
  String dropdownValue;

  final _formKey = new GlobalKey<FormState>();
  TextEditingController controller = TextEditingController();
  List<ProductTypeDTO> productTypes = new List();

  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      onModelReady: (model) async {
        productTypes = await model.apiProductTypeGetProductTypesGet();
      },
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Ürün Tipi Girişi",
        ),
        body: SafeArea(
          child: SingleChildScrollView(
            child: Form(
              key: _formKey,
              child: OPCard(
                body: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: <Widget>[
                    Material(
                      elevation: 2,
                      shadowColor: Colors.black,
                      child: Container(
                        color: Colors.white,
                        child: Padding(
                          padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
                          child: DropdownButtonHideUnderline(
                            child: DropdownButton<String>(
                              isExpanded: true,
                              hint: Text(
                                "Ürün Tipi",
                              ),
                              value: dropdownValue,
                              onChanged: (String newValue) {
                                setState(
                                  () {
                                    dropdownValue = newValue;
                                    productTypes.forEach(
                                      (productType) {
                                        if (productType.typeName == newValue) {
                                          model.selectedProductTypeID =
                                              productType.productTypeID;
                                        }
                                      },
                                    );
                                  },
                                );
                              },
                              items: productTypes.map<DropdownMenuItem<String>>(
                                (ProductTypeDTO item) {
                                  return DropdownMenuItem<String>(
                                    value: item.typeName,
                                    child: Text(
                                      item.typeName,
                                    ),
                                  );
                                },
                              ).toList(),
                            ),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),
        ),
        bottomNavigationBar: OPButton(
          onPressed: () async {
            if (_formKey.currentState.validate() && dropdownValue != null) {
              final action = await OPDialog.yesAbortDialog(
                context,
                "Onay",
                "Devam etmek istiyor musunuz?",
              );
              if (action == DialogAction.yes) {
                var response = await model.apiAPNSAddAPNSPost(
                  aPNSNo: model.apnsNo,
                  productTypeID: model.selectedProductTypeID,
                );
                if (response.isSuccessful) {
                  Navigator.pushNamedAndRemoveUntil(
                    context,
                    Routes.home,
                    ModalRoute.withName(
                      Routes.home,
                    ),
                  );
                  model.apnsdto = null;
                  model.apnsNo = null;
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
            } else if (dropdownValue == null) {
              Toast.error(
                context,
                "Ürün tipini seçiniz",
              );
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

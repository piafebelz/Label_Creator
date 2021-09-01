import 'package:flutter/material.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';
import 'base_screen.dart';

class Fragmentation extends StatefulWidget {
  @override
  _FragmentationState createState() => _FragmentationState();
}

class _FragmentationState extends State<Fragmentation> {
  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      onModelReady: (model) => model.apiAPNSGetAddedPartsListPost(
        APNSID: model.apnsdto.apnsid,
        recordID: model.apnsdto.recordID,
      ),
      builder: (context, child, model) => Scaffold(
        appBar: Header(title: "Parçalama", actions: [
          IconButton(
            icon: Icon(
              FontAwesomeIcons.lightMinusOctagon,
            ),
            onPressed: model.state != ViewState.Busy
                ? () async {
                    final action = await OPDialog.yesAbortDialog(
                      context,
                      "Onay",
                      "Tüm parçaları silmek istediğinize emin misiniz?",
                    );
                    if (action == DialogAction.yes) {
                      var response = await model.apiAPNSDeleteAllPartsPost(
                        APNSID: model.apnsdto.apnsid,
                        recordID: model.apnsdto.recordID,
                      );
                      if (response.isSuccessful) {
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
                : null,
          )
        ]),
        body: OPCard(
          body: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              Expanded(
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: model.fragmentationItem.partDTOs.length > 0
                      ? ListView.builder(
                          itemCount: model.fragmentationItem.partDTOs.length,
                          itemBuilder: (BuildContext ctxt, int index) => Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: <Widget>[
                              Text(
                                model.fragmentationItem.partDTOs[index]
                                    .description,
                                style: TextStyle(
                                  fontSize: DesignToken.fontSize_24,
                                ),
                              ),
                              IconButton(
                                onPressed: model.state != ViewState.Busy
                                    ? () async {
                                        final action =
                                            await OPDialog.yesAbortDialog(
                                          context,
                                          "Paça Silme",
                                          "Parçayı silmek istiyor musunuz?",
                                        );
                                        if (action == DialogAction.yes) {
                                          var response =
                                              await model.apiAPNSDeletePartPost(
                                            APNSID: model.apnsdto.apnsid,
                                            recordID: model.apnsdto.recordID,
                                            partID: model.fragmentationItem
                                                .partDTOs[index].partID,
                                          );
                                          if (response.isSuccessful) {
                                            Toast.success(
                                              context,
                                              "İşlem başarı ile tamamlanmıştır.",
                                            );
                                          } else {
                                            Toast.error(
                                                context, response.error);
                                          }
                                          Toast.info(
                                            context,
                                            "Paça başarı ile silinmiştir.",
                                          );
                                        }
                                      }
                                    : () {},
                                icon: Icon(
                                  FontAwesomeIcons.lightTrashAlt,
                                ),
                              )
                            ],
                          ),
                        )
                      : Center(
                          child: Text(
                            "Parça eklenmedi",
                          ),
                        ),
                ),
              ),
              OPButton(
                onPressed: () {
                  Navigator.pushNamed(
                    context,
                    Routes.addItem,
                  );
                },
                context: context,
                color: Colors.orange,
                label: "Paça Ekle",
                loading: model.state == ViewState.Busy,
                textColor: Colors.white,
                splashColor: Colors.blue[800],
              ),
              OPButton(
                onPressed: () async {
                  final action = await OPDialog.yesAbortDialog(
                    context,
                    "Onay",
                    "Tamamlamak istiyor musunuz?",
                  );
                  if (action == DialogAction.yes) {
                    var response = await model.apiAPNSCompleteAPNSPost(
                        APNSID: model.apnsdto.apnsid);
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
                      Toast.error(context, response.error);
                    }
                  }
                },
                context: context,
                loading: model.state == ViewState.Busy,
                color: Colors.blue,
                label: "Tamamla",
                textColor: Colors.white,
                splashColor: Colors.blue[800],
              )
            ],
          ),
        ),
      ),
    );
  }
}

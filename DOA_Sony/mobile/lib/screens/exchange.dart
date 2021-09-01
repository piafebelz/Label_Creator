import 'package:flutter/material.dart';
import 'package:openapi/api.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/organisms/menu_item.dart';
import 'package:sony/design_system/organisms/widgets/widget_two.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';
import 'base_screen.dart';

class Exchange extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Değişim",
        ),
        body: Column(
          children: <Widget>[
            MenuItem(
              context: context,
              title: "İşlemler",
              action: Row(
                children: <Widget>[
                  WidgetTwo(
                    title: "İmha",
                    context: context,
                    onPressed: () async {
                      final action = await OPDialog.yesAbortDialog(
                        context,
                        "İmha",
                        "APNS No: ${model.apnsdto.apnsNo} olan ürünün imha işlemini onaylıyor musunuz?",
                      );
                      if (action == DialogAction.yes) {
                        var response = await model.apiAPNSDetailChangePost(
                          APNSID: model.apnsdto.apnsid,
                          recordID: model.apnsdto.recordID,
                          changeType: ChangeType.destruction_,
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
                    },
                    backgroudColor: DesignToken.colors.orange.shade500,
                    middleColor: DesignToken.colors.orange.shade400,
                    bottomColor: DesignToken.colors.orange.shade600,
                    middle: Icon(
                      FontAwesomeIcons.lightConstruction,
                      size: DesignToken.fontSize_24,
                      color: DesignToken.colors.orange.shade700,
                    ),
                  ),
                  WidgetTwo(
                    title: "Swap",
                    context: context,
                    onPressed: () async {
                      final action = await OPDialog.yesAbortDialog(
                        context,
                        "Swap",
                        "APNS No: ${model.apnsdto.apnsNo} olan ürünün swap işlemini onaylıyor musunuz?",
                      );
                      if (action == DialogAction.yes) {
                        var response = await model.apiAPNSDetailChangePost(
                            APNSID: model.apnsdto.apnsid,
                            recordID: model.apnsdto.recordID,
                            changeType: ChangeType.swap_);
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
                    },
                    backgroudColor: DesignToken.colors.green.shade500,
                    middleColor: DesignToken.colors.green.shade400,
                    bottomColor: DesignToken.colors.green.shade600,
                    middle: Icon(
                      FontAwesomeIcons.lightRetweet,
                      size: DesignToken.fontSize_24,
                      color: DesignToken.colors.green.shade700,
                    ),
                  ),
                  WidgetTwo(
                    title: "Parçalama",
                    context: context,
                    onPressed: () async {
                      final action = await OPDialog.yesAbortDialog(
                        context,
                        "Parçalama",
                        "APNS No: ${model.apnsdto.apnsNo} olan ürünün parçalama işlemini onaylıyor musunuz?",
                      );
                      if (action == DialogAction.yes) {
                        var response = await model.apiAPNSDetailChangePost(
                          APNSID: model.apnsdto.apnsid,
                          changeType: ChangeType.fragmentation_,
                          recordID: model.apnsdto.recordID,
                        );
                        if (response.isSuccessful) {
                          Navigator.pushNamed(
                            context,
                            Routes.fragmentation,
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
                    },
                    backgroudColor: DesignToken.colors.red.shade500,
                    middleColor: DesignToken.colors.red.shade400,
                    bottomColor: DesignToken.colors.red.shade600,
                    middle: Icon(
                      FontAwesomeIcons.lightBoxFragile,
                      size: DesignToken.fontSize_24,
                      color: DesignToken.colors.red.shade700,
                    ),
                  ),
                ],
              ),
            ),
            MenuItem(
              context: context,
              title: "",
              action: Row(
                children: <Widget>[
                  WidgetTwo(
                    title: "Satış",
                    context: context,
                    onPressed: () async {
                      final action = await OPDialog.yesAbortDialog(
                        context,
                        "Satış",
                        "APNS No: ${model.apnsdto.apnsNo} olan ürünün satış işlemini onaylıyor musunuz?",
                      );
                      if (action == DialogAction.yes) {
                        var response = await model.apiAPNSDetailChangePost(
                          APNSID: model.apnsdto.apnsid,
                          recordID: model.apnsdto.recordID,
                          changeType: ChangeType.sales_,
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
                    },
                    backgroudColor: DesignToken.colors.turquoise.shade500,
                    middleColor: DesignToken.colors.turquoise.shade400,
                    bottomColor: DesignToken.colors.turquoise.shade600,
                    middle: Icon(
                      FontAwesomeIcons.lightMoneyBillAlt,
                      size: DesignToken.fontSize_24,
                      color: DesignToken.colors.turquoise.shade700,
                    ),
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

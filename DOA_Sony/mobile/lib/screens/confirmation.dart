import 'package:flutter/material.dart';
import 'package:openapi/api.dart';
import 'package:sony/design_system/atoms/dialog.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/molecules/card.dart';
import 'package:sony/design_system/organisms/menu_item.dart';
import 'package:sony/design_system/organisms/widgets/widget_two.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import 'package:sony/screens/base_screen.dart';
import 'package:sony/utils/toast.dart';

import '../router.dart';

class Confirmation extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Karar",
        ),
        body: SafeArea(
          child: SingleChildScrollView(
            child: Container(
              child: Column(
                children: <Widget>[
                  OPCard(
                    body: Text(
                      "APNS NO: ${model.apnsdto.apnsNo}",
                      style: TextStyle(
                        fontSize: DesignToken.fontSize_18,
                      ),
                    ),
                  ),
                  MenuItem(
                    context: context,
                    title: "İşlemler",
                    action: Row(
                      children: <Widget>[
                        WidgetTwo(
                          title: "İade",
                          context: context,
                          onPressed: () {
                            model.detailInputScreenTitle = "İade";
                            model.detailInputScreenDesciption = "İade Sebebi";
                            model.detailRecordType = RecordType.return_;
                            Navigator.pushNamed(context, Routes.detailInput);
                          },
                          backgroudColor: DesignToken.colors.purple.shade500,
                          middleColor: DesignToken.colors.purple.shade400,
                          bottomColor: DesignToken.colors.purple.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightHandHoldingBox,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.purple.shade700,
                          ),
                        ),
                        WidgetTwo(
                          title: "Onarım",
                          context: context,
                          onPressed: () {
                            model.detailInputScreenTitle = "Onarım";
                            model.detailInputScreenDesciption =
                                "Yapılan onarım";
                            model.detailRecordType = RecordType.repair_;
                            Navigator.pushNamed(context, Routes.detailInput);
                          },
                          backgroudColor: DesignToken.colors.blue.shade500,
                          middleColor: DesignToken.colors.blue.shade400,
                          bottomColor: DesignToken.colors.blue.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightFragile,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.blue.shade700,
                          ),
                        ),
                        WidgetTwo(
                          title: "Değişim",
                          context: context,
                          onPressed: () async {
                            final action = await OPDialog.yesAbortDialog(
                              context,
                              "Onay",
                              "Devam etmek istiyor musunuz?",
                            );
                            if (action == DialogAction.yes) {
                              var response =
                                  await model.apiAPNSCreateDecisionPost(
                                      APNSID: model.apnsdto.apnsid,
                                      description: "",
                                      recordType: RecordType.change_);
                              if (response.isSuccessful) {
                                model.apnsdto = response.apnsdto;
                                print(response.apnsdto);
                                Navigator.pushNamed(context, Routes.exchange);
                                Toast.success(context,
                                    "İşlem başarı ile tamamlanmıştır.");
                              } else {
                                Toast.error(context, response.error);
                              }
                            }
                          },
                          backgroudColor: DesignToken.colors.turquoise.shade500,
                          middleColor: DesignToken.colors.turquoise.shade400,
                          bottomColor: DesignToken.colors.turquoise.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightSyncAlt,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.turquoise.shade700,
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

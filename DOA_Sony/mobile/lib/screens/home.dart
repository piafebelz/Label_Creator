import 'package:flutter/material.dart';
import 'package:openapi/api.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/molecules/app_bar.dart';
import 'package:sony/design_system/organisms/menu_item.dart';
import 'package:sony/design_system/organisms/widgets/widget_two.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';
import '../design_system/token/design_style.dart';
import '../router.dart';
import 'base_screen.dart';

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BaseScreen<InputScreenBaseModel>(
      builder: (context, child, model) => Scaffold(
        appBar: Header(
          title: "Sony Servis",
        ),
        body: SafeArea(
          child: SingleChildScrollView(
            child: Container(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: <Widget>[
                  MenuItem(
                    context: context,
                    title: "İşlemler",
                    action: Row(
                      children: <Widget>[
                        WidgetTwo(
                          title: "APNS Girişi",
                          context: context,
                          onPressed: () {
                            model.title = "APNS Giriş";
                            model.description = "APNS Numarasını Giriniz:";
                            model.operationType = OperationType.create_;
                            model.onPressed = () {
                              Navigator.pushNamed(context, Routes.addAPNS);
                            };
                            Navigator.pushNamed(context, Routes.inputScreen);
                          },
                          backgroudColor: DesignToken.colors.turquoise.shade500,
                          middleColor: DesignToken.colors.turquoise.shade400,
                          bottomColor: DesignToken.colors.turquoise.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightHandReceiving,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.turquoise.shade700,
                          ),
                        ),
                        WidgetTwo(
                          title: "Ürün Kabul",
                          context: context,
                          onPressed: () {
                            model.title = "Ürün Kabul";
                            model.description = "APNS Numarasını Giriniz:";
                            model.operationType = OperationType.control_;
                            model.onPressed = () {
                              model.productAddStatus = false;
                              model.productCheckBoxTrueCount = 0;
                              Navigator.pushNamed(
                                  context, Routes.productAcceptDetail);
                            };
                            Navigator.pushNamed(context, Routes.inputScreen);
                          },
                          backgroudColor: DesignToken.colors.orange.shade500,
                          middleColor: DesignToken.colors.orange.shade400,
                          bottomColor: DesignToken.colors.orange.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightTruckLoading,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.orange.shade700,
                          ),
                        ),
                        WidgetTwo(
                          title: "Veri Girişi",
                          context: context,
                          onPressed: () {
                            model.title = "Veri Giriş";
                            model.description = "APNS Numarasını Giriniz:";
                            model.operationType = OperationType.detail_;
                            model.onPressed = () {
                              Navigator.pushNamed(context, Routes.dataEntrance);
                            };
                            Navigator.pushNamed(context, Routes.inputScreen);
                          },
                          backgroudColor: DesignToken.colors.green.shade500,
                          middleColor: DesignToken.colors.green.shade400,
                          bottomColor: DesignToken.colors.green.shade600,
                          middle: Icon(
                            FontAwesomeIcons.lightInfo,
                            size: DesignToken.fontSize_24,
                            color: DesignToken.colors.green.shade700,
                          ),
                        ),
                      ],
                    ),
                  ),
                  DesignStyle.verticalSpace(DesignToken.space_12),
                  Row(
                    children: <Widget>[
                      WidgetTwo(
                        title: "Karar",
                        context: context,
                        onPressed: () {
                          model.title = "Karar";
                          model.description = "APNS Numarasını Giriniz:";
                          model.operationType = OperationType.decision_;
                          model.onPressed = () {
                            Navigator.pushNamed(context, Routes.confirmation);
                          };
                          Navigator.pushNamed(context, Routes.inputScreen);
                        },
                        backgroudColor: DesignToken.colors.red.shade500,
                        middleColor: DesignToken.colors.red.shade400,
                        bottomColor: DesignToken.colors.red.shade600,
                        middle: Icon(
                          FontAwesomeIcons.lightUserCheck,
                          size: DesignToken.fontSize_24,
                          color: DesignToken.colors.red.shade700,
                        ),
                      ),
                    ],
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

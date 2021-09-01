import 'package:date_format/date_format.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:sony/design_system/atoms/button.dart';
import 'package:sony/design_system/atoms/font_awesome_flutter.dart';
import 'package:sony/design_system/atoms/skeleton.dart';
import 'package:sony/design_system/token/design_style.dart';
import 'package:sony/design_system/token/design_token.dart';
import 'package:sony/utils/date_time.dart';

Widget WidgetThree({
  @required BuildContext context,
  @required String operationImageUrl,
  @required String operationName,
  @required DateTime date,
  @required String poNumber,
  String label,
  @required String waybillNo,
  bool loading = false,
}) {
  return Container(
    margin: EdgeInsets.only(
        left: DesignToken.space_12,
        bottom: DesignToken.space_6,
        right: DesignToken.space_12),
    padding: EdgeInsets.only(
        left: DesignToken.space_12,
        top: DesignToken.space_12,
        right: DesignToken.space_12),
    decoration: BoxDecoration(
      color: Colors.white,
      borderRadius: BorderRadius.circular(4),
      boxShadow: DesignStyle.boxShadowInput(),
    ),
    child: Column(
      children: <Widget>[
        Row(
          children: <Widget>[
            Expanded(
              flex: 6,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  !loading
                      ? Container(
                          width: 25,
                          height: 25,
                          decoration: BoxDecoration(
                            boxShadow: [
                              DesignStyle.boxShadowCard(),
                            ],
                            image: DecorationImage(
                              fit: BoxFit.fitWidth,
                              image: AssetImage(
                                operationImageUrl,
                              ),
                            ),
                          ),
                        )
                      : Skeleton(
                          25,
                          25,
                        ),
                  DesignStyle.horizontalSpace(DesignToken.space_8),
                  !loading
                      ? Flexible(
                          child: Text(
                            operationName,
                            softWrap: false,
                            overflow: TextOverflow.ellipsis,
                            style: DesignStyle.textStyle(
                              DesignToken.colors.text.shade500,
                              DesignToken.fontSize_18,
                              FontWeight.w500,
                            ),
                          ),
                        )
                      : Flexible(
                          child: Skeleton(
                            MediaQuery.of(context).size.width,
                            18,
                          ),
                        )
                ],
              ),
            ),
            Expanded(
              flex: 4,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  Icon(
                    FontAwesomeIcons.lightCalendarAlt,
                    size: DesignToken.fontSize_14,
                  ),
                  DesignStyle.horizontalSpace(DesignToken.space_6),
                  !loading
                      ? Container(
                          width: 75,
                          child: Text(
                            date != null
                                ? formatDate(
                                    date,
                                    dateFormat,
                                  )
                                : "-",
                            style: DesignStyle.textStyle(
                              DesignToken.colors.text.shade500,
                              DesignToken.fontSize_14,
                              FontWeight.w500,
                            ),
                          ),
                        )
                      : Skeleton(
                          75,
                          14,
                        ),
                ],
              ),
            ),
          ],
        ),
        DesignStyle.divider2(),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            Text(
              FlutterI18n.translate(
                context,
                "NewOrderAcceptanceScreen.Cards.PONumber",
              ),
              style: DesignStyle.textStyle(
                DesignToken.colors.text.shade500,
                DesignToken.fontSize_12,
                FontWeight.w300,
              ),
            ),
            label != null
                ? Text(
                    FlutterI18n.translate(
                      context,
                      "NewGoodsAcceptanceScreen.Cards.Tag",
                    ),
                    style: DesignStyle.textStyle(
                      DesignToken.colors.text.shade500,
                      DesignToken.fontSize_12,
                      FontWeight.w300,
                    ),
                  )
                : Container(),
            Text(
              FlutterI18n.translate(
                context,
                "NewOrderAcceptanceScreen.Cards.OrderAcceptanceNo",
              ),
              style: DesignStyle.textStyle(
                DesignToken.colors.text.shade500,
                DesignToken.fontSize_12,
                FontWeight.w300,
              ),
            ),
          ],
        ),
        DesignStyle.verticalSpace(DesignToken.space_6),
        Row(
          children: <Widget>[
            Expanded(
              flex: 6,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  Icon(
                    FontAwesomeIcons.lightHashtag,
                    size: DesignToken.fontSize_14,
                  ),
                  SizedBox(
                    width: DesignToken.space_6,
                  ),
                  !loading
                      ? Flexible(
                          child: Text(
                            poNumber,
                            softWrap: false,
                            overflow: TextOverflow.ellipsis,
                            style: DesignStyle.textStyle(
                              DesignToken.colors.text.shade500,
                              DesignToken.fontSize_14,
                              FontWeight.w500,
                            ),
                          ),
                        )
                      : Flexible(
                          child: Skeleton(
                            MediaQuery.of(context).size.width,
                            14,
                          ),
                        )
                ],
              ),
            ),
            label != null
                ? Expanded(
                    flex: 6,
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: <Widget>[
                        Icon(
                          FontAwesomeIcons.lightHashtag,
                          size: DesignToken.fontSize_14,
                        ),
                        SizedBox(
                          width: DesignToken.space_6,
                        ),
                        !loading
                            ? Flexible(
                                child: Text(
                                  label,
                                  softWrap: false,
                                  overflow: TextOverflow.ellipsis,
                                  style: DesignStyle.textStyle(
                                    DesignToken.colors.text.shade500,
                                    DesignToken.fontSize_14,
                                    FontWeight.w500,
                                  ),
                                ),
                              )
                            : Flexible(
                                child: Skeleton(
                                  MediaQuery.of(context).size.width,
                                  14,
                                ),
                              )
                      ],
                    ),
                  )
                : Container(),
            Expanded(
              flex: 6,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: <Widget>[
                  Icon(
                    FontAwesomeIcons.lightHashtag,
                    size: DesignToken.fontSize_14,
                  ),
                  DesignStyle.horizontalSpace(DesignToken.space_6),
                  !loading
                      ? Container(
                          width: 75,
                          child: Text(
                            waybillNo,
                            softWrap: false,
                            overflow: TextOverflow.ellipsis,
                            style: DesignStyle.textStyle(
                              DesignToken.colors.text.shade500,
                              DesignToken.fontSize_14,
                              FontWeight.w500,
                            ),
                          ),
                        )
                      : Container(
                          width: 75,
                          child: Skeleton(
                            MediaQuery.of(context).size.width,
                            14,
                          ),
                        ),
                ],
              ),
            )
          ],
        ),
        Padding(
          padding: EdgeInsets.only(
              top: DesignToken.space_12, bottom: DesignToken.space_12),
          child: Divider(
            height: DesignToken.space_1,
          ),
        ),
        Align(
          alignment: Alignment.centerRight,
          child: OPButton(
            context: context,
            onPressed: () {},
            label: label != null
                ? "NewGoodsAcceptanceScreen.Cards.Button"
                : "NewOrderAcceptanceScreen.Cards.Button",
            color: DesignToken.colors.purple.shade500,
            splashColor: DesignToken.colors.purple.shade500,
            textColor: Colors.white,
            textSize: DesignToken.fontSize_12,
            height: 40,
            fluid: false,
            card: false,
            loading: loading,
          ),
        ),
      ],
    ),
  );
}

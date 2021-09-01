import 'package:openapi/api.dart';
import 'package:sony/enums/view_state.dart';
import 'package:sony/utils/api.dart';

import 'base_model.dart';

class InputScreenBaseModel extends BaseModel {
  String title;
  String description;
  Function onPressed;
  List<String> dropDownValues;
  OperationType operationType;
  String selectedProductTypeID;
  APNSDTO apnsdto;
  String apnsNo;

  //detailInoutScreen
  String detailInputScreenTitle;
  String detailInputText;
  String detailInputScreenDesciption;
  RecordType detailRecordType;

  PartListResponse fragmentationItem;

  Map<String, bool> productParts;

  int productCheckBoxTrueCount = 0;
  bool productAddStatus = false;

  InputScreenBaseModel() {
    dropDownValues = new List();
    productParts = new Map<String, bool>();
  }

  increaseCheckBoxTrueCount(int index) {
    productCheckBoxTrueCount++;
    if (productCheckBoxTrueCount == productParts.length) {
      productAddStatus = true;
      notifyListeners();
    }
  }

  decraseCheckBoxTrueCount(int index) {
    productCheckBoxTrueCount--;
    productParts[productParts.keys.toList()[index]] = false;
    productAddStatus = false;
    notifyListeners();
  }

  Future<APNSResponse> apiAPNSAddAPNSPost({String aPNSNo, String productTypeID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSAddAPNSPost(aPNSNo: aPNSNo, productTypeID: productTypeID);
    setState(ViewState.Success);
    return response;
  }

  Future<List<ProductTypeDTO>> apiProductTypeGetProductTypesGet() async {
    setState(ViewState.Busy);
    var response = await api.productTypeApi.apiProductTypeGetProductTypesGet();
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSGetAPNSPost({String aPNSNo, OperationType operationType}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSGetAPNSPost(aPNSNo: aPNSNo, operationType: operationType);
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSDetailAPNSPost({String APNSID, String cargoNo, String serialNo, String general}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSDetailAPNSPost(serialNo: serialNo, APNSID: APNSID, cargoNo: cargoNo, general: general);
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSCreateDecisionPost({String APNSID, RecordType recordType, String description}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSCreateDecisionPost(APNSID: APNSID, recordType: recordType, description: description);
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSDetailChangePost({String APNSID, String recordID, ChangeType changeType}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSDetailChangePost(changeType: changeType, recordID: recordID, APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }

  Future<ControlListResponse> apiAPNSGetControlListGet({String APNSID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSGetControlListGet(APNSID: APNSID);
    productParts = {};
    response.controlDTOs.forEach((dto) {
      productParts[dto.controlName] = false;
    });
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSControlAPNSPost({String APNSID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSControlAPNSPost(APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }

  Future<void> apiAPNSGetAddedPartsListPost({String APNSID, String recordID}) async {
    setState(ViewState.Busy);
    fragmentationItem = await api.apnsApi.apiAPNSGetAddedPartsListPost(APNSID: APNSID, recordID: recordID);
    setState(ViewState.Success);
  }

  Future<PartListResponse> apiAPNSDeletePartPost({String APNSID, String recordID, String partID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSDeletePartPost(APNSID: APNSID, recordID: recordID, partID: partID);
    await apiAPNSGetAddedPartsListPost(recordID: recordID, APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }

  Future<PartListResponse> apiAPNSDeleteAllPartsPost({String APNSID, String recordID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSDeleteAllPartsPost(APNSID: APNSID, recordID: recordID);
    await apiAPNSGetAddedPartsListPost(recordID: recordID, APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }

  Future<PartListResponse> apiAPNSCreatePartPost(
      {String APNSID, String recordID, String partZCode, String partBarcode, String description, String partNo, String receiptNo}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSCreatePartPost(
        APNSID: APNSID,
        recordID: recordID,
        description: description,
        partNo: partNo,
        partBarcode: partBarcode,
        partZCode: partZCode,
        receiptNo: receiptNo);
    await apiAPNSGetAddedPartsListPost(recordID: recordID, APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }

  Future<APNSResponse> apiAPNSCompleteAPNSPost({String APNSID}) async {
    setState(ViewState.Busy);
    var response = await api.apnsApi.apiAPNSCompleteAPNSPost(APNSID: APNSID);
    setState(ViewState.Success);
    return response;
  }
}

import 'package:flutter_i18n/flutter_i18n.dart';
import 'package:wc_form_validators/wc_form_validators.dart';

class FormValidators {
  static Function(String) required(context) {
    return Validators.required("Bu alan zorunludur.");
  }

  static Function(String) minNumber(context, number) {
    return Validators.compose(
      [
        FormValidators.required(context),
        Validators.min(
          number,
          FlutterI18n.translate(context, "Form.minNumber"),
        ),
      ],
    );
  }

  static Function(String) maxNumber(context, number) {
    return Validators.compose(
      [
        FormValidators.required(context),
        Validators.max(
          number,
          FlutterI18n.translate(context, "Form.maxNumber"),
        ),
      ],
    );
  }

  static Function(String) email(context) {
    return Validators.compose(
      [
        FormValidators.required(context),
        Validators.email(
          FlutterI18n.translate(context, "Form.email"),
        ),
      ],
    );
  }

  static Function(String) minLength(context, number) {
    return Validators.compose(
      [
        FormValidators.required(context),
        Validators.minLength(
          number,
          FlutterI18n.translate(context, "Form.minLength"),
        ),
      ],
    );
  }

  static Function(String) maxLength(context, number) {
    return Validators.compose(
      [
        FormValidators.required(context),
        Validators.maxLength(
          number,
          FlutterI18n.translate(context, "Form.maxLength"),
        ),
      ],
    );
  }

  static Function(String) pattern(context, String pattern, String i18nError) {
    return Validators.patternString(
      pattern,
      FlutterI18n.translate(context, i18nError),
    );
  }
}

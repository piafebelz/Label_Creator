import 'package:get_it/get_it.dart';
import 'package:sony/scope_models/input_screen_base_model.dart';

GetIt locator = GetIt.instance;

void setupLocator() {
  locator.registerLazySingleton<InputScreenBaseModel>(
        () => InputScreenBaseModel(),
  );
}
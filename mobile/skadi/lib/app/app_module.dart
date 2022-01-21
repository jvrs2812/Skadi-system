import 'package:flutter_modular/flutter_modular.dart';
import 'package:skadi/app/shared/guards/app_guard.dart';

import 'modules/auth/auth_module.dart';
import 'modules/home/home_module.dart';

class AppModule extends Module {
  @override
  final List<Bind> binds = [
    ...AuthModule.exports,
  ];

  @override
  final List<ModularRoute> routes = [
    ModuleRoute(Modular.initialRoute,
        module: AuthModule(), guards: [AppGuard('/')]),
    ModuleRoute('/home', module: HomeModule()),
  ];
}

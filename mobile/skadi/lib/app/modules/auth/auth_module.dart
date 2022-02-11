import 'package:flutter_modular/flutter_modular.dart';
import 'package:skadi/app/modules/auth/store/auth_store.dart';
import 'package:skadi/app/modules/auth/submodules/login/login_module.dart';
import 'package:skadi/app/modules/auth/submodules/signup/singup_module.dart';
import 'package:skadi/app/modules/auth/submodules/splash/presenter/splashpage.dart';

class AuthModule extends Module {
  static List<Bind> exports = [
    //stores
    Bind.singleton((i) => AuthStore()),
  ];

  @override
  final List<Bind> binds = [];

  @override
  final List<ModularRoute> routes = [
    ChildRoute('/', child: (_, args) => const SplashPage()),
    ModuleRoute('/login', module: LoginModule()),
    ModuleRoute('/singup', module: SingUpModule())
  ];
}

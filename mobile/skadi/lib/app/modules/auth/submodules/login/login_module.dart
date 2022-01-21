import 'package:flutter_modular/flutter_modular.dart';
import 'package:skadi/app/modules/auth/auth_module.dart';
import 'package:skadi/app/modules/auth/submodules/login/presenter/login_controller.dart';
import 'domain/usecases/authenticate_user_with_email.dart';
import 'external/datasources/api_user_datasource.dart';
import 'infra/repositories/authenticate_user_repository.dart';
import 'presenter/login_page.dart';

class LoginModule extends Module {
  @override
  final List<Bind> binds = [
    ...AuthModule.exports,

    Bind.lazySingleton((i) => LoginController(i(), i())),

    //domain
    Bind((i) => AuthenticateUserWithEmail(i())),

    //infra
    Bind((i) => AuthenticateUserRepository(i())),

    //external
    Bind((i) => ApiUserDatasource()),
  ];

  @override
  final List<ModularRoute> routes = [
    ChildRoute('/', child: (_, args) => LoginPage()),
  ];
}

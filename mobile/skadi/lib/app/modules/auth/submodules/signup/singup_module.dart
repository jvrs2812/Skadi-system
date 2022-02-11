import 'package:flutter_modular/flutter_modular.dart';
import 'package:skadi/app/modules/auth/auth_module.dart';
import 'package:skadi/app/modules/auth/submodules/signup/presenter/singup_controller.dart';
import 'package:skadi/app/modules/auth/submodules/signup/presenter/singup_page.dart';

import 'domain/entities/usecases/sing_up_with_email_password.dart';
import 'external/datasources/user_register_datasource.dart';
import 'infra/repositories/register_user_repository.dart';

class SingUpModule extends Module {
  @override
  final List<Bind> binds = [
    ...AuthModule.exports,

    Bind.lazySingleton((i) => SingUpController(i(), i())),

    //domain
    Bind((i) => SingUpWithEmailPassword(i())),

    //infra
    Bind((i) => SingUpRepository(i())),

    //external
    Bind((i) => SingUpDatasource()),
  ];

  @override
  final List<ModularRoute> routes = [
    ChildRoute('/', child: (_, args) => SingUpPage()),
  ];
}

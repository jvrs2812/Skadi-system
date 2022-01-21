import 'package:flutter_modular/flutter_modular.dart';
import 'package:skadi/app/modules/auth/auth_module.dart';
import 'package:skadi/app/modules/home/presenter/home_page.dart';

class HomeModule extends Module {
  @override
  final List<Bind> binds = [
    ...AuthModule.exports,
  ];

  @override
  final List<ModularRoute> routes = [
    ChildRoute('/', child: (_, args) => HomePage()),
  ];
}

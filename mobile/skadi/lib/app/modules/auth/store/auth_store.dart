import 'package:flutter_triple/flutter_triple.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/entities/response/user_logged.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';

class AuthStore extends StreamStore<ILoginFailure, UserLogged> {
  AuthStore() : super(UserLogged());

  UserLogged get userLogger => state;

  void setUserLogged(UserLogged user) => update(user);

  bool get isLoggedIn => state.id.isNotEmpty;
}

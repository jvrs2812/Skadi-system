import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_triple/flutter_triple.dart';
import 'package:skadi/app/modules/auth/store/auth_store.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/entities/params/login_credentials.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/usecases/authenticate_user_with_email.dart';
import 'package:asuka/asuka.dart' as asuka;

class LoginController extends StreamStore<ILoginFailure, LoginCredentials> {
  LoginController(this._authenticateUserWithEmail, this._authStore)
      : super(LoginCredentials());

  final IAuthenticateUserWithEmail _authenticateUserWithEmail;
  final AuthStore _authStore;

  void setEmail(String value) => update(state.copyWith(email: value));

  void setPassword(String value) => update(state.copyWith(password: value));

  Future<void> checkUser() async {}

  Future<void> onSingInPress(String email, String password) async {
    setLoading(true);

    final result = await _authenticateUserWithEmail(state);

    result.fold(
      onFailure: (failure) {
        update(state, force: true);
        setLoading(false);
        return asuka.showSnackBar(SnackBar(
          content: Text(
            'Falha ao entrar: ${failure.message}',
            textAlign: TextAlign.center,
          ),
          backgroundColor: Colors.red,
        ));
      },
      onSuccess: (sucess) async {
        _authStore.setUserLogged(sucess);
        update(state, force: true);
        setLoading(false);
        await Modular.to.pushNamed('/home');
      },
    );
  }
}

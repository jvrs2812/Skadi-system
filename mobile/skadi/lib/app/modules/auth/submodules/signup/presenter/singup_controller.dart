import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_triple/flutter_triple.dart';
import 'package:skadi/app/modules/auth/store/auth_store.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:asuka/asuka.dart' as asuka;
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/usecases/sing_up_with_email_password.dart';

class SingUpController extends StreamStore<ILoginFailure, UserRegister> {
  SingUpController(this._singUpDatasource, this._authStore)
      : super(UserRegister());

  final SingUpWithEmailPassword _singUpDatasource;
  final AuthStore _authStore;

  void setEmail(String value) => update(state.copyWith(email: value));

  void setPassword(String value) => update(state.copyWith(password: value));

  void setName(String value) => update(state.copyWith(name: value));

  void setCnpj(String value) => update(state.copyWith(cnpj: value));

  Future<void> checkUser() async {}

  Future<void> onRegisterInPress() async {
    setLoading(true);

    final result = await _singUpDatasource(state);

    result.fold(
      onFailure: (failure) {
        update(state, force: true);
        setLoading(false);
        return asuka.showSnackBar(SnackBar(
          content: Text(
            'Falha ao registrar: ${failure.message}',
            textAlign: TextAlign.center,
          ),
          backgroundColor: Colors.red,
        ));
      },
      onSuccess: (sucess) async {
        _authStore.state.copyWith(email: sucess.email);
        update(state, force: true);
        setLoading(false);
        asuka.showSnackBar(const SnackBar(
          content: Text(
            'Cadastro realizado com sucesso !!',
            textAlign: TextAlign.center,
          ),
          backgroundColor: Colors.green,
        ));
        await Modular.to.pushNamed('/login');
      },
    );
  }
}

import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/utils/either/login_either.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/repositories/i_sing_up_repository.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';

abstract class ISingUpWithEmailPassword {
  Future<LoginEither<ILoginFailure, UserRegister>> call(UserRegister user);
}

class SingUpWithEmailPassword implements ISingUpWithEmailPassword {
  final ISingUpRepository _repository;

  SingUpWithEmailPassword(this._repository);
  @override
  Future<LoginEither<ILoginFailure, UserRegister>> call(
      UserRegister user) async {
    if (!user.isValidParameters) {
      return FailureResponse(NotValidParamsFailure(
          message: 'Parâmetros Inválidos, tente novamente.'));
    }
    return _repository.SingUpWithEmail(user);
  }
}

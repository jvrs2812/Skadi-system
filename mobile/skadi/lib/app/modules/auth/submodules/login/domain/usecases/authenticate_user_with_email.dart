import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';
import '../entities/response/user_logged.dart';
import '../errors/login_failure.dart';
import '../repositories/i_authenticate_user_repository.dart';
import '../utils/either/login_either.dart';

abstract class IAuthenticateUserWithEmail {
  Future<LoginEither<ILoginFailure, UserLogged>> call(
      LoginCredentials credentials);
}

class AuthenticateUserWithEmail implements IAuthenticateUserWithEmail {
  final IAuthenticateUserRepository _repository;

  AuthenticateUserWithEmail(this._repository);
  @override
  Future<LoginEither<ILoginFailure, UserLogged>> call(
      LoginCredentials credentials) async {
    if (!credentials.isValidCredentials) {
      return FailureResponse(NotValidParamsFailure(
          message: 'Parâmetros Inválidos, tente novamente.'));
    }
    return _repository.authenticateWithEmail(credentials);
  }
}

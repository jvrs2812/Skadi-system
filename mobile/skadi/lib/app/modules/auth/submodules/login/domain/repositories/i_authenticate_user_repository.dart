import 'package:skadi/app/modules/auth/submodules/login/domain/utils/either/login_either.dart';

import '../entities/params/login_credentials.dart';
import '../entities/response/user_logged.dart';
import '../errors/login_failure.dart';

abstract class IAuthenticateUserRepository {
  Future<LoginEither<ILoginFailure, UserLogged>> authenticateWithEmail(
      LoginCredentials credentials);
}

import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';

import '../../domain/entities/response/user_logged.dart';

abstract class IAuthenticateUserDatasource {
  Future<UserLogged> authenticateWithEmail(LoginCredentials credentials);
}

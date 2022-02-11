import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';

import '../../domain/entities/response/user_logged.dart';
import '../../domain/errors/login_failure.dart';
import '../../domain/repositories/i_authenticate_user_repository.dart';
import '../../domain/utils/either/login_either.dart';
import '../datasources/i_authenticate_user_datasource.dart';

class AuthenticateUserRepository implements IAuthenticateUserRepository {
  final IAuthenticateUserDatasource _datasource;

  AuthenticateUserRepository(this._datasource);

  @override
  Future<LoginEither<ILoginFailure, UserLogged>> authenticateWithEmail(
      LoginCredentials credentials) async {
    try {
      return SuccessResponse(
          await _datasource.authenticateWithEmail(credentials));
    } on ILoginFailure catch (e) {
      return FailureResponse(DatasourceFailure(message: e.message));
    } catch (e) {
      return FailureResponse(DatasourceFailure(message: '$e'));
    }
  }
}

import '../../domain/entities/params/login_credentials.dart';
import '../../domain/entities/response/user_logged.dart';

abstract class IAuthenticateUserDatasource {
  Future<UserLogged> authenticateWithEmail(LoginCredentials credentials);
}

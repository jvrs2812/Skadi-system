import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';

abstract class ISingUpDatasource {
  Future<UserRegister> RegisterWithEmail(UserRegister user);
}

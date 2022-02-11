import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/utils/either/login_either.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';

abstract class ISingUpRepository {
  Future<LoginEither<ILoginFailure, UserRegister>> SingUpWithEmail(
      UserRegister user);
}

import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/utils/either/login_either.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/repositories/i_sing_up_repository.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';
import 'package:skadi/app/modules/auth/submodules/signup/infra/datasources/i_sing_up_datasource.dart';

class SingUpRepository implements ISingUpRepository {
  final ISingUpDatasource _datasource;

  SingUpRepository(this._datasource);

  @override
  Future<LoginEither<ILoginFailure, UserRegister>> SingUpWithEmail(
      UserRegister user) async {
    try {
      return SuccessResponse(await _datasource.RegisterWithEmail(user));
    } on ILoginFailure catch (e) {
      return FailureResponse(DatasourceFailure(message: e.message));
    } catch (e) {
      return FailureResponse(DatasourceFailure(message: '$e'));
    }
  }
}

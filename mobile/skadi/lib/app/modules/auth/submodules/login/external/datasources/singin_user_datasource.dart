import 'dart:convert';
import 'package:skadi/app/modules/auth/submodules/login/domain/entities/request/login_credentials.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/entities/response/user_logged.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/infra/datasources/i_authenticate_user_datasource.dart';
import 'package:skadi/app/shared/constants/constants.dart';
import 'package:http/http.dart' as http;

class ApiUserDatasource implements IAuthenticateUserDatasource {
  @override
  Future<UserLogged> authenticateWithEmail(LoginCredentials credentials) async {
    var result = await http.post(Uri.parse(baseUrl + 'login'),
        headers: requestHeaders, body: jsonEncode(credentials.toJson()));

    if (result.statusCode != 200) {
      var error = jsonDecode(result.body);
      throw DatasourceFailure(message: error['errors'][0].toString());
    }

    var data = jsonDecode(result.body)['data'];

    return UserLogged.fromJson(data);
  }
}

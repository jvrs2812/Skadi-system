import 'dart:convert';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';
import 'package:skadi/app/modules/auth/submodules/signup/infra/datasources/i_sing_up_datasource.dart';
import 'package:skadi/app/shared/constants/constants.dart';
import 'package:http/http.dart' as http;

class SingUpDatasource implements ISingUpDatasource {
  @override
  Future<UserRegister> RegisterWithEmail(UserRegister user) async {
    var result;
    try {
      result = await http.post(Uri.parse(baseUrl + 'register'),
          headers: requestHeaders, body: jsonEncode(user.toJson()));
    } catch (err) {
      print(err);
    }

    if (result.statusCode != 201) {
      var error = jsonDecode(result.body);
      throw DatasourceFailure(message: error['errors'][0].toString());
    }

    return user;
  }
}

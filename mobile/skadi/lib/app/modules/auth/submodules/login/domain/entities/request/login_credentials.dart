class LoginCredentials {
  final String email;
  final String password;

  LoginCredentials({this.email = "", this.password = ""});

  bool get isValidPassword => password.isNotEmpty;
  bool get isValidEmail => email.isNotEmpty && email.contains('@');
  bool get isValidCredentials => isValidEmail && isValidPassword;

  LoginCredentials copyWith({
    String? email,
    String? password,
  }) {
    return LoginCredentials(
        email: email ?? this.email, password: password ?? this.email);
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['email'] = email;
    data['password'] = password;
    return data;
  }
}

class UserRegister {
  final String email;
  final String name;
  final String password;
  final String cnpj;

  UserRegister(
      {this.email = '', this.name = '', this.cnpj = '', this.password = ''});

  bool get isValidPassword => password.isNotEmpty;
  bool get isValidEmail => email.isNotEmpty && email.contains('@');
  bool get isValidCnpj => cnpj.isNotEmpty && cnpj.length >= 14;
  bool get isValidName => name.isNotEmpty && name.length >= 6;
  bool get isValidParameters =>
      isValidEmail && isValidPassword && isValidCnpj && isValidName;

  UserRegister copyWith({
    String? email,
    String? name,
    String? password,
    String? cnpj,
  }) {
    return UserRegister(
      name: name ?? this.name,
      cnpj: cnpj ?? this.cnpj,
      email: email ?? this.email,
      password: password ?? this.password,
    );
  }

  factory UserRegister.fromJson(Map<String, dynamic> json) {
    return UserRegister(
        name: json['name'] ?? '',
        email: json['email'] ?? '',
        password: json['password'] ?? '',
        cnpj: json['cnpj'] ?? '');
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['email'] = email;
    data['password'] = password;
    data['cnpj'] = cnpj;
    data['name'] = name;
    return data;
  }
}

class UserLogged {
  final String id;
  final String name;
  final String email;
  final String cnpj;
  final String token;
  final DateTime tokenExpired;

  UserLogged(
      {this.id = '',
      this.name = '',
      this.email = '',
      this.cnpj = '',
      this.token = '',
      DateTime? tokenExpired})
      : tokenExpired = tokenExpired ?? DateTime.now();

  UserLogged copyWith({
    String? id,
    String? name,
    String? email,
    String? cnpj,
    String? token,
    DateTime? tokenExpired,
  }) {
    return UserLogged(
        id: id ?? this.id,
        name: name ?? this.name,
        email: email ?? this.email,
        cnpj: cnpj ?? this.cnpj,
        token: token ?? this.token,
        tokenExpired: tokenExpired ?? this.tokenExpired);
  }

  factory UserLogged.fromJson(Map<String, dynamic> json) {
    return UserLogged(
        id: json['id'] ?? '',
        name: json['name'] ?? '',
        email: json['email'] ?? '',
        cnpj: json['cnpj'] ?? '',
        token: json['token'] ?? '',
        tokenExpired: DateTime.now());
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['email'] = email;
    data['cnpj'] = cnpj;
    data['token'] = token;
    return data;
  }
}

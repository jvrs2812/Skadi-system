abstract class ILoginFailure implements Exception {
  final String? message;
  ILoginFailure({this.message});
}

abstract class EitherFailure implements Exception {
  final String? message;
  EitherFailure({this.message});
}

class NotValidParamsFailure implements ILoginFailure {
  @override
  final String? message;
  NotValidParamsFailure({this.message});

  @override
  String toString() {
    return message ?? '';
  }
}

class RepositoryFailureFailure implements ILoginFailure {
  @override
  final String? message;
  RepositoryFailureFailure({this.message});

  @override
  String toString() {
    return message ?? '';
  }
}

class DatasourceFailure implements ILoginFailure {
  @override
  final String? message;
  DatasourceFailure({this.message});

  @override
  String toString() {
    return message ?? '';
  }
}

import '../../errors/login_failure.dart';

abstract class LoginEither<TFailure, TSuccess> {
  final TFailure? _failure;
  final TSuccess? _success;
  LoginEither({TFailure? failure, TSuccess? success})
      : _failure = failure,
        _success = success,
        assert(success != null || failure != null)
  //assert(failure == null && success != null)
  ;

  T fold<T>(
      {T Function(TFailure)? onFailure, T Function(TSuccess)? onSuccess}) {
    // ignore: null_check_on_nullable_type_parameter
    if (_failure != null && onFailure != null) return onFailure(_failure!);
    // ignore: null_check_on_nullable_type_parameter
    if (_success != null && onSuccess != null) return onSuccess(_success!);
    throw ResponseNotDefinedFailure();
  }

  static SuccessResponse<TFailure, TSuccess> success<TFailure, TSuccess>(
          TSuccess value) =>
      SuccessResponse<TFailure, TSuccess>(value);

  static FailureResponse<TFailure, TSuccess> failure<TFailure, TSuccess>(
          TFailure value) =>
      FailureResponse<TFailure, TSuccess>(value);

  bool get isSuccess => _success != null;
  bool get isFailure => _failure != null;

  // ignore: unnecessary_this
  TSuccess? operator |(TSuccess? newFailureResponse) => this
      .fold(onFailure: (_) => newFailureResponse, onSuccess: (value) => value);
}

class SuccessResponse<TFailure, TSuccess>
    extends LoginEither<TFailure, TSuccess> {
  SuccessResponse(TSuccess value) : super(success: value);
}

class FailureResponse<TFailure, TSuccess>
    extends LoginEither<TFailure, TSuccess> {
  FailureResponse(TFailure value) : super(failure: value);
}

class ResponseNotDefinedFailure extends EitherFailure {
  ResponseNotDefinedFailure()
      : super(message: 'Invalid Response. Failure or Success not defined');
}

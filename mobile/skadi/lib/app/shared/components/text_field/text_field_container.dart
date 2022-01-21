import 'package:flutter/material.dart';
import 'package:skadi/app/shared/constants/constants.dart';

class TextFieldContainer extends StatefulWidget {
  final Widget? child;
  final bool? enabled;
  final TextInputType? keyboardType;
  final bool autocorrect;
  final bool isPassword;
  final Function(String)? onChanged;
  final TextEditingController? controller;
  final Function(String?)? onSaved;

  final double radius;

  final double? width;

  final String? hintText;

  final IconData? icon;

  final InputDecoration? decoration;

  final String? Function(String?)? validator;

  final FocusNode? focusNode;

  final void Function(String)? onFieldSubmitted;

  const TextFieldContainer(
      {Key? key,
      this.enabled,
      this.keyboardType,
      this.autocorrect = true,
      this.isPassword = false,
      this.onChanged,
      this.onSaved,
      this.hintText,
      this.icon,
      this.decoration,
      this.validator,
      this.child,
      this.controller,
      this.focusNode,
      this.onFieldSubmitted,
      this.radius = 29,
      this.width})
      : super(key: key);
  @override
  _TextFieldContainerState createState() => _TextFieldContainerState();
}

class _TextFieldContainerState extends State<TextFieldContainer> {
  bool isObscure = false;

  @override
  void initState() {
    super.initState();
    if (widget.isPassword) {
      isObscure = true;
    }
  }

  @override
  Widget build(BuildContext context) {
    final Size size = MediaQuery.of(context).size;

    return Container(
      margin: const EdgeInsets.symmetric(vertical: 10),
      padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 5),
      width: widget.width ?? size.width * 0.8,
      decoration: BoxDecoration(
        color: coloricon,
        borderRadius: BorderRadius.circular(widget.radius),
      ),
      child: widget.child ??
          TextFormField(
            focusNode: widget.focusNode,
            controller: widget.controller,
            obscureText: isObscure,
            enabled: widget.enabled,
            keyboardType: widget.keyboardType,
            cursorColor: backGroundColor,
            autocorrect: widget.autocorrect,
            validator: widget.validator,
            onFieldSubmitted: widget.onFieldSubmitted,
            //maxLength: 5,
            decoration: widget.decoration ??
                InputDecoration(
                  icon: Icon(
                    widget.icon,
                    color: kSecondaryColor,
                  ),
                  suffixIcon: widget.isPassword
                      ? IconButton(
                          onPressed: () {
                            setState(() {
                              //isObscure = !isObscure;
                              if (isObscure) {
                                isObscure = false;
                              } else {
                                isObscure = true;
                              }
                            });
                          },
                          icon: Icon(
                            isObscure ? Icons.visibility : Icons.visibility_off,
                            color: kSecondaryColor,
                          ),
                        )
                      : null,
                  hintText: widget.hintText ?? '',
                  border: InputBorder.none,
                ),
            onChanged: widget.onChanged,
            onSaved: widget.onSaved,
          ),
    );
  }
}

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_modular/flutter_modular.dart';
import 'package:flutter_triple/flutter_triple.dart';
import 'package:skadi/app/modules/auth/submodules/login/domain/errors/login_failure.dart';
import 'package:skadi/app/modules/auth/submodules/login/presenter/login_controller.dart';
import 'package:skadi/app/modules/auth/submodules/signup/domain/entities/request/UserRegister.dart';
import 'package:skadi/app/modules/auth/submodules/signup/presenter/singup_controller.dart';
import 'package:skadi/app/shared/components/text_field/text_field_container.dart';
import 'package:skadi/app/shared/constants/constants.dart';

class SingUpPage extends StatefulWidget {
  @override
  State<SingUpPage> createState() => _SingUpPageState();
}

class _SingUpPageState extends State<SingUpPage> {
  final GlobalKey<FormState> formKey = GlobalKey<FormState>();

  final SingUpController controller = Modular.get();
  final TextEditingController emailController = TextEditingController();
  final TextEditingController senhaController = TextEditingController();
  final TextEditingController cnpjController = TextEditingController();
  final TextEditingController nameController = TextEditingController();

  final focusNodeEmail = FocusNode();
  final focusNodePassword = FocusNode();
  final focusNodecnpj = FocusNode();
  final focusNodename = FocusNode();
  @override
  Widget build(BuildContext context) {
    final Size size = MediaQuery.of(context).size;
    return Scaffold(
      backgroundColor: backGroundColor,
      body: SizedBox(
        height: double.infinity,
        width: size.height,
        child: Stack(
          alignment: Alignment.center,
          children: [
            SingleChildScrollView(
              child: Form(
                key: formKey,
                child: ScopedBuilder<SingUpController, ILoginFailure,
                    UserRegister>(
                  store: controller,
                  onState: (context, state) {
                    return Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Image.asset(
                          "assets/images/logo.png",
                          height: size.height * 0.15,
                        ),
                        TextFieldContainer(
                          controller: nameController,
                          enabled: !controller.isLoading,
                          keyboardType: TextInputType.name,
                          autocorrect: false,
                          icon: Icons.account_box,
                          onFieldSubmitted: (_) =>
                              focusNodeEmail.requestFocus(),
                          validator: (name) {
                            if (name?.isEmpty ?? true) {
                              return 'Campo obrigatório';
                            } else if (name!.length < 6) {
                              return 'Nome precisa ser maior que 6 caracteres';
                            }
                            return null;
                          },
                          focusNode: focusNodename,
                          hintText: "Nome",
                          onChanged: controller.setName,
                          onSaved: (name) => controller.setName(name ?? ''),
                        ),
                        TextFieldContainer(
                          controller: emailController,
                          enabled: !controller.isLoading,
                          keyboardType: TextInputType.emailAddress,
                          autocorrect: false,
                          icon: Icons.email,
                          onFieldSubmitted: (_) =>
                              focusNodePassword.requestFocus(),
                          validator: (email) {
                            if (email?.isEmpty ?? true) {
                              return 'Campo obrigatório';
                            } else if (!email!.contains('@')) {
                              return 'E-mail inválido';
                            }
                            return null;
                          },
                          focusNode: focusNodeEmail,
                          hintText: "E-mail",
                          onChanged: controller.setEmail,
                          onSaved: (email) => controller.setEmail(email ?? ''),
                        ),
                        TextFieldContainer(
                          controller: senhaController,
                          enabled: !controller.isLoading,
                          isPassword: true,
                          icon: Icons.vpn_key,
                          focusNode: focusNodePassword,
                          onFieldSubmitted: (_) => focusNodePassword.unfocus(),
                          validator: (senha) {
                            if (senha?.isEmpty ?? true) {
                              return 'Campo obrigatório';
                            } else if ((senha ?? '').length < 6) {
                              return 'A senha deve ter no mínimo 6 caractéres';
                            }
                            return null;
                          },
                          hintText: "Senha",
                          onChanged: controller.setPassword,
                          onSaved: (senha) =>
                              controller.setPassword(senha ?? ''),
                        ),
                        TextFieldContainer(
                          controller: cnpjController,
                          enabled: !controller.isLoading,
                          keyboardType: TextInputType.number,
                          autocorrect: false,
                          icon: Icons.business,
                          onFieldSubmitted: (_) =>
                              focusNodePassword.requestFocus(),
                          validator: (value) {
                            if (value?.isEmpty ?? true) {
                              return 'Campo obrigatório';
                            } else if (value!.length < 14) {
                              return 'CNPJ invalido';
                            }
                            return null;
                          },
                          focusNode: focusNodecnpj,
                          hintText: "CNPJ",
                          onChanged: controller.setCnpj,
                          onSaved: (value) => controller.setCnpj(value ?? ''),
                        ),
                        ScopedBuilder<SingUpController, ILoginFailure,
                            UserRegister>(
                          store: controller,
                          onLoading: (context) {
                            return const CircularProgressIndicator(
                              valueColor: AlwaysStoppedAnimation<Color>(
                                  kSecondaryColor),
                            );
                          },
                          onState: (context, state) {
                            return Column(
                              children: [
                                Container(
                                  margin:
                                      const EdgeInsets.symmetric(vertical: 10),
                                  width: size.width * 0.8,
                                  child: ClipRRect(
                                    borderRadius: BorderRadius.circular(29),
                                    child: ElevatedButton(
                                      onPressed: () {
                                        focusNodeEmail.unfocus();
                                        focusNodePassword.unfocus();
                                        focusNodecnpj.unfocus();
                                        focusNodename.unfocus();
                                        if (formKey.currentState!.validate()) {
                                          controller.onRegisterInPress();
                                        }
                                      },
                                      style: ElevatedButton.styleFrom(
                                        padding: const EdgeInsets.symmetric(
                                            vertical: 20, horizontal: 40),
                                        primary: kSecondaryColor,
                                        onSurface:
                                            kSecondaryColor.withAlpha(100),
                                      ),
                                      child: const Text(
                                        "Registrar",
                                        style: TextStyle(color: Colors.white),
                                      ),
                                    ),
                                  ),
                                ),
                              ],
                            );
                          },
                        ),
                        SizedBox(height: size.height * 0.03),
                        InkWell(
                          onTap: () {
                            Modular.to.pushNamed('/login');
                          },
                          child: const Text(
                            'Entrar',
                            style: TextStyle(fontSize: 14, color: Colors.white),
                          ),
                        ),
                      ],
                    );
                  },
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

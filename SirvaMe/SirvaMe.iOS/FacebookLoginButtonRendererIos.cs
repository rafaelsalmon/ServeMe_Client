using System;
using System.Collections.Generic;
using Foundation;
using MonoTouch.FacebookConnect;
using SirvaMe.CustomControls;
using SirvaMe.iOS;
using SirvaMe.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRendererIos))]
namespace SirvaMe.iOS
{
    public class FacebookLoginButtonRendererIos : ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                UIButton button = Control;

                button.TouchUpInside += delegate
                {
					EfetuaLogin();
                };
            }
        }

//        private void HandleFacebookLoginClicked()
//        {
//			
//			if (FBSession.ActiveSession.IsOpen)
//            {
//                App.PostSuccessFacebookAction(FBSession.ActiveSession.AccessTokenData.AccessToken);
//            }
//            else
//            {
//				//FBSession.ActiveSession = new FBSession (new string[] { "email" });
//
//				FBSession.OpenActiveSession(new string[] { "email" },
//					true,
//					(aSession, status, error) =>
//	                {
//						if (error == null && aSession.AccessTokenData != null)
//	                    {
//	                        App.PostSuccessFacebookAction(aSession.AccessTokenData.AccessToken);
//
//
//	                    }
//	                });
//            }
//
//        }




		public void EfetuaLogin()
		{
			try
			{
				FBSession.ActiveSession = new FBSession (new string[] { "email" });

				FBSession.ActiveSession.Open(FBSessionLoginBehavior.UseSystemAccountIfPresent,
					new FBSessionStateHandler((FBSession S, FBSessionState ST, NSError ER) =>
						{
							if (ER == null && (ST == FBSessionState.Open || ST == FBSessionState.OpenTokenExtended))
							{
//								if (onLogin != null)
//									onLogin();

								FBRequestConnection.GetMe(new FBRequestHandler((FBRequestConnection RC, NSObject OBJ, NSError ERR) =>
									{
										if (ERR != null)
										{
											//Kernel.Estatisticas.Erro("Facebook: Ocorreu um erro ao coletar informações do usuário: " + ERR);
//											if(onErro != null)
//												onErro(TipoErro.Login, "Ocorreu um erro ao coletar informaçõeses do usuário: " + ERR);
										}
										else
										{
											Dictionary<String, String> DadosUsuario = new Dictionary<string,string>();
											DadosUsuario.Add("Nome", GetValor(OBJ, "first_name"));
											DadosUsuario.Add("Sobrenome", GetValor(OBJ, "last_name"));
//											DadosUsuario.Add("Sexo", (GetValor(OBJ, "gender") == "male" ? "Masculino" : "Feminino"));
//											DadosUsuario.Add("Aniversario", GetValor(OBJ, "birthday"));
											DadosUsuario.Add("Email", GetValor(OBJ, "email"));

//											DadosUsuario.Add("Cidade", OBJ.ValueForKey(new NSString("location")).ValueForKey(new NSString("name")).ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0]);
//											DadosUsuario.Add("Pais", OBJ.ValueForKey(new NSString("location")).ValueForKey(new NSString("name")).ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1]);
//
//											String EC = GetValor(OBJ, "relationship_status").ToLower();
//
//											switch (EC)
//											{
//											case "single":
//												DadosUsuario.Add("EstadoCivil", "Solteiro");
//												break;
//											case "in_relationship":
//												DadosUsuario.Add("EstadoCivil", "Em um relacionamento sério");
//												break;
//											case "married":
//												DadosUsuario.Add("EstadoCivil", "Casado");
//												break;
//											case "engaged":
//												DadosUsuario.Add("EstadoCivil", "Noivo");
//												break;
//											case "not specified":
//												DadosUsuario.Add("EstadoCivil", "Não especificado");
//												break;
//											case "in a civil union":
//												DadosUsuario.Add("EstadoCivil", "União civil");
//												break;
//											case "in a domestic partnership":
//												DadosUsuario.Add("EstadoCivil", "Uniãoo estável");
//												break;
//											case "in an open relationship":
//												DadosUsuario.Add("EstadoCivil", "Relacionamento aberto");
//												break;
//											case "it's complicated":
//												DadosUsuario.Add("EstadoCivil", "é complicado");
//												break;
//											case "separated":
//												DadosUsuario.Add("EstadoCivil", "Separado");
//												break;
//											case "divorced":
//												DadosUsuario.Add("EstadoCivil", "Divorciado");
//												break;
//											case "widowed":
//												DadosUsuario.Add("EstadoCivil", "Viúvo");
//												break;
//											default:
//												DadosUsuario.Add("EstadoCivil", EC);
//												break;
//											}

											DadosUsuario.Add("IDFacebook", GetValor(OBJ, "id"));

											DadosUsuario.Add("TokenSessaoFacebook", S.AccessTokenData.AccessToken);


											App.PostSuccessFacebookAction("");

										}
									}));
							}
							else if (ER != null)
							{
								try
								{
									S.CloseAndClearTokenInformation();
								}
								catch { }
								try
								{
									FBSession.ActiveSession.CloseAndClearTokenInformation();
								}
								catch { }

								//FBErrorCategory ERRO = FBErrorUtility.ErrorCategory(ER);

								//switch (ERRO)
								//{
								//    case FBErrorCategory.AuthenticationReopenSession:
								//    case FBErrorCategory.BadRequest:
								//    case FBErrorCategory.FacebookOther:
								//    case FBErrorCategory.Invalid:
								//    case FBErrorCategory.Permissions:
								//    case FBErrorCategory.Retry:
								//    case FBErrorCategory.Server:
								//    case FBErrorCategory.Throttling:
								//    case FBErrorCategory.UserCancelled:
								//        break;
								//}

								//Kernel.Estatisticas.Erro("Facebook: " + ER.Description);

//								if (onErro != null)
//									onErro(TipoErro.Login, "Ocorreu um erro ao tentar se cadastrar pelo facebook.\nVerifique sua conexão, se você não bloqueou o aplicativo em seu perfil do facebook e tente novamente.");
							}
							else if (ST == FBSessionState.Closed)
							{
								//Kernel.Estatisticas.Erro("Facebook: FBSessionState.Closed");
								//FinalizaLoad();

								try
								{
									S.CloseAndClearTokenInformation();
								}
								catch { }
								try
								{
									FBSession.ActiveSession.CloseAndClearTokenInformation();
								}
								catch { }

								EfetuaLogin();
							}
							else if (ST == FBSessionState.ClosedLoginFailed)
							{
								//FinalizaLoad();

								try
								{
									S.CloseAndClearTokenInformation();
								}
								catch { }
								try
								{
									FBSession.ActiveSession.CloseAndClearTokenInformation();
								}
								catch { }

								EfetuaLogin();
							}

						}));
			}
			catch (Exception ex)
			{
//				Kernel.Estatisticas.Erro("Facebook: " + ex.Message);
//				if (onErro != null)
//					onErro(TipoErro.Login, "Ocorreu um erro ao tentar se cadastrar pelo facebook.\nVerifique sua conexão, se você não bloqueou o aplicativo em seu perfil do facebook e tente novamente.");
			}
		}

		String GetValor(NSObject O, String Campo)
		{
			try
			{
				return O.ValueForKey(new NSString(Campo)).ToString();
			}
			catch
			{
				return "";
			}
		}
    }
}
using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Helper;
using MvcCadastro.Models;
using MvcCadastro.Repositorios;

namespace MvcCadastro.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUsuarioRepositorio _repositorio;
		private readonly ISessao _sessao;
		private readonly IEmail _email;
		public LoginController(IUsuarioRepositorio repositorio,
								ISessao sessao,
								IEmail email)
		{
			_repositorio = repositorio;
			_sessao = sessao;
			_email = email;
		}

		public IActionResult Index()
		{
			// Se o usuário estiver logado, redirecionar para Home

			if (_sessao.BuscarSessaoUsuario() != null)
				return RedirectToAction("Index", "Home");

			return View();
		}

		public IActionResult RedefinirSenha()
		{
			return View();
		}


		public IActionResult Sair()
		{
			_sessao.RemoverSessaoUsuario();

			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public IActionResult Entrar(LoginModel loginModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UsuarioModel usuario = _repositorio.BuscarPorLogin(loginModel.Login!);

					if (usuario != null)
					{
						if (usuario.SenhaValida(loginModel.Senha!))
						{
							_sessao.CriarSessaoUsuario(usuario);
							return RedirectToAction("Index", "Home");
						}


						TempData["MensagemErro"] = @$"Senha do Usuário é inválida.
							Por favor tente novamente!!!";
					}

					TempData["MensagemErro"] = @$"Usuario e/ou senha inválido(s).
							Por favor tente novamente!!!";

				}

				return View("Index");

			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Não conseguimos realizar o seu Login,
					detalhe do erro {erro.Message}";
				return RedirectToAction("Index");
			}

		}


		[HttpPost]
		public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var usuario = _repositorio.BuscarPorEmail_E_Login(redefinirSenhaModel.Email!, redefinirSenhaModel.Login!);

					if (usuario != null && usuario.Email != null)
					{
						string novaSenha = usuario.GerarNovaSenha();
						string mensagem = $"Sua senha foi atualizado para: {novaSenha}";
						
						bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Redefinição de Senha", mensagem);

						if (emailEnviado)
						{
							_repositorio.Atualizar(usuario);
							TempData["MensagemSucesso"] = @$"Enviamos para o seu E-mail Cadastrado
							um link Para redefinição da sua Senha. !!!";
						}
						else
						{
							TempData["MensagemErro"] = @$"Não conseguimos Enviar o E-mail.
							Por favor, tente novamente. !!!";
						}

						return RedirectToAction("Index", "Login");
					}

					TempData["MensagemErro"] = @$"Não conseguimos Redifinir sua senha.
							Por favor, revise os Dados informados!!!";

				}

				return View("Index");

			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Ops não conseguimos Redifinir sua senha,
				mais detalhes: {erro.Message}";
				return RedirectToAction("Index");
			}

		}


	}
}
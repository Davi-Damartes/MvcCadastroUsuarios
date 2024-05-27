using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Helper;
using MvcCadastro.Models;
using MvcCadastro.Repositorios;

namespace MvcCadastro.Controllers
{

	public class AlterarSenhaController : Controller
	{
		private readonly IUsuarioRepositorio _usuarioRepositorio;
		private readonly ISessao _sessao;

		public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, 
									  ISessao sessao)
		{
			_usuarioRepositorio = usuarioRepositorio;
			_sessao = sessao;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
		{
			try
			{
				UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
				alterarSenhaModel.Id = usuarioLogado.Id;
				if (ModelState.IsValid)
				{		
					_usuarioRepositorio.AtualizarSenha(alterarSenhaModel);								
					TempData["MensagemSucesso"] = "Senha Alterada com Sucesso!!!";
					return View("index", alterarSenhaModel);

				}

				return View("index", alterarSenhaModel);

			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Erro ao Alterar a sua senha,
				detalhe do erro: {erro.Message}";

				return View("index", alterarSenhaModel);

			}

		}
	}
}
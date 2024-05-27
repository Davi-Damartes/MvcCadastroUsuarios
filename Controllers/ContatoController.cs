using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Filters;
using MvcCadastro.Helper;
using MvcCadastro.Models;
using MvcCadastro.Repositorios;

namespace MvcCadastro.Controllers
{
	[PaginaRestritaSomenteAdmin]
	public class ContatoController : Controller
	{
		private readonly IContatoRepositorio _repositorio;
		private readonly ISessao _sessao;
		public ContatoController(IContatoRepositorio repositorio, ISessao sessao)
		{
			_repositorio = repositorio;
			_sessao = sessao;
		}

		public IActionResult Index()
		{
			var usuarioLogado = _sessao.BuscarSessaoUsuario();
			var contatos = _repositorio.BuscarTodos(usuarioLogado.Id);
			return View(contatos);

		}

		public IActionResult Criar()
		{
			return View();
		}
		public IActionResult Editar(Guid Id)
		{
			ContatoModel contato = _repositorio.BuscarPorId(Id);

			return View(contato);

		}

		public IActionResult ApagarConfirmacao(Guid id)
		{
			var contato = _repositorio.BuscarPorId(id);

			return View(contato);
		}

		[HttpPost]
		public IActionResult Criar(ContatoModel contato)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var usuarioLogado = _sessao.BuscarSessaoUsuario();
					contato.UsuarioId = usuarioLogado.Id;
					
					contato = _repositorio.Adicionar(contato);
					TempData["MensagemSucesso"] = "Contato Cadastrado com Sucesso!";
					return RedirectToAction("Index");
				}

				return View(contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Erro ao criar Contato ,
					detalhe do erro {erro.Message}";
				return RedirectToAction("Index");

			}

		}

		[HttpPost]

		public IActionResult Alterar(ContatoModel contato)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var usuarioLogado = _sessao.BuscarSessaoUsuario();
					contato.UsuarioId = usuarioLogado.Id;
					
					contato = _repositorio.Atualizar(contato);
					TempData["MensagemSucesso"] = "Contato Alterado com Sucesso!";
					return RedirectToAction("Index");
				}
				return View("Editar", contato);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Erro ao Alterar Contato,
					detalhe do erro {erro.Message}";

				return RedirectToAction("Index");
			}


		}


		[HttpGet]
		public IActionResult Apagar(Guid Id)
		{
			try
			{
				var apagado = _repositorio.Apagar(Id);
				if (apagado == true)
				{
					TempData["MensagemSucesso"] = "Contato Excluido com Sucesso!";
				}
				else
				{
					TempData["MensagemErro"] = @$"Não foi possível excluir o Contato!";
				}

				return RedirectToAction("Index");
			}

			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Não foi possível excluir o Contato,
				detalhes do erro [{erro.Message}]!";
				return RedirectToAction("Index");
			}

		}

	}
}
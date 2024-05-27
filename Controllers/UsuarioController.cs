using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Filters;
using MvcCadastro.Models;
using MvcCadastro.Repositorios;

namespace MvcCadastro.Controllers
{
	
	[PaginaRestritaSomenteAdmin]
	public class UsuarioController : Controller
	{
		private readonly IUsuarioRepositorio _repositorioUsuario;
		private readonly IContatoRepositorio _repositorioContatos;
		public UsuarioController(IUsuarioRepositorio repositorioUsuario,
								IContatoRepositorio repositorioContatos)
		{
			_repositorioUsuario = repositorioUsuario;
			_repositorioContatos = repositorioContatos;
		}

		public IActionResult Index()
		{
			List<UsuarioModel> contatos = _repositorioUsuario.BuscarTodos();
			
			return View(contatos);

		}

		public IActionResult Criar()
		{
			return View();
		}
		public IActionResult Editar(Guid Id)
		{
			UsuarioModel contato = _repositorioUsuario.BuscarPorId(Id);

			return View(contato);

		}

		public IActionResult ApagarConfirmacao(Guid id)
		{
			var contato = _repositorioUsuario.BuscarPorId(id);

			return View(contato);
		}
		
		public IActionResult ListarContatosPorUsuarioId(Guid id)
        {
            List<ContatoModel> contatos = _repositorioContatos.BuscarTodos(id);
            return PartialView("~/Views/Shared/PartialViews/_ContatosUsuario.cshtml", contatos);
        }


		[HttpPost]
		public IActionResult Criar(UsuarioModel usuario)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_repositorioUsuario.Adicionar(usuario);
					TempData["MensagemSucesso"] = "Usuario Cadastrado com Sucesso!";
					return RedirectToAction("Index");
				}

				return View(usuario);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Erro ao cadastrar o Usuário,
					detalhe do erro {erro.Message}";
				return RedirectToAction("Index");

			}

		}

		
		[HttpPost]
		public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
		{
			try
			{
				var usuario = new UsuarioModel();
				
				if (ModelState.IsValid)
				{
					usuario = new UsuarioModel()
					{
						Id = usuarioSemSenha.Id,	
						Nome = usuarioSemSenha.Nome,	
						Email = usuarioSemSenha.Email,	
						Login = usuarioSemSenha.Login,	
						Perfil = usuarioSemSenha.Perfil	
					};
					
					usuario = _repositorioUsuario.Atualizar(usuario);
					TempData["MensagemSucesso"] = "Usuario Alterado com Sucesso!";
					return RedirectToAction("Index");
				}
				
				return View("Editar", usuario);
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Erro ao Alterar Usuario,
					detalhe do erro {erro.Message}";

				return RedirectToAction("Index");
			}


		}


		[HttpGet]
		public IActionResult Apagar(Guid Id)
		{
			try
			{
				var apagado = _repositorioUsuario.Apagar(Id);
				if (apagado == true)
				{
					TempData["MensagemSucesso"] = "Usuário Excluido com Sucesso!";
				}
				else
				{
					TempData["MensagemErro"] = @$"Não foi possível excluir o Usuário!";
				}

				return RedirectToAction("Index");
			}

			catch (Exception erro)
			{
				TempData["MensagemErro"] = @$"Não foi possível excluir o Usuario,
				detalhes do erro [{erro.Message}]!";
				return RedirectToAction("Index");
			}

		}

	}
}
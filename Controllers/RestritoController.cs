using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Filters;

namespace MvcCadastro.Controllers
{
	[PaginaParaUsuarioLogado]
	public class RestritoController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}
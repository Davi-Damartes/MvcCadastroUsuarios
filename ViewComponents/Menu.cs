using Microsoft.AspNetCore.Mvc;
using MvcCadastro.Models;
using Newtonsoft.Json;

namespace MvcCadastro.ViewComponents
{
	public class Menu : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado")!;

			if (string.IsNullOrEmpty(sessaoUsuario))
			{
				 return Content(string.Empty);

			}

			UsuarioModel usuarioLogado = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario)!;

			return View(usuarioLogado);
		}

	}
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MvcCadastro.Models;
using Newtonsoft.Json;

namespace MvcCadastro.Filters
{
	public class PaginaParaUsuarioLogado : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			string sessaoDoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado")!;
			
			if(string.IsNullOrEmpty(sessaoDoUsuario))
			{
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary
					{ 
						{"controller", "Login"}, {"action", "Index"}
					});
			}
			else
			{
				UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoDoUsuario)!;
				
				if(usuario == null)
				{
					context.Result = new RedirectToRouteResult(
					new RouteValueDictionary
					{ 
						{"controller", "Login"}, {"action", "Index"}
					});
				}
			}
			
			base.OnActionExecuting(context);
		}



    }
}
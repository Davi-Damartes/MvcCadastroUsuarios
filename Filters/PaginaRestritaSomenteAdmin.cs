using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MvcCadastro.Models;
using Newtonsoft.Json;

namespace MvcCadastro.Filters
{
	public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
	{
		
		public override void OnActionExecuted(ActionExecutedContext context)
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
				
				if(usuario!.Perfil != Enums.PerfilEnum.Administrador)
				{
					context.Result = new RedirectToRouteResult(
					new RouteValueDictionary
					{ 
						{"controller", "Restrito"}, {"action", "Index"}
					});
					
				}
			}
			
			
			base.OnActionExecuted(context);
		}
	}
}
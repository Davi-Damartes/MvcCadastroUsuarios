using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcCadastro.Enums;

namespace MvcCadastro.Models
{
	public class UsuarioSemSenhaModel
	{
		public Guid Id { get; set; }
		
		[Required(ErrorMessage = "Digite o Nome do Usuário.")]
		public string? Nome{ get; set; }
		
		[Required(ErrorMessage = "Digite o Login do Usuário")]
		public string? Login { get; set; }
		
		[Required(ErrorMessage = "Digite o Email do Usuario.")]
		[EmailAddress(ErrorMessage = "Formato do E-mail Inválido.")]
		public string? Email { get; set; }
		

		[Required(ErrorMessage = "Infomer o Perfil do Usuario.")]
		public PerfilEnum? Perfil { get; set; }
		

		
		
	}
}
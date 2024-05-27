using System.ComponentModel.DataAnnotations;
using MvcCadastro.Enums;
using MvcCadastro.Helper;

namespace MvcCadastro.Models
{
	public class UsuarioModel
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
		
		
		
		[Required(ErrorMessage = "Digite a senha do Usuário")]
		public string? Senha { get; set; }
		
		public DateTime DataDeCadastro { get; set; }
		
		public DateTime? DataDeAlteracao { get; set; }
			
		public virtual List<ContatoModel>? Contatos {get;set;}
			
			
		public bool SenhaValida(string senha)
		{
			return Senha == senha.GerarHash();
		}
		
		public void SetSenhaHash()
		{
			Senha = Senha!.GerarHash();			
			
		}
		
		public void SetNovaSenha(string novaSenha)
		{
			Senha = novaSenha.GerarHash();			
			
		}
		
		public string GerarNovaSenha()
		{
			string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
			Senha = novaSenha.GerarHash();
			
			return novaSenha;
			
		}
		
		
	}
}
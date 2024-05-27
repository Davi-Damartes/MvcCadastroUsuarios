using System.ComponentModel.DataAnnotations;

namespace MvcCadastro.Models
{
	public class AlterarSenhaModel
	{
			
		public Guid Id { get; set; }
			
		[Required(ErrorMessage = "Digite a senha Atual do Usuário.")]
		public string? SenhaAtual { get; set; }
		
		[Required(ErrorMessage = "Digite a nova senha do Usuário.")]
		public string? NovaSenha { get; set; }
		
		
		[Required(ErrorMessage = "Confirme a nova senha do Usuário.")]
		[Compare("NovaSenha", ErrorMessage ="Ops! Parece que as novas senhas não correspondem. Verifique e tente novamente.")]
		public string? ConfirmarNovaSenha { get; set; }
	}
}
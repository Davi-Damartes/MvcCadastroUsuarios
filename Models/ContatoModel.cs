using System.ComponentModel.DataAnnotations;

namespace MvcCadastro.Models
{
    public class ContatoModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Digite o Nome do Contato.")]
		public string? Nome { get; set; }


		[Required(ErrorMessage = "Digite o Email do Contato.")]
		[EmailAddress(ErrorMessage = "Formato do E-mail Inválido.")]
		public string? Email { get; set; }


		[Required(ErrorMessage = "Digite o Telefone do Contato.")]
		[Phone(ErrorMessage = "O Telefone informado não é válido.")]
		[RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = @"
		Formato do Telegone inválido TENTE DESSA MANEIRA (XX) XXXXX-XXXX")]
		public string? Telefone { get; set; }

		public Guid? UsuarioId { get; set; }
		public UsuarioModel? Usuario { get; set; }


	}
}
using MvcCadastro.Models;

namespace MvcCadastro.Repositorios
{
	public interface IUsuarioRepositorio 
	{
		UsuarioModel BuscarPorLogin(string login);	
		UsuarioModel BuscarPorEmail_E_Login(string email, string login);	
		List<UsuarioModel> BuscarTodos();
		UsuarioModel BuscarPorId(Guid id);
		UsuarioModel Adicionar(UsuarioModel usuario);
		UsuarioModel Atualizar(UsuarioModel usuario);
		
		UsuarioModel AtualizarSenha(AlterarSenhaModel alterarSenha);
		
		bool Apagar(Guid Id);
	}
}
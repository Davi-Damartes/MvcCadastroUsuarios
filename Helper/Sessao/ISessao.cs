using MvcCadastro.Models;

namespace MvcCadastro.Helper
{
	public interface ISessao
	{
		void CriarSessaoUsuario(UsuarioModel usuario);
		void RemoverSessaoUsuario();
		UsuarioModel BuscarSessaoUsuario();
	}
}
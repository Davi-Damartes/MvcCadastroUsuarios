using MvcCadastro.Models;

namespace MvcCadastro.Repositorios
{
    public interface IContatoRepositorio
	{
		List<ContatoModel> BuscarTodos(Guid UsuarioId);
		ContatoModel BuscarPorId(Guid id);
		
		ContatoModel Adicionar(ContatoModel contato);
		
		ContatoModel Atualizar(ContatoModel contato);		
		bool Apagar(Guid Id);
	}
}
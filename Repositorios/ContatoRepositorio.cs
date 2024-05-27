using MvcCadastro.Data;
using MvcCadastro.Models;

namespace MvcCadastro.Repositorios
{
    public class ContatoRepositorio : IContatoRepositorio
	{
		private readonly MeuBancoContext _context;

		public ContatoRepositorio(MeuBancoContext context)
		{
			_context = context;
		}

		public ContatoModel Adicionar(ContatoModel contato)
		{
			_context.Contatos.Add(contato);
			_context.SaveChanges();
			return contato;
		}

		public ContatoModel BuscarPorId(Guid id)
		{
			var result = _context.Contatos.FirstOrDefault(x => x.Id == id);
			if (result == null)
			{
				return null!;
			}

			return result;

		}

		public List<ContatoModel> BuscarTodos(Guid usuarioId)
		{
			var result = _context.Contatos
						.Where(x=> x.UsuarioId == usuarioId)
						.ToList();
			return result;

		}

		public ContatoModel Atualizar(ContatoModel contato)
		{
			var contatoDb = BuscarPorId(contato.Id);

			if (contatoDb == null)
			{
				throw new Exception("Erro na atualização do Contato!!!");
			}
			contatoDb.Nome = contato.Nome;
			contatoDb.Email = contato.Email;
			contatoDb.Telefone = contato.Telefone;

			_context.Contatos.Update(contatoDb);
			_context.SaveChanges();
			return contatoDb;
		}

		public bool Apagar(Guid Id)
		{
			var contatoDb = BuscarPorId(Id);
			if (contatoDb == null)
			{
				throw new Exception("Erro na Exclusão do Contato!!!");
			}
			
			_context.Contatos.Remove(contatoDb);
			_context.SaveChanges();
			return true;
			
		}
	}
}
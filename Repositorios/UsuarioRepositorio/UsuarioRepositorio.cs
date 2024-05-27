using Microsoft.EntityFrameworkCore;
using MvcCadastro.Data;
using MvcCadastro.Models;

namespace MvcCadastro.Repositorios
{
	public class UsuarioRepositorio : IUsuarioRepositorio
	{
		private readonly MeuBancoContext _context;

		public UsuarioRepositorio(MeuBancoContext context)
		{
			_context = context;
		}

		public UsuarioModel Adicionar(UsuarioModel usuario)
		{
			usuario.DataDeCadastro = DateTime.Now;
			
			usuario.SetSenhaHash();
			_context.Usuarios.Add(usuario);
			_context.SaveChanges();
			return usuario;
		}

		public UsuarioModel BuscarPorId(Guid id)
		{
			var result = _context.Usuarios.FirstOrDefault(x => x.Id == id);
			if (result == null)
			{
				return null!;
			}

			return result;

		}

		public List<UsuarioModel> BuscarTodos()
		{
			var result = _context.Usuarios
								.Include(x => x.Contatos)
								.ToList();
			return result;

		}

		public UsuarioModel Atualizar(UsuarioModel usuario)
		{
			var usuarioDb = BuscarPorId(usuario.Id);

			if (usuarioDb == null)
			{
				throw new Exception("Erro na atualização do Usuario!!!");
			}
			usuarioDb.Nome = usuario.Nome;
			usuarioDb.Login = usuario.Login;
			usuarioDb.Email = usuario.Email;
			usuarioDb.Perfil = usuario.Perfil;
			usuarioDb.DataDeAlteracao = DateTime.Now;

			_context.Usuarios.Update(usuarioDb);
			_context.SaveChanges();
			return usuarioDb;
		}
		
		public UsuarioModel AtualizarSenha(AlterarSenhaModel alterarSenhaModel)
		{
			UsuarioModel usuarioDb = BuscarPorId(alterarSenhaModel.Id);
			
			if(usuarioDb == null)
			{
				throw new Exception("Houve um erro na atualização da Senha, Usuário não encontrado!");
			}
			
			if(!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual!))
			{
				throw new Exception("Senha atual Não confere!");
			}
			
			if(usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha!))
			{
				throw new Exception("A nova senha deve ser diferente da atual!");
			}
			
			usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha!);	
			usuarioDb.DataDeAlteracao = DateTime.Now;
			
			_context.Usuarios.Update(usuarioDb);	
			_context.SaveChanges();					
			
			return usuarioDb;
		}

		public bool Apagar(Guid Id)
		{
			var usuarioDb = BuscarPorId(Id);
			if (usuarioDb == null)
			{
				throw new Exception("Erro na Exclusão do Usuário!!!");
			}

			_context.Usuarios.Remove(usuarioDb);
			_context.SaveChanges();
			return true;

		}

		public UsuarioModel BuscarPorLogin(string login)
		{
			
			var usuario = _context.Usuarios.FirstOrDefault(x => x.Login!.ToUpper() == login.ToUpper());

			return usuario!;
			
		}

		public UsuarioModel BuscarPorEmail_E_Login(string email, string login)
		{
			var usuario = _context.Usuarios
			.FirstOrDefault(x => 
				x.Email!.ToUpper() == email.ToUpper()
								&& 
				x.Login!.ToUpper() == login.ToUpper());

			return usuario!;
		}

   
	}
}
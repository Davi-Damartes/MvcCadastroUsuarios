using Microsoft.EntityFrameworkCore;
using MvcCadastro.Data.Map;
using MvcCadastro.Models;

namespace MvcCadastro.Data
{
	public class MeuBancoContext : DbContext
	{
		public MeuBancoContext(DbContextOptions options) : base(options)
		{ 
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ContatoMap());
			
			base.OnModelCreating(modelBuilder);
		}
		
		public DbSet<ContatoModel> Contatos { get; set; }
		public DbSet<UsuarioModel> Usuarios { get; set; }
	}
}
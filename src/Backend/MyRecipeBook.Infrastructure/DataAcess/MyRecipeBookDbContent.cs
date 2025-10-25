using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

/* Projeto: MyRecipeBook
 * Descrição: Classe de contexto do Entity Framework para o banco de dados do MyRecipeBook.
 * Autor: Lukas L. Moreira
 * Data: 2025-10-24
 */
namespace MyRecipeBook.Infrastructure.DataAcess
{
    public class MyRecipeBookDbContent : DbContext
    {
        public MyRecipeBookDbContent(DbContextOptions options) : base(options) { } // Construtor que aceita opções de DbContext

        public DbSet<User> Users { get; set; } // Definindo o DbSet para a entidade User

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContent).Assembly); // Aplicando as configurações de entidade automaticamente
        }
    }
}

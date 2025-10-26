using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcess;
using MyRecipeBook.Infrastructure.DataAcess.Repositories;

/* Projeto: MyRecipeBook
 * Descrição: Extensão para configurar as dependências da camada de infraestrutura.
 * Autor: Lukas L. Moreira
 * Data: 2025-10-26
 */
namespace MyRecipeBook.Infrastructure
{
    public static class DependecyInjectionExtension
    {
        // Função de extensão para adicionar serviços de infraestrutura ao contêiner de injeção de dependência
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddDbContext(services); // Adiciona o DbContext ao contêiner de serviços
            AddRepositories(services); // Adiciona os repositórios ao contêiner de serviços
        }

        // Configura o DbContext(Banco de dados) para o Entity Framework
        private static void AddDbContext(IServiceCollection services)
        {
            // Configurações do DbContext
            var connectionString = "Data Source=LUKAS;" +
                                   "Initial Catalog=meulivrodereceitas;" +
                                   "User ID=sa;" +
                                   "Password=Manaus@01.;" +
                                   "Trusted_Connection=True;" +
                                   "Encrypt=True;" +
                                   "TrustServerCertification=True;";

            // Configura o DbContext para usar o SQL Server
            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        // Registra os repositórios no contêiner de serviços
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}

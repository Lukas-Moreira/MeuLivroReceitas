using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
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
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var database = configuration.GetConnectionString("DatabaseType");

            if (string.IsNullOrEmpty(database))
            {
                throw new ArgumentNullException("DatabaseType", "DatabaseType não está configurado nas strings de conexão.");
            }

            var databaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), database);

            if (databaseType == DatabaseType.SqlServer)
                AddDbContext(services, configuration); // Adiciona o DbContext ao contêiner de serviços

            else
                throw new NotSupportedException($"O tipo de banco de dados '{databaseType}' não é suportado.");

            AddRepositories(services); // Adiciona os repositórios ao contêiner de serviços
        }

        // Configura o DbContext(Banco de dados) para o Entity Framework
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Configurações do DbContext
            var connectionString = configuration.GetConnectionString("ConnectionSQLServer"); // Obtém a string de conexão do SQL Server

            // Configura o DbContext para usar o SQL Server
            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        // Registra os repositórios no contêiner de serviços
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOnWork, UnitOnWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}

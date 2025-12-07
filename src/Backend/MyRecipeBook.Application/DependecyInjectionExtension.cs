using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependecyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //AddMappers(services); // Esperando retorno do professor sobre o uso do Mapster
            AddUseCases(services);
            AddPassworEncrypter(services);
        }

        private static void AddMappers(IServiceCollection services)
        {
            /* Configurar os mapeamentos (presume-se que esta chamada popula TypeAdapterConfig.GlobalSettings) */
            MapConfigurations.Configure();

            // Registrar o TypeAdapterConfig.GlobalSettings como singleton garante que as chamadas estáticas
            // (por exemplo: request.Adapt<T>()) usem a mesma configuração e evita problemas de configuração duplicada.
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);

            // Se você quiser suporte a IMapper via DI (por exemplo, para resolvers que dependem de serviços
            // injetados), pode registrar o ServiceMapper. Como você deseja permanecer com o uso estático,
            // mantenha esta linha comentada. Descomente se precisar:
            // services.AddScoped<IMapper, ServiceMapper>();
        }

        // Adiciona a interface IRegisterUserUseCase e retorna uma instância de RegisterUserUseCase ao contêiner de injeção de dependência
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPassworEncrypter(IServiceCollection services)
        {
            services.AddScoped(option => new PasswordEncrypter());
        }
    }
}

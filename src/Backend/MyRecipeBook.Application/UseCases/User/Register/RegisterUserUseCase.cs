using Mapster;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        /* Regra de negócio para registrar um usuário */
        public async Task<ResponseResgisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            /* Configurar os mapeamentos */
            MapConfigurations.Configure(); /* Ainda falta fazer a injeção de dependencias */

            /* Instanciar o encriptador de senhas */
            var passwordEncrypter = new PasswordEncrypter();

            /* Validar a request */
            ValidateRequest(request);

            /* Mapear a request para a entidade */
            var user = request.Adapt<Domain.Entities.User>();

            /* Criptografar a senha */
            user.Password = passwordEncrypter.Encrypt(request.Password);

            /* Salvar a entidade no banco de dados */
            await _userWriteOnlyRepository.Add(user);

            return new ResponseResgisteredUserJson
            {
                Name = request.Name
            };
        }

        /* Regra para validar a request */
        private void ValidateRequest(RequestRegisterUserJson request)
        {
            /* Implementar a validação da request */
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                /* Seleciona a mensagem de erro com o select */
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                /* Lança uma exceção com as mensagens de erro */
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}

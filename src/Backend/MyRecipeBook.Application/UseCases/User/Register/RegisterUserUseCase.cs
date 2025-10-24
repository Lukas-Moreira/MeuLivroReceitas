using Mapster;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        /* Regra de negócio para registrar um usuário */
        public ResponseResgisteredUserJson Execute(RequestRegisterUserJson request)
        {
            /* Validar a request */
            ValidateRequest(request);

            /* Mapear a request para a entidade */
            MapConfigurations.Configure(); /* Ainda falta fazer a injeção de dependencias */
            var user = request.Adapt<Domain.Entities.User>();

            /* Criptografar a senha */



            /* Salvar a entidade no banco de dados */

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

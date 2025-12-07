using Mapster;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly IUnitOnWork _unitOfWork;

        private readonly PasswordEncrypter _passwordEncrypter;

        // Construtor da classe que injeta as dependências necessárias
        public RegisterUserUseCase(
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOnWork unitOnWork,
            PasswordEncrypter passwordEncrypter)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOnWork;
            _passwordEncrypter = passwordEncrypter;
            MapConfigurations.Configure(); // Esperando retorno do professor sobre o uso do Mapster
        }

        /* Regra de negócio para registrar um usuário */
        public async Task<ResponseResgisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            /* Validar a request */
            ValidateRequest(request);

            /* Mapear a request para a entidade */
            var user = request.Adapt<Domain.Entities.User>();

            /* Criptografar a senha */
            user.Password = _passwordEncrypter.Encrypt(request.Password);

            /* Salvar a entidade no banco de dados */
            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();

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

using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    /* Classe que valida as propriedades Name, Email e Password */
    internal class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().
                WithMessage(ResourceMessagesExceptions.NAME_EMPTY);     /* Verifico se o campo nome não é vazio */

            RuleFor(user => user.Email).NotEmpty().
                WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);    /* Verifico se o campo email não é vazio */

            RuleFor(user => user.Email).EmailAddress().
                WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);  /* Verifico se o campo email é um tipo de e-mail */

            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).
                WithMessage(ResourceMessagesExceptions.PASSWORD_SHORT); /* Verifico se o campo senha tem no minimo 6 caracteres */
        }
    }
}

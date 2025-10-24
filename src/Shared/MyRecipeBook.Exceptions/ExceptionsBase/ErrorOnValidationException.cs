/*
 * ErrorOnValidationException é uma exceção personalizada que representa erros de validação.
 */
namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyRecipeBookException
    {
        public IList<string> ErrorMessages { get; private set; }

        public ErrorOnValidationException(IList<string> errosMessages) 
        {
            ErrorMessages = errosMessages;
        }
    }
}

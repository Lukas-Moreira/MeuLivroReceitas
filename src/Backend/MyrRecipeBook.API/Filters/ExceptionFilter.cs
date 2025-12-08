using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.Net;

namespace MyRecipeBook.API.Filters
{
    /* *
     * Filtro de exceção para capturar exceções lançadas durante o processamento das requisições HTTP
     * e retornar respostas HTTP apropriadas com mensagens de erro
     */
    public class ExceptionFilter : IExceptionFilter
    {
        /* *
         * Método chamado quando uma exceção é lançada durante o processamento de uma requisição HTTP
         */
        public void OnException(ExceptionContext context)
        {
            // Verifica se a exceção é do tipo MyRecipeBookException
            if (context.Exception is MyRecipeBookException)
                HandleProjectException(context); // Trata exceções conhecidas do projeto

            else
                ThrowUnknownException(context);  // Trata exceções desconhecidas
        }

        /* *
         * Trata exceções específicas do projeto MyRecipeBook
         */
        private void HandleProjectException(ExceptionContext context)
        {
            // Verifica se a exceção é do tipo ErrorOnValidationException
            if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException; // Cast da exceção para ErrorOnValidationException

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // Define o status code como 400 Bad Request
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages)); // Retorna as mensagens de erro na resposta
            }
        }

        private void ThrowUnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Define o status code como 500 Internal Server Error
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.UNKNOWN_ERROR)); // Retorna uma mensagem de erro genérica na resposta
        }
    }
}

using System.Globalization;

/*
 * Middleware para definir a cultura da aplicação com base no cabeçalho Accept-Language da requisição
 */
namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /* 
         * Invoke é o método chamado para cada requisição HTTP
         */
        public async Task Invoke(HttpContext context)
        {
            /*
             * - Primeiro obtem todas as culturas suportadas pela aplicação
             * - Obtém a cultura solicitada no cabeçalho Accept-Language
             * - Define a cultura padrão como "en" (inglês)
             * - Se a cultura solicitada for válida e suportada, define-a como a cultura atual
             * - Define a cultura atual e a cultura da interface do usuário
             * - Chama o próximo middleware na pipeline
             */
            var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (string.IsNullOrWhiteSpace(requestedCulture) == false 
                && supportedCultures.Any(c => c.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}

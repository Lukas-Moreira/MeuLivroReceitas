namespace MyRecipeBook.Communication.Requests
{
    public class RequestRegisterUserJson
    {
        public string Name { get; set; } = string.Empty;      /* Nome do usuário */
        public string Email { get; set; } = string.Empty;     /* Email do usuário */
        public string Password { get; set; } = string.Empty;  /* Senha do usuário */
    }
}

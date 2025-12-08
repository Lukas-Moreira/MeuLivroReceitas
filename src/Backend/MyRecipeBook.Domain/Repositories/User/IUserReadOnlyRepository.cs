/* Interface para operações de leitura relacionadas à entidade User.
 * Projeto: MyRecipeBook
 * Autor: Lukas L. Moreira
 * Data: 2025-10-24
 */
namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserByEmail(string email);
    }
}

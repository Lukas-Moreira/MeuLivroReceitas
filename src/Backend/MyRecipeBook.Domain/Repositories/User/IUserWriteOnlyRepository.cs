/* Interface para operações de escrita relacionadas à entidade User.
 * Projeto: MyRecipeBook
 * Autor: Lukas L. Moreira
 * Data: 2025-10-24
 */
namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entities.User user);
    }
}

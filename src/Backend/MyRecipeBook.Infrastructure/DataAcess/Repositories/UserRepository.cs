using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAcess;

/* Projeto: MyRecipeBook
 * Descrição: Repositório para operações relacionadas à entidade User.
 * Autor: Lukas L. Moreira
 * Data: 2025-10-24
 */
namespace MyRecipeBook.Infrastructure.DataAcess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        // Cria uma instância do contexto do banco de dados
        private readonly MyRecipeBookDbContext _context;

        // Construtor que recebe o contexto do banco de dados via injeção de dependência
        public UserRepository(MyRecipeBookDbContext context) => _context = context;

        // Adiciona um novo usuário ao banco de dados
        public async Task Add(User user) => await _context.Users.AddAsync(user);

        // Verifica se existe um usuário ativo com o email fornecido
        public async Task<bool> ExistActiveUserByEmail(string email) => await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    }
}

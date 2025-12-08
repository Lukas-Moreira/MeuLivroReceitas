using MyRecipeBook.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.DataAcess
{
    public class UnitOnWork : IUnitOnWork
    {
        // Cria uma instância do contexto do banco de dados
        private readonly MyRecipeBookDbContext _context;

        // Construtor que recebe o contexto do banco de dados via injeção de dependência
        public UnitOnWork(MyRecipeBookDbContext context) => _context = context;

        // Salva as alterações feitas no contexto do banco de dados
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}

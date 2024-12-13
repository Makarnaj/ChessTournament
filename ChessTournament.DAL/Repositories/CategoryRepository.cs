using ChessTournament.DAL.Database;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Category>> GetByIdsAsync(IEnumerable<int> categoryIds)
        {
            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }
    }
}

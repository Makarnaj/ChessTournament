
using ChessTournament.DAL.Database;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Repositories
{
    public class TournamentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Tournament> _dbSet;

        public TournamentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Tournament>();
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(Tournament tournament)
        {
            await _dbSet.AddAsync(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tournament tournament)
        {
            _dbSet.Update(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

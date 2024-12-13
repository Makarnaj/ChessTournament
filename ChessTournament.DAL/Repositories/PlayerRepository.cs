using ChessTournament.DAL.Database;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Player> _dbSet;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Player>();
        }

        public async Task<Player> CreateAsync(Player player)
        {
            await _context.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> UpdateAsync(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;

            }else
            {
                return false;
            }
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);                      
        }
        public async Task<Player?> GetByPseudoAsync(string pseudo)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Pseudo == pseudo);
        }
        public async Task<Player?> GetByEmailAsync(string email)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}


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
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ApplicationDbContext _context;


        public TournamentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments.ToListAsync();
        }

        public async Task AddAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<Tournament?> UpdateAsync(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Tournaments.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }else
            {
                return false;
            }
        }
        public async Task<Tournament?> CreateAsync(Tournament tournament)
        {
            if (tournament != null)
            {
                _context.Tournaments.Add(tournament);
                await _context.SaveChangesAsync();
                return tournament;
            }
            return null;
        }
        public async Task<Tournament?> GetTournamentDetailsAsync(int tournamentId)
        {
            // Récupérer le tournoi avec les joueurs
            return await _context.Tournaments.Include(t => t.PlayerTournaments)
                    .ThenInclude(pt => pt.Player)
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(t => t.Id == tournamentId);
        }
    }
}

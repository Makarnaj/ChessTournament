using ChessTournament.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Interfaces
{
    public interface ITournamentService
    {
        Task<Tournament?> GetByIdAsync(int id);
        Task<Tournament> CreateAsync(Tournament tournament);
        Task<Tournament?> UpdateAsync(Tournament tournament);
        Task<bool> DeleteAsync(int id);
        Task<List<Tournament>> GetTournamentsAsync();
        Task<Tournament?> GetTournamentDetailsAsync(int tournamentId);
    }
}

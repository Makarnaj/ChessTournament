using ChessTournament.DAL.Repositories.Interface;
using ChessTournament.Domain.Models;

namespace ChessTournament.DAL.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        public Task<Player> GetByPseudoAsync(string pseudo);
        public Task<Player> GetByEmailAsync(string email);
    }
}

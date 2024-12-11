using ChessTournament.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> CreateAsync(Player player);
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(int id);
    }
}

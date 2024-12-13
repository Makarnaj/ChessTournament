using ChessTournament.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<ICollection<Category>> GetByIdsAsync(IEnumerable<int> categoryIds);
    }
}

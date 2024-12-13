using ChessTournament.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<ICollection<Category>> GetByIdsAsync(ICollection<int> categoryId);
    }
}

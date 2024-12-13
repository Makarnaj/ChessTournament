using ChessTournament.BLL.Interfaces;
using ChessTournament.DAL.Repositories.Interfaces;
using ChessTournament.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTournament.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ICollection<Category>> GetByIdsAsync(ICollection<int> categoryId)
        {
            return await _categoryRepository.GetByIdsAsync(categoryId);

        }
    }
}

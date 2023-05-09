using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface ICategoryService
    {
        internal Task<ICollection<Category>> GetCategoriesByIds(ICollection<int> ids);
        public Task<ICollection<SimpleCategoryDTO>> GetSimpleCategories(GetSimpleCategoriesDTO requestDTO);

        public Task<ICollection<CategoryDTO>> GeetCategories(GetSimpleCategoriesDTO requestDTO);
        public Task Add(AddCategoryDTO categoryDTO);
        public Task Update(UpdateCategoryDTO categoryDTO);
    }
}

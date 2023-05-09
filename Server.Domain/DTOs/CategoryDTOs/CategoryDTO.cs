using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CategoryDTOs
{
    public class CategoryDTO:SimpleCategoryDTO
    {
        public int? ParentId { get; set; }
    }
}

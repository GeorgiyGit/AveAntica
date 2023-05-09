using Microsoft.AspNetCore.Http;
using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CategoryDTOs
{
    public class AddCategoryDTO
    {
        public ICollection<CategoryTranslatedText> Names { get; set; } = new List<CategoryTranslatedText>();
        public ICollection<CategoryTranslatedText> Descriptions { get; set; } = new List<CategoryTranslatedText>();

        public ICollection<int> TagIds { get; set; } = new List<int>();
        public int? ParentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.TagDTOs
{
    public class AddTagDTO
    {
        public ICollection<CategoryTranslatedText> Names { get; set; } = new List<CategoryTranslatedText>();
        public ICollection<CategoryTranslatedText> Descriptions { get; set; } = new List<CategoryTranslatedText>();
        public IFormFile? Image { get; set; }
    }
}

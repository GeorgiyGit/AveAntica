using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CategoryDTOs
{
    public class GetSimpleCategoriesDTO
    {
        public ICollection<int> TagIds { get; set; }
        public string LanguageCode { get; set; }
    }
}

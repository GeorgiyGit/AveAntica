using Microsoft.AspNetCore.Http;
using Server.Domain.DTOs.LanguageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        [DataMember(IsRequired = true)]
        public IList<TranslatedTextDTO> Names { get; set; }

        [DataMember(IsRequired = true)]
        public IList<TranslatedTextDTO> Descriptions { get; set; } = new List<TranslatedTextDTO>();
        public ICollection<int> CategoryIds { get; set; } = new List<int>();
        public ICollection<int> TagIds { get; set; } = new List<int>();
        public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
        public int Price { get; set; }
    }
}

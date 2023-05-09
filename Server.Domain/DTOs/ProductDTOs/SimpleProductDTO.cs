using Server.Domain.DTOs.ImageDTOs;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.ProductDTOs
{
    public class SimpleProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ImageDTO> Images { get; set; } = new List<ImageDTO>();
        public int Price { get; set; }
        public bool IsLiked { get; set; }
    }
}

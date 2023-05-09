using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.ProductDTOs
{
    public class UpdateProductDTO:AddProductDTO
    {
        public int Id { get; set; }
        public ICollection<int> ImageIds { get; set; } = new HashSet<int>();
    }
}

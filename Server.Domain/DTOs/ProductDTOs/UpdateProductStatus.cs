using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.ProductDTOs
{
    public class UpdateProductStatus
    {
        public int StatusId { get; set; }
        public int ProductId { get; set; }
    }
}

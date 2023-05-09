using Server.Domain.DTOs.PageDTOs;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface IProductService
    {
        public Task Add(AddProductDTO productDTO);
        public Task Update(UpdateProductDTO productDTO);

        public Task<ICollection<SimpleProductDTO>> GetSimpleList(ProductRequestDTO requestDTO);
        public Task UpdateStatus(UpdateProductStatus statusDTO);
    }
}

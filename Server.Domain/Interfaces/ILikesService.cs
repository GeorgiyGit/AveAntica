using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface ILikesService
    {
        public Task AddLike(int id);
        public Task RemoveLike(int id);
        public Task<ICollection<SimpleProductDTO>> AddIsLiked(List<Product> products, List<SimpleProductDTO> productDTOs);
    }
}

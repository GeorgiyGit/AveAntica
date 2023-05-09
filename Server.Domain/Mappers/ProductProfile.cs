using Server.Domain.DTOs.ImageDTOs;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Mappers
{
    internal class ProductProfile:AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, SimpleProductDTO>()
                           .ForMember(dest => dest.Images,
                                      opt => opt.Ignore())
                           .ForMember(dest => dest.Name,
                                      opt => opt.Ignore())
                           .ForMember(dest => dest.IsLiked,
                                      opt => opt.Ignore());
        }
    }
}

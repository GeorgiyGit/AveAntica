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
    internal class ImageProfile : AutoMapper.Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDTO>();
        }
    }
}

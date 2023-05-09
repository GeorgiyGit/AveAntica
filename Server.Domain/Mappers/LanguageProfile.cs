using Server.Domain.DTOs.ImageDTOs;
using Server.Domain.DTOs.LanguageDTOs;
using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Mappers
{
    internal class LanguageProfile : AutoMapper.Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageDTO>();
        }
    }
}

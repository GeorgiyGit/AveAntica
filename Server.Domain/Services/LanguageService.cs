using AutoMapper;
using Server.Domain.DTOs.LanguageDTOs;
using Server.Domain.Entities.Language;
using Server.Domain.Exceptions;
using Server.Domain.Interfaces;
using Server.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> repository;
        private readonly IMapper mapper;
        
        public LanguageService(IRepository<Language> repository,
                               IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ICollection<LanguageDTO>> GetAll()
        {
            return mapper.Map<ICollection<LanguageDTO>>(await repository.GetAllAsync());
        }

        public async Task<Language> GetOriginalById(string code)
        {
            var language = await repository.FindAsync(code);

            if (language == null) throw new HttpException(ErrorMessages.LanguageNotFound, HttpStatusCode.BadRequest);

            return language;
        }
    }
}

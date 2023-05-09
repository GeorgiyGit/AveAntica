using Server.Domain.DTOs.LanguageDTOs;
using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface ILanguageService
    {
        internal Task<Language> GetOriginalById(string code);
        public Task<ICollection<LanguageDTO>> GetAll();
    }
}

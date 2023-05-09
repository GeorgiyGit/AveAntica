using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.DTOs.TagDTOs;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface ITagService
    {
        internal Task<Tag> GetOriginalById(int id);
        internal Task<ICollection<Tag>> GetOriginalsById(ICollection<int> ids);
        public Task<ICollection<SimpleTagDTO>> GetSimpleTags(string languageCode);
        public Task Add(AddTagDTO tagDTO);
        public Task Update(UpdateTagDTO tagDTO);
    }
}

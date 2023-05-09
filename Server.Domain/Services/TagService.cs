using Server.Domain.DTOs.CategoryDTOs;
using Server.Domain.DTOs.TagDTOs;
using Server.Domain.Entities;
using Server.Domain.Entities.Language;
using Server.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> repository;
        private readonly ILanguageService languageService;
        private readonly IImageService imageService;

        public TagService(IRepository<Tag> repository,
                          ILanguageService languageService,
                          IImageService imageService)
        {
            this.repository = repository;
            this.languageService = languageService;
            this.imageService = imageService;
        }


        public async Task Add(AddTagDTO tagDTO)
        {
            var tag= new Tag();

            foreach (var name in tagDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new TagTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = tag,
                    LanguageCode = language.Code
                };

                tag.Names.Add(translatedText);
            }

            foreach (var description in tagDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new TagTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = tag,
                    LanguageCode = language.Code
                };

                tag.Descriptions.Add(translatedText);
            }

            if (tagDTO.Image != null)
            {
                var image = await imageService.SaveUserAvatar(tagDTO.Image);

                image.Tag = tag;
                tag.Image = image;

                await imageService.SaveImageToDatabase(image);
            }

            await repository.AddAsync(tag);
            await repository.SaveChangesAsync();
        }

        public async Task Update(UpdateTagDTO tagDTO)
        {
            var tag= (await repository.GetAsync(c => c.Id == tagDTO.Id, includeProperties: $"{nameof(Category.Names)},{nameof(Category.Descriptions)},{nameof(Category.Parent)}")).First();

            foreach (var name in tagDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new TagTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = tag,
                    LanguageCode = language.Code
                };

                tag.Names.Add(translatedText);
            }

            foreach (var description in tagDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new TagTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = tag,
                    LanguageCode = language.Code
                };

                tag.Descriptions.Add(translatedText);
            }


            if (tagDTO.IsImageChanged && tagDTO.Image != null)
            {
                await imageService.RemoveUserAvatar(tag.Image.Id);

                var image = await imageService.SaveUserAvatar(tagDTO.Image);

                tag.Image.Name = image.Name;
                tag.Image.Title = image.Title;

                tag.Image.IsEdited = true;
                tag.Image.LastEditTime = DateTime.UtcNow;

                await imageService.UpdateImageInDatabase(tag.Image);
            }

            tag.IsEdited = true;
            tag.LastEditTime = DateTime.UtcNow;

            repository.Update(tag);
            await repository.SaveChangesAsync();
        }


        public async Task<ICollection<SimpleTagDTO>> GetSimpleTags(string languageCode)
        {
            var tags = (await repository.GetAsync(includeProperties: $"{nameof(Tag.Names)}")).ToList();

            List<SimpleTagDTO> mappedTags = new List<SimpleTagDTO>();

            for (int i = 0; i < tags.Count; i++)
            {
                var tag = tags[i];

                string name = tag.Names.Where(n => n.LanguageCode == languageCode).Select(n => n.Value).First();

                mappedTags.Add(new SimpleTagDTO()
                {
                    Id = tag.Id,
                    Name = name,
                });
            }

            return mappedTags;
        }

        public async Task<Tag> GetOriginalById(int id)
        {
            return (await this.repository.GetAsync(t => t.Id == id, includeProperties: $"{nameof(Tag.Categories)}")).First();
        }
        public async Task<ICollection<Tag>> GetOriginalsById(ICollection<int> ids)
        {
            ICollection<Tag> tags = new List<Tag>();
            
            foreach (var id in ids)
            {
                tags.Add((await this.repository.GetAsync(t => t.Id == id, includeProperties: $"{nameof(Tag.Products)}")).First());
            }

            return tags;
        }
    }
}

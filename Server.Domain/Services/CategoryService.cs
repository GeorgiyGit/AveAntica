using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Server.Domain.DTOs.CategoryDTOs;
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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;
        private readonly IImageService imageService;
        private readonly ILanguageService languageService;
        private readonly ITagService tagService;
        private readonly IMapper mapper;
        public CategoryService(IRepository<Category> repository,
                               IImageService imageService,
                               ILanguageService languageService,
                               IMapper mapper,
                               ITagService tagService)
        {
            this.repository = repository;
            this.imageService = imageService;
            this.languageService = languageService;
            this.mapper = mapper;
            this.tagService = tagService;
        }

        public async Task Add(AddCategoryDTO categoryDTO)
        {
            var category = new Category();

            foreach (var name in categoryDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new CategoryTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = category,
                    LanguageCode = language.Code
                };

                category.Names.Add(translatedText);
            }

            foreach (var description in categoryDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new CategoryTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = category,
                    LanguageCode = language.Code
                };

                category.Descriptions.Add(translatedText);
            }

            foreach (var id in categoryDTO.TagIds)
            {
                var tag = await tagService.GetOriginalById(id);

                if (tag != null)
                {
                    tag.Categories.Add(category);
                    category.Tags.Add(tag);
                }
            }


            if (categoryDTO.ParentId != null)
            {
                var parent = await repository.FindAsync(categoryDTO.ParentId);

                if (parent != null)
                {
                    category.ParentId = (int)categoryDTO.ParentId;
                    category.Parent = parent;
                }
            }

            if (categoryDTO.Image != null)
            {
                var image = await imageService.SaveUserAvatar(categoryDTO.Image);

                image.Category = category;
                category.Image = image;

                await imageService.SaveImageToDatabase(image);
            }

            await repository.AddAsync(category);
            await repository.SaveChangesAsync();
        }

        public async Task Update(UpdateCategoryDTO categoryDTO)
        {
            var category = (await repository.GetAsync(c => c.Id == categoryDTO.Id, includeProperties: $"{nameof(Category.Names)},{nameof(Category.Descriptions)},{nameof(Category.Parent)}")).First();

            category.Names=new List<CategoryTranslatedText>();
            category.Descriptions = new List<CategoryTranslatedText>();
            category.Tags = new List<Tag>();

            foreach (var name in categoryDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new CategoryTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = category,
                    LanguageCode = language.Code
                };

                category.Names.Add(translatedText);
            }

            foreach (var description in categoryDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new CategoryTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = category,
                    LanguageCode = language.Code
                };

                category.Descriptions.Add(translatedText);
            }

            foreach (var id in categoryDTO.TagIds)
            {
                var tag = await tagService.GetOriginalById(id);



                if (tag != null)
                {
                    tag.Categories.Remove(category);
                    category.Tags.Add(tag);
                }
            }

            if (categoryDTO.ParentId != null)
            {
                var parent = await repository.FindAsync(categoryDTO.ParentId);

                if (parent != null)
                {
                    category.ParentId = (int)categoryDTO.ParentId;
                    category.Parent = parent;
                }
            }
            else
            {
                if (category.Parent != null)
                {
                    category.Parent = null;
                    category.ParentId = null;
                }
            }

            if (categoryDTO.IsImageChanged && categoryDTO.Image!=null)
            {
                await imageService.RemoveUserAvatar(category.Image.Id);
                
                var image = await imageService.SaveUserAvatar(categoryDTO.Image);

                category.Image.Name = image.Name;
                category.Image.Title = image.Title;

                category.Image.IsEdited = true;
                category.Image.LastEditTime = DateTime.UtcNow;

                await imageService.UpdateImageInDatabase(category.Image);
            }

            category.IsEdited = true;
            category.LastEditTime = DateTime.UtcNow;

            repository.Update(category);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<Category>> GetCategoriesByIds(ICollection<int> ids)
        {
            List<Category> categories = new List<Category>();

            foreach(var id in ids)
            {
                var category = (await repository.GetAsync(c=>c.Id==id,includeProperties:$"{nameof(Category.Products)}")).First();

                if(category!= null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }

        public async Task<ICollection<SimpleCategoryDTO>> GetSimpleCategories(GetSimpleCategoriesDTO requestDTO)
        {
            var categories = (await repository.GetAsync(includeProperties: $"{nameof(Category.Names)},{nameof(Category.Tags)}")).ToList();
        
            ICollection<SimpleCategoryDTO> mappedCategories = new List<SimpleCategoryDTO>();

            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];

                var tagIds = category.Tags.Select(t => t.Id).ToList();

                bool flag = false;

                foreach (var id in requestDTO.TagIds)
                {
                    if (!tagIds.Contains(id))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) continue;

                string name = category.Names.Where(n => n.LanguageCode == requestDTO.LanguageCode).Select(n => n.Value).First();

                mappedCategories.Add(new SimpleCategoryDTO()
                {
                    Id = category.Id,
                    Name = name,
                });
            }

            return mappedCategories;
        }

        public async Task<ICollection<CategoryDTO>> GeetCategories(GetSimpleCategoriesDTO requestDTO)
        {
            var categories = (await repository.GetAsync(includeProperties: $"{nameof(Category.Names)},{nameof(Category.Tags)}")).ToList();

            ICollection<CategoryDTO> mappedCategories = new List<CategoryDTO>();

            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];

                var tagIds = category.Tags.Select(t => t.Id).ToList();

                bool flag = false;

                foreach (var id in requestDTO.TagIds)
                {
                    if (!tagIds.Contains(id))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) continue;

                string name = category.Names.Where(n => n.LanguageCode == requestDTO.LanguageCode).Select(n => n.Value).First();

                mappedCategories.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name = name,
                    ParentId= category.ParentId
                });
            }

            return mappedCategories;
        }
    }
}

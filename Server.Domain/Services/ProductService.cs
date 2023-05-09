using AutoMapper;
using Server.Domain.DTOs.ImageDTOs;
using Server.Domain.DTOs.PageDTOs;
using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.DTOs.RequestDTOs;
using Server.Domain.Entities;
using Server.Domain.Entities.Language;
using Server.Domain.Exceptions;
using Server.Domain.Interfaces;
using Server.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;
        private readonly IRepository<ProductType> productTypeRepository;
        private readonly IImageService imageService;
        private readonly ILanguageService languageService;
        private readonly ICategoryService categoryService;
        private readonly ITagService tagService;
        private readonly IMapper mapper;
        private readonly ILikesService likesService;
        public ProductService(IRepository<Product> repository,
                              IImageService imageService,
                              ILanguageService languageService,
                              ICategoryService categoryService,
                              IMapper mapper,
                              IRepository<ProductType> productTypeRepository,
                              ITagService tagService,
                              ILikesService likesService)
        {
            this.repository = repository;
            this.imageService = imageService;
            this.languageService = languageService;
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.productTypeRepository = productTypeRepository;
            this.tagService = tagService;
            this.likesService = likesService;
        }

        public async Task Add(AddProductDTO productDTO)
        {
            var product = new Product();

            product.Price=productDTO.Price;

            foreach (var name in productDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new ProductTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = product,
                    LanguageCode = language.Code
                };

                product.Names.Add(translatedText);
            }

            foreach (var description in productDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new ProductTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = product,
                    LanguageCode = language.Code
                };

                product.Descriptions.Add(translatedText);
            }

            var categories = await categoryService.GetCategoriesByIds(productDTO.CategoryIds);

            foreach (var category in categories)
            {
                category.Products.Add(product);
                product.Categories.Add(category);
            }

            var tags = await tagService.GetOriginalsById(productDTO.TagIds);
            foreach (var tag in tags)
            {
                tag.Products.Add(product);
                product.Tags.Add(tag);
            }


            var images = await imageService.SaveImages(productDTO.Images.ToList());

            foreach (var image in images)
            {
                image.Product = product;
                product.Images.Add(image);

                await imageService.SaveImageToDatabase(image);
            }

            var productStatus = (await productTypeRepository.GetAsync(p => p.Id == 2, includeProperties: $"{nameof(ProductType.Products)}")).First();

            if (productStatus != null)
            {
                product.Status = productStatus;
                product.StatusId = productStatus.Id;

                productStatus.Products.Add(product);
            }

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
        }

        public async Task Update(UpdateProductDTO productDTO)
        {
            var product = (await repository.GetAsync(p => p.Id == productDTO.Id, includeProperties: $"{nameof(Product.Images)},{nameof(Product.Names)},{nameof(Product.Descriptions)},{nameof(Product.Categories)},{nameof(Product.Tags)}")).First();

            product.Price = productDTO.Price;

            product.Names = new HashSet<ProductTranslatedText>();
            product.Descriptions = new HashSet<ProductTranslatedText>();
            product.Categories = new HashSet<Category>();

            foreach (var name in productDTO.Names)
            {
                var language = await languageService.GetOriginalById(name.LanguageCode);

                var translatedText = new ProductTranslatedText()
                {
                    Value = name.Value,
                    NameEntity = product,
                    LanguageCode = language.Code
                };

                product.Names.Add(translatedText);
            }

            foreach (var description in productDTO.Descriptions)
            {
                var language = await languageService.GetOriginalById(description.LanguageCode);

                var translatedText = new ProductTranslatedText()
                {
                    Value = description.Value,
                    DescriptionEntity = product,
                    LanguageCode = language.Code
                };

                product.Descriptions.Add(translatedText);
            }

            var categories = await categoryService.GetCategoriesByIds(productDTO.CategoryIds);

            foreach (var category in categories)
            {
                category.Products.Add(product);
                product.Categories.Add(category);
            }

            var tags = await tagService.GetOriginalsById(productDTO.CategoryIds);
            foreach (var tag in tags)
            {
                tag.Products.Add(product);
                product.Tags.Add(tag);
            }

            List<Image> imageForDelete = new List<Image>();

            foreach(var image in product.Images)
            {
                if (!productDTO.ImageIds.Contains(image.Id))
                {
                    imageForDelete.Add(image);
                }
            }

            foreach(var image in imageForDelete)
            {
                product.Images.Remove(image);

                await imageService.RemoveImageFile(image.Id);
                await imageService.DeleteImageFromDatabase(image.Id);
            }


            var images = await imageService.SaveImages(productDTO.Images.ToList());

            foreach (var image in images)
            {
                image.Product = product;
                product.Images.Add(image);

                await imageService.SaveImageToDatabase(image);
            }

            product.IsEdited = true;
            product.LastEditTime = DateTime.Now;

            repository.Update(product);
            await repository.SaveChangesAsync();
        }

        public async Task<ICollection<SimpleProductDTO>> GetSimpleList(ProductRequestDTO requestDTO)
        {
            ICollection<SimpleProductDTO> result = new List<SimpleProductDTO>();
            List<Product> products = new List<Product>();

            if (requestDTO.FilterType == FilterTypes.ByTimeDesc)
            {
                products = (await repository.GetAsync(includeProperties: $"{nameof(Product.Images)},{nameof(Product.Names)},{nameof(Product.Categories)},{nameof(Product.Tags)},{nameof(Product.LikedUsers)}", orderBy: p => p.OrderByDescending(p => p.CreationTime))).ToList();
                result = await MapProducts(products, requestDTO);
            }
            else if (requestDTO.FilterType == FilterTypes.ByTimeInc)
            {
                products = (await repository.GetAsync(includeProperties: $"{nameof(Product.Images)},{nameof(Product.Names)},{nameof(Product.Categories)},{nameof(Product.Tags)},{nameof(Product.LikedUsers)}", orderBy: p => p.OrderBy(p => p.CreationTime))).ToList();
                result = await MapProducts(products, requestDTO);
            }
            else if (requestDTO.FilterType == FilterTypes.ByPriceDesc)
            {
                products = (await repository.GetAsync(includeProperties: $"{nameof(Product.Images)},{nameof(Product.Names)},{nameof(Product.Categories)},{nameof(Product.Tags)},{nameof(Product.LikedUsers)}", orderBy: p => p.OrderByDescending(p => p.Price))).ToList();
                result = await MapProducts(products, requestDTO);
            }
            else if (requestDTO.FilterType == FilterTypes.ByPriceInc)
            {
                products = (await repository.GetAsync(includeProperties: $"{nameof(Product.Images)},{nameof(Product.Names)},{nameof(Product.Categories)},{nameof(Product.Tags)},{nameof(Product.LikedUsers)}", orderBy: p => p.OrderBy(p => p.Price))).ToList();
                result = await MapProducts(products, requestDTO);
            }
            else
            {
                throw new HttpException(ErrorMessages.ProductBadRequest,HttpStatusCode.BadRequest);
            }

            result = await this.likesService.AddIsLiked(products, result.ToList());

            return result;
        }
        private async Task<ICollection<SimpleProductDTO>> MapProducts(List<Product> products, ProductRequestDTO requestDTO)
        {
            var mappedProducts = new List<SimpleProductDTO>();

            for (var i = 0; i < products.Count; i++)
            {
                var product = products[i];

                string? name = product.Names.Select(n => n).Where(c => c.LanguageCode == requestDTO.LanguageCode).FirstOrDefault()?.Value;

                if (name != null &&
                   requestDTO.FilterStr != "" &&
                   !name.Contains(requestDTO.FilterStr)) continue;

                var categoryIds = product.Categories.Select(c => c.Id).ToList();

                bool flag = false;
                foreach (var category in requestDTO.CategoryIds)
                {
                    if (!categoryIds.Contains(category))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) continue;


                var tagIds = product.Tags.Select(c => c.Id).ToList();

                flag = false;
                foreach (var tag in requestDTO.TagIds)
                {
                    if (!tagIds.Contains(tag))
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) continue;

                var mappedProduct = mapper.Map<SimpleProductDTO>(product);

                if (name == null)
                {
                    mappedProduct.Name = product.Names.First().Value;
                }
                else
                {
                    mappedProduct.Name = name;
                }

                List<Image> images = product.Images.ToList();
                if (images.Count > 0)
                {
                    mappedProduct.Images.Add(mapper.Map<ImageDTO>(images[0]));

                    if (images.Count > 1) mappedProduct.Images.Add(mapper.Map<ImageDTO>(images[1]));
                }

                mappedProducts.Add(mappedProduct);
            }
            
            var pageParameters = requestDTO.PageParameters;

            return mappedProducts.Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                                                            .Take(pageParameters.PageSize).ToList();
        }

        public async Task UpdateStatus(UpdateProductStatus statusDTO)
        {
            var status = (await productTypeRepository.GetAsync(s => s.Id == statusDTO.StatusId, includeProperties: $"{nameof(ProductType.Products)}")).First();

            if (status == null) throw new HttpException(ErrorMessages.StatusNotFound, HttpStatusCode.BadRequest);

            var product = (await repository.GetAsync(p => p.Id == statusDTO.ProductId, includeProperties: $"{nameof(Product.Status)}")).First();

            if (product == null) throw new HttpException(ErrorMessages.ProductBadId, HttpStatusCode.BadRequest);

            var oldType = (await productTypeRepository.GetAsync(s => s.Id == product.StatusId, includeProperties: $"{nameof(ProductType.Products)}")).First();

            if (oldType != null)
            {
                oldType.Products.Remove(product);
            }

            product.Status = status;
            product.StatusId = status.Id;

            status.Products.Add(product);

            productTypeRepository.Update(status);
            repository.Update(product);

            await repository.SaveChangesAsync();
        }
    }
}
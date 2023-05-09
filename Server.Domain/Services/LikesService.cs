using Server.Domain.DTOs.ProductDTOs;
using Server.Domain.Entities;
using Server.Domain.Exceptions;
using Server.Domain.Interfaces;
using Server.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Services
{
    public class LikesService : ILikesService
    {
        private readonly IRepository<Product> product_repository;
        private readonly IRepository<Customer> customer_repository;
        private readonly IUserService userService;
        public LikesService(IRepository<Product> product_repository,
                            IRepository<Customer> customer_repository,
                            IUserService userService)
        {
            this.product_repository = product_repository;
            this.customer_repository = customer_repository;
            this.userService = userService;
        }

        public async Task<ICollection<SimpleProductDTO>> AddIsLiked(List<Product> products, List<SimpleProductDTO> productDTOs)
        {
            try
            {
                var userId = userService.GetCurrentUserId();

                if (userId == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

                var user = (await customer_repository.GetAsync(c => c.Id == userId, includeProperties: $"{nameof(Customer.LikedProducts)}")).FirstOrDefault();

                if (user == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

                for (int i = 0; i < products.Count; i++)
                {
                    if (user.LikedProducts.Contains(products[i]))
                    {
                        productDTOs[i].IsLiked = true;
                    }
                }
                return productDTOs;
            }
            catch (Exception ex)
            {
                return productDTOs;
            }
        }

        public async Task AddLike(int id)
        {
            var userId = userService.GetCurrentUserId();

            if (userId == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

            var user = (await customer_repository.GetAsync(c => c.Id == userId, includeProperties: $"{nameof(Customer.LikedProducts)}")).FirstOrDefault();

            if (user == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

            var product = (await product_repository.GetAsync(p => p.Id == id, includeProperties: $"{nameof(Product.LikedUsers)}")).FirstOrDefault();

            if (product == null) throw new HttpException(ErrorMessages.ProductBadId, HttpStatusCode.NotFound);


            product.LikedUsers.Add(user);
            user.LikedProducts.Add(product);

            product_repository.Update(product);
            customer_repository.Update(user);

            await product_repository.SaveChangesAsync();
        }

        public async Task RemoveLike(int id)
        {
            var userId = userService.GetCurrentUserId();

            if (userId == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

            var user = (await customer_repository.GetAsync(c => c.Id == userId, includeProperties: $"{nameof(Customer.LikedProducts)}")).FirstOrDefault();

            if (user == null) throw new HttpException(ErrorMessages.UserNotAuthorized, HttpStatusCode.NotFound);

            var product = (await product_repository.GetAsync(p => p.Id == id, includeProperties: $"{nameof(Product.LikedUsers)}")).FirstOrDefault();

            if (product == null) throw new HttpException(ErrorMessages.ProductBadId, HttpStatusCode.NotFound);


            product.LikedUsers.Remove(user);
            user.LikedProducts.Remove(product);

            product_repository.Update(product);
            customer_repository.Update(user);

            await product_repository.SaveChangesAsync();
        }
    }
}

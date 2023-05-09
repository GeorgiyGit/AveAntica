using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Server.Domain.Entities;
using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure
{
    public class AveAnticaDbContext : IdentityDbContext<Customer>
    {
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<GlobalChat> GlobalChats { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LocalChat> LocalChats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<OrderTypeTranslatedText> OrderTypeTranslatedTexts { get; set; }
        public virtual DbSet<ProductTranslatedText> ProductTranslatedTexts { get; set; }
        public virtual DbSet<ProductTypeTranslatedText> ProductTypeTranslatedTexts { get; set; }
        public virtual DbSet<CategoryTranslatedText> CategoryTranslatedTexts { get; set; }
        public AveAnticaDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Basket>().HasOne(e => e.Customer)
                                         .WithOne(e => e.Basket)
                                         .HasForeignKey<Basket>(e => e.CustomerId);

            modelBuilder.Entity<Basket>().HasMany(e => e.Products)
                                         .WithMany(e => e.Baskets);

            modelBuilder.Entity<Category>().HasOne(e => e.Parent)
                                         .WithMany(e => e.Children)
                                         .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Category>().HasOne(e => e.Image)
                                         .WithOne(e => e.Category)
                                         .HasForeignKey<Image>(e => e.CategoryId);

            modelBuilder.Entity<Category>().HasMany(e => e.Products)
                                         .WithMany(e => e.Categories);

            modelBuilder.Entity<Customer>().HasOne(e => e.Avatar)
                                         .WithOne(e => e.Customer)
                                         .HasForeignKey<Image>(e => e.CustomerId)
                                         .IsRequired(false);

            modelBuilder.Entity<Customer>().HasOne(e => e.Basket)
                                         .WithOne(e => e.Customer)
                                         .HasForeignKey<Basket>(e => e.CustomerId);

            modelBuilder.Entity<Customer>().HasOne(e => e.GlobalChat)
                                         .WithOne(e => e.Customer)
                                         .HasForeignKey<GlobalChat>(e => e.CustomerId);

            modelBuilder.Entity<Customer>().HasMany(e => e.Messages)
                                        .WithOne(e => e.Owner)
                                        .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<Customer>().HasMany(e => e.Orders)
                                       .WithOne(e => e.Customer)
                                       .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Discount>().HasMany(e => e.Orders)
                                           .WithMany(e => e.Discounts);


            modelBuilder.Entity<GlobalChat>().HasMany(e => e.Messages)
                                             .WithOne(e => e.GlobalChat)
                                             .HasForeignKey(e => e.GlobalChatId);

            modelBuilder.Entity<LocalChat>().HasMany(e => e.Messages)
                                             .WithOne(e => e.LocalChat)
                                             .HasForeignKey(e => e.LocalChatId);

            modelBuilder.Entity<Language>().HasKey(e => e.Code);

            modelBuilder.Entity<Order>().HasOne(e => e.Status)
                                             .WithMany(e => e.Orders)
                                             .HasForeignKey(e => e.StatusId);


            modelBuilder.Entity<Product>().HasOne(e => e.Status)
                                             .WithMany(e => e.Products)
                                             .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Product>().HasMany(e => e.Images)
                                             .WithOne(e => e.Product)
                                             .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Product>().HasMany(e => e.Orders)
                                             .WithMany(e => e.Products);



            modelBuilder.Entity<Product>().HasMany(e => e.Names)
                                            .WithOne(e => e.NameEntity)
                                            .HasForeignKey(e => e.NameEntityId)
                                            .IsRequired(false);

            modelBuilder.Entity<Product>().HasMany(e => e.Descriptions)
                                .WithOne(e => e.DescriptionEntity)
                                .HasForeignKey(e => e.DescriptionEntityId)
                                .IsRequired(false);


            modelBuilder.Entity<Category>().HasMany(e => e.Names)
                                            .WithOne(e => e.NameEntity)
                                            .HasForeignKey(e => e.NameEntityId)
                                            .IsRequired(false);

            modelBuilder.Entity<Category>().HasMany(e => e.Descriptions)
                                .WithOne(e => e.DescriptionEntity)
                                .HasForeignKey(e => e.DescriptionEntityId)
                                .IsRequired(false);


            modelBuilder.Entity<ProductType>().HasMany(e => e.Names)
                                            .WithOne(e => e.NameEntity)
                                            .HasForeignKey(e => e.NameEntityId)
                                            .IsRequired(false);

            modelBuilder.Entity<ProductType>().HasMany(e => e.Descriptions)
                                .WithOne(e => e.DescriptionEntity)
                                .HasForeignKey(e => e.DescriptionEntityId)
                                .IsRequired(false);


            modelBuilder.Entity<OrderType>().HasMany(e => e.Names)
                                            .WithOne(e => e.NameEntity)
                                            .HasForeignKey(e => e.NameEntityId)
                                            .IsRequired(false);

            modelBuilder.Entity<OrderType>().HasMany(e => e.Descriptions)
                                .WithOne(e => e.DescriptionEntity)
                                .HasForeignKey(e => e.DescriptionEntityId)
                                .IsRequired(false);

            modelBuilder.Entity<OrderType>().HasMany(e => e.Descriptions)
                               .WithOne(e => e.DescriptionEntity)
                               .HasForeignKey(e => e.DescriptionEntityId)
                               .IsRequired(false);



            modelBuilder.Entity<Tag>().HasMany(e => e.Descriptions)
                                .WithOne(e => e.DescriptionEntity)
                                .HasForeignKey(e => e.DescriptionEntityId)
                                .IsRequired(false);

            modelBuilder.Entity<Tag>().HasMany(e => e.Descriptions)
                               .WithOne(e => e.DescriptionEntity)
                               .HasForeignKey(e => e.DescriptionEntityId)
                               .IsRequired(false);


            modelBuilder.Entity<Tag>().HasMany(e => e.Categories)
                                      .WithMany(c => c.Tags);

            modelBuilder.Entity<Tag>().HasMany(e => e.Products)
                                      .WithMany(c => c.Tags);


            modelBuilder.Entity<Tag>().HasOne(e => e.Image)
                                         .WithOne(e => e.Tag)
                                         .HasForeignKey<Image>(e => e.TagId);


            modelBuilder.Entity<Product>().HasMany(e => e.LikedUsers)
                                      .WithMany(c => c.LikedProducts);


            Seeder(modelBuilder);
        }

        private void Seeder(ModelBuilder modelBuilder)
        {
            Language ukr = new Language()
            {
                Name = "Ukrainian",
                Code = "ukr"
            };

            Language rus = new Language()
            {
                Name = "Russian",
                Code = "rus"
            };

            ProductTypeTranslatedText nameUkr = new ProductTypeTranslatedText()
            {
                Value = "Продається",
                LanguageCode = ukr.Code,
                Id = 1
            };

            ProductTypeTranslatedText descriptionUkr = new ProductTypeTranslatedText()
            {
                Value = "Товар продається",
                LanguageCode = ukr.Code,
                Id = 2
            };


            ProductTypeTranslatedText nameRus = new ProductTypeTranslatedText()
            {
                Value = "Продаеться",
                LanguageCode = rus.Code,
                Id = 3
            };

            ProductTypeTranslatedText descriptionRus = new ProductTypeTranslatedText()
            {
                Value = "Товар продаеться",
                LanguageCode = rus.Code,
                Id = 4
            };


            ProductType productType = new ProductType()
            {
                Id = 2
            };


            nameUkr.NameEntityId = productType.Id;
            nameRus.NameEntityId = productType.Id;

            descriptionUkr.DescriptionEntityId = productType.Id;
            descriptionRus.DescriptionEntityId = productType.Id;

            nameUkr.DescriptionEntityId = null;
            nameRus.DescriptionEntityId = null;

            descriptionUkr.NameEntityId = null;
            descriptionRus.NameEntityId = null;

            modelBuilder.Entity<Language>().HasData(
                ukr,
                rus
            );
            modelBuilder.Entity<ProductTypeTranslatedText>().HasData(
                nameRus,
                descriptionRus,
                nameUkr,
                descriptionUkr
            );
            modelBuilder.Entity<ProductType>().HasData(
                productType
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain.Entities.Language;

namespace Server.Domain.Entities
{
    public class Product : Monitoring
    {
        public int Id { get; set; }
        public ICollection<ProductTranslatedText> Names { get; set; } = new HashSet<ProductTranslatedText>();
        public ICollection<ProductTranslatedText> Descriptions { get; set; } = new HashSet<ProductTranslatedText>();

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public ICollection<Basket> Baskets { get; set; } = new HashSet<Basket>();

        public ICollection<LocalChat> LocalChats { get; set; } = new HashSet<LocalChat>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();


        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public ProductType Status { get; set; }
        public int StatusId { get; set; }
        public int Price { get; set; }

        public ICollection<Customer> LikedUsers { get; set; } = new HashSet<Customer>();
    }
}

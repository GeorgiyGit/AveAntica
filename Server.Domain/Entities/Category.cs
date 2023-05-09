using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain.Entities.Language;

namespace Server.Domain.Entities
{
    public class Category:Monitoring
    {
        public int Id { get; set; }
        public ICollection<CategoryTranslatedText> Names { get; set; } = new HashSet<CategoryTranslatedText>();
        public ICollection<CategoryTranslatedText> Descriptions { get; set; } = new HashSet<CategoryTranslatedText>();


        public Category? Parent { get; set; }
        public int? ParentId { get; set; }

        public Image Image { get; set; }

        public ICollection<Category> Children { get; set; } = new HashSet<Category>();

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}

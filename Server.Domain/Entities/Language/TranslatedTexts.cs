using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities.Language
{
    public class OrderTypeTranslatedText : TranslatedText<OrderType, int?> { }
    public class CategoryTranslatedText : TranslatedText<Category, int?> { }
    public class ProductTranslatedText : TranslatedText<Product, int?> { }
    public class ProductTypeTranslatedText : TranslatedText<ProductType, int?> { }
    public class TagTranslatedText : TranslatedText<Tag, int?> { }

    public abstract class TranslatedText<Entity, EntityId>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string LanguageCode { get; set; }

        public Entity? NameEntity { get; set; }
        public EntityId NameEntityId { get; set; }

        public Entity? DescriptionEntity { get; set; }
        public EntityId DescriptionEntityId { get; set; }
    }
}

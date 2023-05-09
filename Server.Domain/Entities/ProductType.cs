using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        public ICollection<ProductTypeTranslatedText> Names { get; set; } = new HashSet<ProductTypeTranslatedText>();
        public ICollection<ProductTypeTranslatedText> Descriptions { get; set; } = new HashSet<ProductTypeTranslatedText>();

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

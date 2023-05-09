using Server.Domain.Entities.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Tag : Monitoring
    {
        public int Id { get; set; }
        public ICollection<TagTranslatedText> Names { get; set; } = new HashSet<TagTranslatedText>();
        public ICollection<TagTranslatedText> Descriptions { get; set; } = new HashSet<TagTranslatedText>();

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public Image Image { get; set; }
    }
}

using Server.Domain.DTOs.PageDTOs;
using Server.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.RequestDTOs
{
    public class ProductRequestDTO
    {
        public PageParameters PageParameters { get; set; }
        public ICollection<int> CategoryIds { get; set; } = new List<int>();
        public ICollection<int> TagIds { get; set; } = new List<int>();
        public string LanguageCode { get; set; } = "ukr";
        public string FilterStr { get; set; } = "";
        public string FilterType { get; set; } = FilterTypes.ByPriceDesc;
        public int? ProductTypeId { get; set; } = null;
    }
}

using Server.Domain.DTOs.ImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CustomerDTOs
{
    public class CustomerBaseDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public ImageDTO? Avatar { get; set; }
    }
}

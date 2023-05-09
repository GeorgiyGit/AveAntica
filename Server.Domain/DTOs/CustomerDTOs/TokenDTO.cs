using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CustomerDTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public ICollection<string> Roles { get; set; }
        //public DateTime Expiration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CustomerDTOs
{
    public class CustomerEditDTO:CustomerCreateDTO
    {
        public string Id { get; set; }
    }
}

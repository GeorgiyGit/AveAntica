using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.TagDTOs
{
    public class UpdateTagDTO : AddTagDTO
    {
        public int Id { get; set; }
        public bool IsImageChanged { get; set; }
    }
}

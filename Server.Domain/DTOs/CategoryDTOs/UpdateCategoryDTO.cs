using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO:AddCategoryDTO
    {
        public int Id { get; set; }
        public bool IsImageChanged { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface IUserService
    {
        internal string? GetCurrentUserId();
    }
}

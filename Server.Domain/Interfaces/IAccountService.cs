using Server.Domain.DTOs.CustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Interfaces
{
    public interface IAccountService
    {
        public Task Register(CustomerCreateDTO user);
        public Task RegisterAdmin(CustomerCreateDTO user);

        public Task<TokenDTO> Login(CustomerLoginDTO user);
        public Task<TokenDTO> ExternalLogin(ExternalAuthDTO externalAuth);

    }
}

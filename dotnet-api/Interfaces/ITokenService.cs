using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api.Models;

namespace dotnet_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
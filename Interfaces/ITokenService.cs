using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet8_api.Models;

namespace dotnet8_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
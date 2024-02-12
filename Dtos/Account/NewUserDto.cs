using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet8_api.Dtos.Account
{
    public class NewUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
    }
}
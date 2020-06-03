using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.WebAPI.Authentication
{
    public interface ITokenProvider
    {
        string CreateToken(string userLogin, DateTime expiry);

        TokenValidationParameters GetTokenValidationParameters();
    }
}

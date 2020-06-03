using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProLab.WebAPI.Authentication
{
    public class JwtProvider : ITokenProvider
    {
        private readonly RsaSecurityKey _key;
        private readonly string _algoritmo;
        private readonly string _issuer;
        private readonly string _audience;
        private string test;

        public JwtProvider(string issuer, string audience, string secret_key)
        {
            var parameters = new CspParameters() { KeyContainerName = secret_key };

            var provider = new RSACryptoServiceProvider( 2048, parameters );

            this._key = new RsaSecurityKey( provider );

            this._algoritmo = SecurityAlgorithms.RsaSha256Signature;

            this._issuer = issuer;

            this._audience = audience;

        }

        public string CreateToken(string userLogin, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            throw new NotImplementedException();
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            throw new NotImplementedException();
        }
    }
}

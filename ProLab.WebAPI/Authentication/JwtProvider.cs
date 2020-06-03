using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

            var provider = new RSACryptoServiceProvider(2048, parameters);

            this._key = new RsaSecurityKey(provider);

            this._algoritmo = SecurityAlgorithms.RsaSha256Signature;

            this._issuer = issuer;

            this._audience = audience;

        }

        public string CreateToken(Model.Entidades.AUTENTICA usuario, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var identity = new ClaimsIdentity(
                    new List<Claim>()
                    {
                        new Claim( ClaimTypes.Name, $"{usuario.FIRST_NAME} {usuario.FISRT_LAST_NAME}" ),
                        new Claim(ClaimTypes.Role, $"{usuario.ROLE}"),
                        new Claim(ClaimTypes.PrimarySid, $"{usuario.ID.ToString()}")
                    }, "CUSTOM"
                );

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(
                    new SecurityTokenDescriptor 
                    {
                        Audience = this._audience,
                        Issuer = this._issuer,
                        SigningCredentials = new SigningCredentials( this._key, this._algoritmo ),
                        Expires = expiry.ToUniversalTime(),
                        Subject = identity
                    }
                );

            var tokenResponse = tokenHandler.WriteToken(token);

            return tokenResponse;

        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                IssuerSigningKey = this._key,
                ValidAudience = this._audience,
                ValidIssuer = this._issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0)
            };
        }
    }
}

using Domain.Entities;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.IDDOCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly EfRepository _db;

        public AccountController(EfRepository db) => _db = db;



        [HttpPost("/api/token")]
        public object Token(string iin, string password)
        {
            var identity = GetIdentity(iin, password);


            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }


            return GetToken(identity);
        }


        [HttpPost("/api/user/registration")]
        public async Task<object> Create([FromBody] Client client)
        {
            client.ID = Guid.NewGuid();
            client.CreatedAt = DateTime.Now;

            await _db.AddAsync<Client>(client);

            var identity = GetIdentity(client.IIN, client.Password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            return GetToken(identity);
        }


        private ClaimsIdentity GetIdentity(string iin, string password)
        {
            Client client = _db.Get<Client>(x => x.IIN == iin && x.Password == password).Result;
            if (client != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, client.IIN)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }


            return null;
        }

        private object GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var tokenObj = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return tokenObj;
        }
    }
}

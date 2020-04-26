using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task07.Handler
{
    public class BasicAuthHandler
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing authentication header!");
            }

            // Authentication: Basic jggftgjk
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Incorrect authentication header!");
            }

            // here we need to check if the password is correct in the database 

            // if everything is correct
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "kunal"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };


            var identity = new ClaimsIdentity(claims, Scheme.Name);


            var principal = new ClaimsPrincipal(identity);


            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Infrastructure.Security.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Infrastructure.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly ClientRepository _clientRepository;

        public SimpleAuthorizationServerProvider()
        {
            _clientRepository = new ClientRepository();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            var client = _clientRepository.Read().FirstOrDefault(e => e.ClientId == clientId && e.ClientSecret == clientSecret);

            if (client != null)
            {
                context.Validated();
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var username = context.UserName;

            var password = context.Password;

            var userStore = new ApplicationUserStore();

            var usermanager = new ApplicationUserManager(userStore);

            var user = await usermanager.FindAsync(username, password);

            if (user != null)
            {
                var identity = await usermanager.CreateIdentityAsync(user, context.Options.AuthenticationType);

                var authProperties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "x:client_id", context.ClientId }
                });

                var ticket = new AuthenticationTicket(identity, authProperties);

                context.Validated(ticket);
            }
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var identity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(identity, context.Ticket.Properties);

            context.Validated(newTicket);
        }
    }
}
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Api.Security
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
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

                context.Validated(identity);
            }
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

            context.Validated(newTicket);
        }
    }
}
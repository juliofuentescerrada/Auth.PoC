using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace Api.Security
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);

            context.SerializeTicket();

            context.SetToken(refreshTokenId);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}
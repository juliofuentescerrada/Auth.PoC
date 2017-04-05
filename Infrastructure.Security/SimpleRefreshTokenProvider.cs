using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Security;
using Infrastructure.DataAccess;
using Microsoft.Owin.Security.Infrastructure;

namespace Infrastructure.Security
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly RefreshTokenRepository _refreshTokenRepository;

        public SimpleRefreshTokenProvider()
        {
            _refreshTokenRepository = new RefreshTokenRepository();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary["x:client_id"];

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var token = new RefreshToken
            {
                TokenId = refreshTokenId,
                ClientId = clientId,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;

            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            context.SetToken(refreshTokenId);

            _refreshTokenRepository.Create(token);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var refreshToken = _refreshTokenRepository.Read().FirstOrDefault(e => e.TokenId == context.Token);

            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);

                _refreshTokenRepository.Delete(refreshToken.Id);
            }
        }
    }
}
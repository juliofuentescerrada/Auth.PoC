using System;

namespace Domain.Entities.Security
{
    public class RefreshToken : Entity
    {
        public string TokenId { get; set; }
        public string ClientId { get; set; }
        public string Subject { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
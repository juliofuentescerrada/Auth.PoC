namespace Domain.Entities.Security
{
    public class Client : Entity
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
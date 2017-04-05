namespace Domain.Entities.Security
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
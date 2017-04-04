using Microsoft.AspNet.Identity;

namespace Api.Identity
{
    public class ApplicationUser : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}
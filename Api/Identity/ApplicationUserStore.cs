using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Api.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser, int>
    {
        private static readonly ICollection<ApplicationUser> Users = new List<ApplicationUser>();

        public async Task CreateAsync(ApplicationUser user)
        {
            var id = !Users.Any() ? 1 : Users.Max(e => e.Id) + 1;

            user.Id = id;

            Users.Add(user);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            var result = Users.FirstOrDefault(u => u.Id == user.Id);

            if (result != null)
            {
                result.UserName = user.UserName;

                result.PasswordHash = user.PasswordHash;
            }
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            var result = Users.FirstOrDefault(u => u.Id == user.Id);

            Users.Remove(result);
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            return Users.FirstOrDefault(u => u.Id == userId);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName);
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return user.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return !string.IsNullOrEmpty(user.PasswordHash);
        }

        public void Dispose()
        {
        }
    }
}
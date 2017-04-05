using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Security;
using Infrastructure.DataAccess;
using Microsoft.AspNet.Identity;

namespace Infrastructure.Security.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser, int>
    {
        private readonly UserRepository _userRepository;

        public ApplicationUserStore()
        {
            _userRepository = new UserRepository();
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            _userRepository.Create(new User
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName
            });
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _userRepository.Update(new User
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName
            });
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            _userRepository.Delete(user.Id);
        }

        public async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var user = _userRepository.Read(userId);

            return new ApplicationUser
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName
            };
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = _userRepository.Read().FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                return new ApplicationUser
                {
                    Id = user.Id,
                    PasswordHash = user.PasswordHash,
                    UserName = user.UserName
                };

            }

            return null;
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
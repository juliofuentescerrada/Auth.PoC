using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Security;

namespace Infrastructure.DataAccess
{
    public class UserRepository : BaseRepository<User>
    {
        private static readonly ICollection<User> Collection = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "julio",
                PasswordHash = "AMgy8M7Af0hAnfgZmxaQgB8LeETDwCXqBtLCb+IK3zV2fKftgO7OcUxbhAhD0EvvsA=="
            }
        };

        public override void Create(User entity)
        {
            var id = !Collection.Any() ? 1 : Collection.Max(e => e.Id) + 1;

            entity.Id = id;

            Collection.Add(entity);
        }

        public override IEnumerable<User> Read()
        {
            return Collection;
        }

        public override User Read(int id)
        {
            return Collection.FirstOrDefault(u => u.Id == id);
        }

        public override void Update(User entity)
        {
            var result = Collection.FirstOrDefault(u => u.Id == entity.Id);

            if (result != null)
            {
                result.UserName = entity.UserName;

                result.PasswordHash = entity.PasswordHash;
            }
        }

        public override void Delete(int id)
        {
            var result = Collection.FirstOrDefault(u => u.Id == id);

            if (result != null)
            {
                Collection.Remove(result);
            }
        }
    }
}
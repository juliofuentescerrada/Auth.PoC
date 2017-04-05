using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Security;

namespace Infrastructure.DataAccess
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>
    {
        private static readonly ICollection<RefreshToken> Collection = new List<RefreshToken>();

        public override void Create(RefreshToken entity)
        {
            var id = !Collection.Any() ? 1 : Collection.Max(e => e.Id) + 1;

            entity.Id = id;

            Collection.Add(entity);
        }

        public override IEnumerable<RefreshToken> Read()
        {
            return Collection;
        }

        public override RefreshToken Read(int id)
        {
            return Collection.FirstOrDefault(u => u.Id == id);
        }

        public override void Update(RefreshToken entity)
        {
            var result = Collection.FirstOrDefault(u => u.Id == entity.Id);

            if (result != null)
            {
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
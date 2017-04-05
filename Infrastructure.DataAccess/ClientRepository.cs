using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Security;

namespace Infrastructure.DataAccess
{
    public class ClientRepository : BaseRepository<Client>
    {
        private static readonly ICollection<Client> Collection = new List<Client>
        {
            new Client { Id = 1, ClientId = "Polaris", ClientSecret = "cb5f0ce1-50c7-4963-b7c4-4c3ddcab6de7" }
        };

        public override void Create(Client entity)
        {
            var id = !Collection.Any() ? 1 : Collection.Max(e => e.Id) + 1;

            entity.Id = id;

            Collection.Add(entity);
        }

        public override IEnumerable<Client> Read()
        {
            return Collection;
        }

        public override Client Read(int id)
        {
            return Collection.FirstOrDefault(u => u.Id == id);
        }

        public override void Update(Client entity)
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
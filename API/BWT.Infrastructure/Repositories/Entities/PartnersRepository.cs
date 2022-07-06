using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class PartnersRepository : BaseRepository<Partners>, IPartnersRepository
    {
        public PartnersRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}

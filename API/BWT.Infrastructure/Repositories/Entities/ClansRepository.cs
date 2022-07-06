using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class ClansRepository : BaseRepository<Clans>, IClansRepository
    {
        public ClansRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}
using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class GamesRepository : BaseRepository<Games>, IGamesRepository
    {
        public GamesRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}

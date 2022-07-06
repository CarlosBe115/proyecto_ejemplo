using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class SocialNetworksRepository : BaseRepository<SocialNetworks>, ISocialNetworksRepository
    {
        public SocialNetworksRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}

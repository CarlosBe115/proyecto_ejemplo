using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;
using System.Threading.Tasks;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class UserClanRepository : BaseRepository<UserClan>, IUserClanRepository
    {
        public UserClanRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}

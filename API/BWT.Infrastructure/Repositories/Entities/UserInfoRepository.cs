using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(BlackWarriorTeamDBContext context) : base(context) { }


    }
}

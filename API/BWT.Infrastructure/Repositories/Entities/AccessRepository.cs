using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BWT.Infrastructure.Repositories.Entities
{
    public class AccessRepository : BaseRepository<Access>, IAccessRepository
    {
        public AccessRepository(BlackWarriorTeamDBContext context) : base(context) { }

        public async Task<Access> EmailValidation(string email)
        {
            var user = await _entities.FirstOrDefaultAsync(x => x.EmailAddress == email);
            return user;
        }

        public async Task<Access> TokenValidation(string token)
        {
            var user = await _entities.FirstOrDefaultAsync(x => x.AccessKey == token);
            return user;
        }

        public async Task<Access> UserValidation(Access access)
        {
            var user = await _entities.FirstOrDefaultAsync(x =>
                x.EmailAddress == access.EmailAddress && x.EmailPassword == access.EmailPassword);
            return user;
        }


    }
}

using BWT.Core.Entities;
using BWT.Core.Interfaces.Entities;
using System;
using System.Threading.Tasks;

namespace BWT.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IAccessRepository AccessRepository { get; }

        IClansRepository ClansRepository { get; }

        IGamesRepository GamesRepository { get; }

        IPartnersRepository PartnersRepository{ get; }

        ISocialNetworksRepository SocialNetworksRepository { get; }

        IUserClanRepository UserClanRepository { get; }

        IUserInfoRepository UserInfoRepository { get; }

        IRepository<Rol> RolRepository { get; }

        IRepository<UCRol> UCRolRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();

        bool SaveChangesWithTransaction();

        Task SaveChangesAsyncWithTransaction();
    }
}

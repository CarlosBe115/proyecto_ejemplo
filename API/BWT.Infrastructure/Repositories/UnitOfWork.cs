using BWT.Core.Entities;
using BWT.Core.Interfaces;
using BWT.Core.Interfaces.Entities;
using BWT.Infrastructure.Data;
using BWT.Infrastructure.Repositories.Entities;
using System;
using System.Threading.Tasks;

namespace BWT.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlackWarriorTeamDBContext _context;
        private readonly IAccessRepository _access;
        private readonly IClansRepository _clans;
        private readonly IGamesRepository _games;
        private readonly IPartnersRepository _partners;
        private readonly ISocialNetworksRepository _socialNetworks;
        private readonly IUserClanRepository _userClan;
        private readonly IUserInfoRepository _infoRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<UCRol> _UCRolRepository;


        public UnitOfWork(BlackWarriorTeamDBContext context)
        {
            _context = context;
        }

        public IAccessRepository AccessRepository => _access ?? new AccessRepository(_context);

        public IClansRepository ClansRepository => _clans ?? new ClansRepository(_context);

        public IGamesRepository GamesRepository => _games ?? new GamesRepository(_context);

        public IPartnersRepository PartnersRepository => _partners ?? new PartnersRepository(_context);

        public ISocialNetworksRepository SocialNetworksRepository => _socialNetworks ?? new SocialNetworksRepository(_context);

        public IUserClanRepository UserClanRepository => _userClan ?? new UserClanRepository(_context);

        public IUserInfoRepository UserInfoRepository => _infoRepository ?? new UserInfoRepository(_context);

        public IRepository<Rol> RolRepository => _rolRepository ?? new BaseRepository<Rol>(_context);

        public IRepository<UCRol> UCRolRepository => _UCRolRepository ?? new BaseRepository<UCRol>(_context);

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool SaveChangesWithTransaction()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        public async Task SaveChangesAsyncWithTransaction()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
    
    }
}

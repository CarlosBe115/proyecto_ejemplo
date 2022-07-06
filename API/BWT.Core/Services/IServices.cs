using BWT.Core.Entities;
using BWT.Core.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BWT.Core.Services
{
    public interface IServices
    {

        #region Access

        Task<Access> UserValidation(Access access);

        //Task<bool> GeneratorTokenPasswordRecovery(string email);

        Task<string> GeneratorTokenPasswordRecovery(string email);

        Task<bool> ChangePassword(string token, string password1, string password2);

        IEnumerable<Access> GetAccesses(AccessFilters filters);

        Task<Access> GetAccess(int Id);

        Task<bool> AddAccess(Access access);

        Task<bool> UpdateAccess(Access access);

        Task<bool> DeleteAccess(int Id);
        #endregion

        #region User Information
        IEnumerable<UserInfo> GetUsersInfo();

        Task<UserInfo> GetUserInfo(int Id);

        Task<bool> AddUserInfo(UserInfo user);

        Task<bool> UpdateUserInfo(UserInfo user);

        Task<bool> DeleteUserInfo(int Id);
        #endregion

        #region SocialNetworks

        IEnumerable<SocialNetworks> GetSocialNetworks();

        Task<SocialNetworks> GetSocialNetwork(int Id);

        Task<bool> AddSocialNetworks(SocialNetworks socialNetworks);

        Task<bool> UpdateSocialNetworks(SocialNetworks socialNetworks);

        Task<bool> DeleteSocialNetworks(int Id);

        #endregion

        #region Partners

        IEnumerable<Partners> GetPartners();

        Task<Partners> GetPartner(int Id);

        Task<bool> AddPartners(Partners partners);

        Task<bool> UpdatePartners(Partners partners);

        Task<bool> DeletePartners(int Id);

        #endregion

        #region Clans
        IEnumerable<Clans> GetClans(ClansFilters filters);

        Task<Clans> GetClan(int Id);

        Task<bool> AddClans(Clans clans);

        Task<bool> UpdateClans(Clans clans);

        Task<bool> DeleteClans(int Id);
        #endregion

        #region Games

        IEnumerable<Games> GetGames(GamesFilters filters);

        Task<Games> GetGame(int Id);

        Task<bool> AddGames(Games games);

        Task<bool> UpdateGames(Games games);

        Task<bool> DeleteGames(int Id);

        #endregion

        #region Member

        IEnumerable<UserClan> GetUserClans();

        Task<UserClan> GetUserClan(int Id);

        Task<bool> AddUserClan(UserClan userClan);

        Task<bool> UpdateUserClan(UserClan userClan);

        Task<bool> DeleteUserClan(int Id);

        #endregion

        #region Rols

        IEnumerable<Rol> GetRols();

        IEnumerable<UCRol> GetUCRols();

        #endregion

    }
}

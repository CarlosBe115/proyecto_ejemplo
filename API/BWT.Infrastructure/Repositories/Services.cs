using BWT.Core.Entities;
using BWT.Core.Entities.Mail;
using BWT.Core.Exceptions;
using BWT.Core.Filters;
using BWT.Core.Interfaces;
using BWT.Core.Services;
using BWT.Infrastructure.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.Infrastructure.Repositories
{
    public class Services:IServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public Services(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Access

        public async Task<Access> UserValidation(Access access)
        {
            access.EmailPassword = Encode.MD5(access.EmailPassword);

            var user = await _unitOfWork.AccessRepository.UserValidation(access);

            if (user != null)
            {
                if (!user.IsValid)
                    throw new BusinessException($"Cuenta suspendida indefinidamente");

                if (user.TimeBan >= DateTime.Now)
                    throw new BusinessException($"Cuenta suspendida hasta: {user.TimeBan}");


                user.EmailPassword = "";
                user.AccessKey = "";
                user.IsValid = true;
                user.TimeBan = null;
            }
            else
                throw new BusinessException("Usuario no encontrado");

            return user;
        }

        //public async Task<bool> GeneratorTokenPasswordRecovery(string email)
        //{
        //    bool isValid = true;
        //    var user = await _unitOfWork.AccessRepository.EmailValidation(email);

        //    if (user != null)
        //    {
        //        ElectronicMail electronicMail = new ElectronicMail();

        //        user.AccessKey = Encode.SHA256(Guid.NewGuid().ToString());
        //        _unitOfWork.AccessRepository.Update(user);

        //        electronicMail.To = email;
        //        electronicMail.Title = "Recuperacion De Contraseña - Black Warriors Team";
        //        electronicMail.Token = user.AccessKey;

        //        if (Email.Send(electronicMail)) _unitOfWork.SaveChanges();
        //        else isValid = false;
        //    }
        //    else
        //        isValid = false;

        //    return isValid;
        //}

        public async Task<string> GeneratorTokenPasswordRecovery(string email)
        {
            string isValid = string.Empty;
            var user = await _unitOfWork.AccessRepository.EmailValidation(email);

            if (user != null)
            {
                Core.Entities.Mail.EMail electronicMail = new Core.Entities.Mail.EMail();

                user.AccessKey = Encode.SHA256(Guid.NewGuid().ToString());
                _unitOfWork.AccessRepository.Update(user);

                _unitOfWork.SaveChanges();
                isValid = user.AccessKey;
            }
            else
                throw new BusinessException("Correo no valido");

            return isValid;
        }

        public async Task<bool> ChangePassword(string token, string password1, string password2)
        {
            bool isvalid = false;

            if (password1 == password2 && token != null && token != "")
            {
                var user = await _unitOfWork.AccessRepository.TokenValidation(token);
                if (user != null)
                {
                    user.EmailPassword = Encode.MD5(password1);
                    user.AccessKey = "";

                    _unitOfWork.AccessRepository.Update(user);

                    _unitOfWork.SaveChanges();
                    isvalid = true;
                }
            }
            else
                throw new BusinessException("Error en los datos, verifique de nuevo");

            return isvalid;
        }

        public IEnumerable<Access> GetAccesses(AccessFilters filters)
        {
            var accesses = _unitOfWork.AccessRepository.GetAll();

            if (filters.Id != null)
                accesses = accesses.Where(x => x.Id == filters.Id);
            if (filters.EmailAddress != null)
                accesses = accesses.Where(x => x.EmailAddress == filters.EmailAddress);
            if (filters.FkRol != null)
                accesses = accesses.Where(x => x.FkRol == filters.FkRol);
            if (filters.IsValid != null)
                accesses = accesses.Where(x => x.IsValid == filters.IsValid);
            if (filters.TimeBan != null)
                accesses = accesses.Where(x => x.TimeBan == filters.TimeBan);

            if (accesses != null)
            {
                foreach(var a in accesses)
                {
                    a.EmailPassword = "";
                    a.AccessKey = "";
                    a.IsValid = true;
                    a.TimeBan = null;
                }
            }

            return accesses;
        }

        public async Task<Access> GetAccess(int Id)
        {
            var access = await _unitOfWork.AccessRepository.GetById(Id);

            if(access != null){
                access.EmailPassword = "";
                access.AccessKey = "";
                access.IsValid = true;
                access.TimeBan = null;
            }

            return access;
        }
        
        public async Task<bool> AddAccess(Access access)
        {
            var filter = new AccessFilters();
            filter.EmailAddress = access.EmailAddress;

            if (GetAccesses(filter).Count() != 0)
                throw new BusinessException("Correo en existencia, utilize uno diferente");

            access.EmailPassword = Encode.MD5(access.EmailPassword);

            await _unitOfWork.AccessRepository.Add(access);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateAccess(Access access)
        {
            access.EmailPassword = Encode.MD5(access.EmailPassword);

            _unitOfWork.AccessRepository.Update(access);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAccess(int Id)
        {
            await _unitOfWork.AccessRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region User Information

        public IEnumerable<UserInfo> GetUsersInfo()
        {
            var users = _unitOfWork.UserInfoRepository.GetAll();
            return users;
        }

        public async Task<UserInfo> GetUserInfo(int Id)
        {
            var users = _unitOfWork.UserInfoRepository.GetAll();
            var user = users.FirstOrDefault(x =>x.FkAccess == Id);

            return user;
        }

        public async Task<bool> AddUserInfo(UserInfo user)
        {
            await _unitOfWork.UserInfoRepository.Add(user);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateUserInfo(UserInfo user)
        {
            _unitOfWork.UserInfoRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserInfo(int Id)
        {
            await _unitOfWork.UserInfoRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region SocialNetworks

        public IEnumerable<SocialNetworks> GetSocialNetworks()
        {
            var socialNetworks = _unitOfWork.SocialNetworksRepository.GetAll();
            return socialNetworks;
        }

        public async Task<SocialNetworks> GetSocialNetwork(int Id)
        {
            var socialNetwork = await _unitOfWork.SocialNetworksRepository.GetById(Id);
            return socialNetwork;
        }

        public async Task<bool> AddSocialNetworks(SocialNetworks social)
        {
            await _unitOfWork.SocialNetworksRepository.Add(social);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateSocialNetworks(SocialNetworks social)
        {
            _unitOfWork.SocialNetworksRepository.Update(social);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSocialNetworks(int Id)
        {
            await _unitOfWork.SocialNetworksRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region Partners

        public IEnumerable<Partners> GetPartners()
        {
            var partners = _unitOfWork.PartnersRepository.GetAll();
            return partners;
        }

        public async Task<Partners> GetPartner(int Id)
        {
            var partner = await _unitOfWork.PartnersRepository.GetById(Id);
            return partner;
        }

        public async Task<bool> AddPartners(Partners partners)
        {
            await _unitOfWork.PartnersRepository.Add(partners);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdatePartners(Partners partners)
        {
            _unitOfWork.PartnersRepository.Update(partners);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePartners(int Id)
        {
            await _unitOfWork.PartnersRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region Clans

        public IEnumerable<Clans> GetClans(ClansFilters filters)
        {
            var isUIExist = _unitOfWork.UserInfoRepository.GetAll().FirstOrDefault(x => x.FkAccess == filters.FkUserCreator);
            var isClanExist = _unitOfWork.ClansRepository.GetAll();

            if (filters.FkGames != null)
                isClanExist = isClanExist.Where(x => x.FkGames == filters.FkGames);
            if (filters.NameClan != null)
                isClanExist = isClanExist.Where(x => x.NameClan.ToLower().Contains(filters.NameClan.ToLower()));
            if (filters.Abbreviation != null)
                isClanExist = isClanExist.Where(x => x.Abbreviation == filters.Abbreviation);
            if (filters.CreationClan != null)
                isClanExist = isClanExist.Where(x => x.CreationClan == filters.CreationClan);
            if (filters.Ranked != null)
                isClanExist = isClanExist.Where(x => x.Ranked == filters.Ranked);
            if (filters.FkUserCreator != null)
                if (isUIExist != null)
                    isClanExist = isClanExist.Where(x => x.FkUserCreator == isUIExist.Id);

            return isClanExist;
        }

        public async Task<Clans> GetClan(int Id)
        {
            var access = await _unitOfWork.ClansRepository.GetById(Id);
            return access;
        }

        public async Task<bool> AddClans(Clans clans)
        {

            #region Clans
            var isGamesValid = await _unitOfWork.GamesRepository.GetById(clans.FkGames);
            var isUsersValid = _unitOfWork.UserInfoRepository.GetAll();
            var isClanExist = _unitOfWork.ClansRepository.GetAll();

            if (isGamesValid == null)
                throw new BusinessException("Juego no valido");

            var isUserValid = isUsersValid.FirstOrDefault(x => x.FkAccess == clans.FkUserCreator);
            if (isUserValid == null)
                throw new BusinessException("Cree su informacion de usuario");

            if (isClanExist.Where(x => x.FkUserCreator == isUserValid.Id).Count() != 0)
                throw new BusinessException("Ya es dueño de un clan");

            clans.LimitUser = isGamesValid.LimitUserGames;
            clans.CurrentUser = 0;
            clans.FkUserCreator = isUserValid.Id;

            await _unitOfWork.ClansRepository.Add(clans);
            _unitOfWork.SaveChanges();
            #endregion

            #region agregar

            var member = new UserClan();

            member.FkUser= isUserValid.Id;
            member.FkClan= clans.Id;
            member.FkUcrol= 3;
            member.DateRegister= DateTime.Now;
            member.IsValid = true;

            var r = AddUserClan(member);
            #endregion

            return true;
        }

        public async Task<bool> UpdateClans(Clans clans)
        {
            _unitOfWork.ClansRepository.Update(clans);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClans(int Id)
        {
            await _unitOfWork.ClansRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region Games

        public IEnumerable<Games> GetGames(GamesFilters filters)
        {
            var games = _unitOfWork.GamesRepository.GetAll();

            if (filters.NameGame != null)
                games = games.Where(x => x.NameGame == filters.NameGame);
            if (filters.DescriptionGame != null)
                games = games.Where(x => x.DescriptionGame.ToLower().Contains(filters.DescriptionGame.ToLower()));

            return games;
        }

        public async Task<Games> GetGame(int Id)
        {
            var game = await _unitOfWork.GamesRepository.GetById(Id);
            return game;
        }

        public async Task<bool> AddGames(Games games)
        {
            await _unitOfWork.GamesRepository.Add(games);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateGames(Games games)
        {
            _unitOfWork.GamesRepository.Update(games);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGames(int Id)
        {
            await _unitOfWork.GamesRepository.Delete(Id);
            _unitOfWork.SaveChanges();
            return true;
        }

        #endregion

        #region Member

        public IEnumerable<UserClan> GetUserClans()
        {
            var userClans = _unitOfWork.UserClanRepository.GetAll();
            return userClans;
        }

        public async Task<UserClan> GetUserClan(int Id)
        {
            var userClan = await _unitOfWork.UserClanRepository.GetById(Id);
            return userClan;
        }

        public async Task<bool> AddUserClan(UserClan userClan)
        {
            bool response = false;
            var clans = await _unitOfWork.ClansRepository.GetById(userClan.FkClan);
            var userClans = _unitOfWork.UserClanRepository.GetAll();
            var isExistMember = userClans.Where(x => x.FkUser == userClan.FkUser && x.FkClan == userClan.FkClan);

            if (isExistMember.Count() != 0)
                throw new BusinessException("Ya pertence a este clan");

            if (clans.CurrentUser >= clans.LimitUser)
                throw new BusinessException("Clan User Limit Reached");

            clans.CurrentUser = clans.CurrentUser + 1;

            _unitOfWork.ClansRepository.Update(clans);
            await _unitOfWork.UserClanRepository.Add(userClan);
            response = _unitOfWork.SaveChangesWithTransaction();
            return response;
        }

        public async Task<bool> UpdateUserClan(UserClan userClan)
        {
            _unitOfWork.UserClanRepository.Update(userClan);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserClan(int Id)
        {
            bool response = false;
            var member = await _unitOfWork.UserClanRepository.GetById(Id);
            var clan = await _unitOfWork.ClansRepository.GetById(member.FkClan);

            clan.CurrentUser = clan.CurrentUser - 1;

            _unitOfWork.ClansRepository.Update(clan);
            await _unitOfWork.UserClanRepository.Delete(Id);
            response = _unitOfWork.SaveChangesWithTransaction();
            return response;
        }


        #endregion

        #region Rols

        public IEnumerable<Rol> GetRols()
        {
            var rols = _unitOfWork.RolRepository.GetAll();
            return rols;
        }

        public IEnumerable<UCRol> GetUCRols()
        {
            var rols = _unitOfWork.UCRolRepository.GetAll();
            return rols;
        }

        #endregion

    }
}

using BWT.Core.Entities;
using BWT.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BWT.Infrastructure.Data
{
    public partial class BlackWarriorTeamDBContext : DbContext
    {
        public BlackWarriorTeamDBContext(){}

        public BlackWarriorTeamDBContext(DbContextOptions<BlackWarriorTeamDBContext> options) : base(options){}

        public virtual DbSet<Access> TbAccess { get; set; }
        public virtual DbSet<Clans> TbClans { get; set; }
        public virtual DbSet<Games> TbGames { get; set; }
        public virtual DbSet<Partners> TbPartners { get; set; }
        public virtual DbSet<Rol> TbRol { get; set; }
        public virtual DbSet<SocialNetworks> TbSocialNetworks { get; set; }
        public virtual DbSet<UCRol> TbUcrol { get; set; }
        public virtual DbSet<UserClan> TbUserClan { get; set; }
        public virtual DbSet<UserInfo> TbUserInfo { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=DESKTOP-27FSFUB\\SQLEXPRESS01;Database=BlackWarriorTeamDB;Integrated Security = true");
        //              
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccessConfigurations());

            modelBuilder.ApplyConfiguration(new ClansConfigurations());

            modelBuilder.ApplyConfiguration(new GamesConfigurations());

            modelBuilder.ApplyConfiguration(new PartnersConfigurations());

            modelBuilder.ApplyConfiguration(new SocialNetworksConfigurations());

            modelBuilder.ApplyConfiguration(new UserClanConfigurations());

            modelBuilder.ApplyConfiguration(new UserInfoConfigurations());

            modelBuilder.ApplyConfiguration(new UCRolConfigurations());

            modelBuilder.ApplyConfiguration(new RolConfigurations());

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

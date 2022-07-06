using BWT.Core.Entities;
using System.Threading.Tasks;

namespace BWT.Core.Interfaces.Entities
{
    public interface IAccessRepository: IRepository<Access>
    {
        Task<Access> UserValidation(Access access);

        Task<Access> EmailValidation(string email);

        Task<Access> TokenValidation(string token);
    }
}

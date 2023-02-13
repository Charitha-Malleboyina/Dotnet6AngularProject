using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IRegisterUser
    {
       Task<List<RegisterUsersModel>> GetAllUserDetails();
        Task<bool> AddUsersDetails(RegisterUsersModel registerUsersModel);
        Task<String> EditUsersDetails(Guid id, RegisterUsersModel registerUsersModel);
        Task<bool> DeleteUsersDetails(Guid id);
        Task<bool> GetDetails(LoginModel login);
        Task<List<Role>> GetRoleTypes();
        Task<RegisterUsersModel> GetDetailsbyid(string email);
        Task<List<RegisterUsersModel>> GetDetailsbyName(string name);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class RegisterUserRepo:IRegisterUser
    {
        private readonly dbContext _dbContext;
        public RegisterUserRepo(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUsersDetails(RegisterUsersModel registerUsersModel)
        {
            var user = await _dbContext.registerUsers.FirstOrDefaultAsync(s => s.Email == registerUsersModel.Email);
            if (user!= null)
            {
                return false;
            }
            else
            {
                await _dbContext.registerUsers.AddAsync(registerUsersModel);
                var result = await _dbContext.SaveChangesAsync();
                return result == 0 ? false : true;
            }

        }

        public async Task<bool> DeleteUsersDetails(Guid id)
        {
            var studentModel = await _dbContext.registerUsers.FindAsync(id);
            _dbContext.registerUsers.Remove(studentModel);
            await _dbContext.SaveChangesAsync();
          return true;
        }

        public async Task<string> EditUsersDetails(Guid id, RegisterUsersModel registerUsersModel)
        {
           var user= _dbContext.registerUsers.FirstOrDefault(s => s.UserId.Equals(id));
            user.FirstName = registerUsersModel.FirstName;
            user.LastName = registerUsersModel.LastName;
            user.Email = registerUsersModel.Email;
            user.Password = registerUsersModel.Password;
           
            await _dbContext.SaveChangesAsync();
            return "Updated successfully";
        }

        public async Task<List<RegisterUsersModel>> GetAllUserDetails()
        {
           var result = await _dbContext.registerUsers.ToListAsync();
            return result;
        }
        public async Task<bool> GetDetails(LoginModel login)
        {
            var user = await _dbContext.registerUsers.FirstOrDefaultAsync(s=>s.Email==login.email);
            if (user!=null)
            {
                return user.Password== login.password? true:false;
            }
            return false;
        }

        public async Task<List<Role>> GetRoleTypes()
        {
            var result = await _dbContext.Roles.ToListAsync();
            return result;
        }

        public async Task<RegisterUsersModel> GetDetailsbyid(string email)
        {
            var user = await _dbContext.registerUsers.FirstOrDefaultAsync(s => s.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<List<RegisterUsersModel>> GetDetailsbyName(string name)
        {
            var user = await _dbContext.registerUsers.Where(s=>s.FirstName==name).ToListAsync();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Models;

namespace GroopWebApp.Interfaces
{
    public interface IUserRepsotiroy
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
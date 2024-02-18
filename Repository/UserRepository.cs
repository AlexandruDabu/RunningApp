using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Data;
using GroopWebApp.Interfaces;
using GroopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GroopWebApp.Repository
{
    public class UserRepository : IUserRepsotiroy
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
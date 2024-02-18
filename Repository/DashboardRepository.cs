using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Data;
using GroopWebApp.Interfaces;
using GroopWebApp.Models;

namespace GroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
            _context = context;
            
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccesor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r=>r.AppUser.Id == curUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContextAccesor.HttpContext?.User.GetUserId() ;
            var userRaces = _context.Races.Where(r=>r.AppUser.Id == curUser);
            return userRaces.ToList();
        }
    }
}
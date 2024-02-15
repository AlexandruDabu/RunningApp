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
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i=>i.Address).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(c=>c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
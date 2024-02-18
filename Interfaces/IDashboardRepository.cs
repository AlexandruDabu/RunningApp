using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroopWebApp.Models;

namespace GroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
    }
}
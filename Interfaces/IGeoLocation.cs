using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroopWebApp.Interfaces
{
    public interface IGeoLocation
    {
        Task<string> GetIpAddress();
        Task<string> GetGeoInfo();
    }
}
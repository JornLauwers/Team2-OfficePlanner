using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public interface IAuthenticationManager
    {
        string Authenticate(string username);
    }
}

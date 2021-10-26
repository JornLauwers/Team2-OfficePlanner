using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficePlanner.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficePlanner.Shared;

namespace OfficePlanner.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomVersions> RoomVersions { get; set; }
        public DbSet<Setting> Setting { get; set; }
        //public DbSet<Users> Users { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        //public DbSet<Users> Users { get; set; }

        public DbSet<OfficePlanner.Server.Models.ApplicationRole> ApplicationRole { get; set; }
    }
}

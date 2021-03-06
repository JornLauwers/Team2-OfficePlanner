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
        public DbSet<Reservations<ApplicationUser>> Reservations { get; set; }
        public DbSet<Rooms<ApplicationUser>> Rooms { get; set; }
        public DbSet<RoomVersions<ApplicationUser>> RoomVersions { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<PersonUser> PersonUser { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<OfficePlanner.Server.Models.ApplicationRole> ApplicationRole { get; set; }
    }
}

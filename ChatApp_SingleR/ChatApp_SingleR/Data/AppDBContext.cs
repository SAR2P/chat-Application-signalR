using ChatApp_SingleR.Authentication;
using ChatModelsLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_SingleR.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<GroupChat> GropChats { get; set; }

        public DbSet<AvailableUser> AvailableUsers { get; set; }

        public DbSet<IndividualChat> IndividualChats { get; set; }

    }
}

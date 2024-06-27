using ChatApp_SingleR.Authentication;
using ChatApp_SingleR.Client.Models;
using ChatModelsLibrary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_SingleR.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<AvailableUser> AvailableUsers { get; set; }

    }
}

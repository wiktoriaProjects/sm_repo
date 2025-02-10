using IdentityApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.InteropServices;

namespace IdentityApi
{

    public class UserDbContext: IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
    //public class UserDbContext : DbContext
    //{
    //    public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions): base(dbContextOptions)
    //    {
    //        try
    //        {
    //            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
    //            if(databaseCreator != null) 
    //            {
    //                if (!databaseCreator.CanConnect()) databaseCreator.Create();
    //                if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
    //            }
    //        }
    //        catch (Exception ex)
    //        { 
    //        Console.WriteLine(ex.Message);
    //        }

    //    }

    //    public DbSet<User> Users { get; set; }
    //}
}

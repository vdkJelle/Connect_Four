using ConnectFourWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConnectFourWeb.Data;

public class ConnectFourWebContext: IdentityDbContext<ConnectFourWebUser>
{
    public ConnectFourWebContext(DbContextOptions<ConnectFourWebContext> options)
        : base(options)
    {
    }

    public DbSet<DbGame>? Games { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

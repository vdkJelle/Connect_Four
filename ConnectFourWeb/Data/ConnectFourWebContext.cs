using ConnectFourWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        builder.Entity<DbGame>()
            .OwnsOne(g => g.GameManager, gm =>
            {
                gm.Property(g => g.PlayerOne).HasColumnName("PlayerOne")
                    .HasConversion(p => JsonConvert.SerializeObject(p),
                                    json => JsonConvert.DeserializeObject<Player>(json));

                gm.Property(g => g.PlayerTwo).HasColumnName("PlayerTwo")
                    .HasConversion(p => JsonConvert.SerializeObject(p),
                                    json => JsonConvert.DeserializeObject<Player>(json));

                gm.Property(g => g.PlayerTurn).HasColumnName("PlayerTurn")
                    .HasConversion(p => JsonConvert.SerializeObject(p),
                                    json => JsonConvert.DeserializeObject<Player>(json));
            });
    }
}

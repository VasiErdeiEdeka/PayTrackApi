using Microsoft.EntityFrameworkCore;
using PayTrack.Infrastructure.Context;

namespace PayTrack.API;

public static class ExecuteMigration
{
    public static async Task ExecuteMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PayTrackDbContext>();

        if ((await db.Database.GetPendingMigrationsAsync()).Any())
        {
            await db.Database.MigrateAsync();
        }
        else
        {
            await db.Database.EnsureCreatedAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RealTimeChat.DataAccess;

[ExcludeFromCodeCoverage]
public sealed class ChatDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

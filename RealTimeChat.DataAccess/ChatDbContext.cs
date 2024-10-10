using Microsoft.EntityFrameworkCore;
using RealTimeChat.Domain.Users;
using System.Diagnostics.CodeAnalysis;

namespace RealTimeChat.DataAccess;

[ExcludeFromCodeCoverage]
public sealed class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

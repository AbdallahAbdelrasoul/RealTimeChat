using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;
using RealTimeChat.Domain.Users;

namespace RealTimeChat.DataAccess.Mappings
{
    public class UsersDbMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(k => k.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(256);
            builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IsOnline).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsVerified).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.EmailOTP).HasMaxLength(6);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasIndex(x => x.Email).IsUnique().HasFilter("\"IsDeleted\" = false");
            builder.HasIndex(x => x.UserName).IsUnique().HasFilter("\"IsDeleted\" = false");

            builder.Ignore(x => x.Validator);
        }
    }
}

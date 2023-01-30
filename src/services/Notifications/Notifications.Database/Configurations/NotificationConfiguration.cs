using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Notifications.Database.Models;

namespace Notifications.Database.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    private readonly string _tableName = "Notification";

    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SequentialGuidValueGenerator>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt).IsRequired(false);
    }
}
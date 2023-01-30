using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Notifications.Database.Models;

namespace Notifications.Database.Configurations;

public class NotificationClassificationConfiguration : IEntityTypeConfiguration<NotificationClassification>
{
    private readonly string _tableName = "NotificationClassifications";

    public void Configure(EntityTypeBuilder<NotificationClassification> builder)
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

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasData(
            new NotificationClassification { Name = "Email", Id = Guid.NewGuid(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new NotificationClassification { Name = "SMS", Id = Guid.NewGuid(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new NotificationClassification { Name = "Push", Id = Guid.NewGuid(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow }
        );
    }
}
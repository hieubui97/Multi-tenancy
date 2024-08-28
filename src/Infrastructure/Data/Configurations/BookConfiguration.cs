using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using multi_tenant_ca.Domain.Entities;

namespace multi_tenant_ca.Infrastructure.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(DataSchemaConstants.DbTablePrefix + "Books",
               DataSchemaConstants.DbSchema);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}

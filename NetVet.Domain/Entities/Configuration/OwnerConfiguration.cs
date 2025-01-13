using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace NetVet.Domain.Entities.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");
            builder.HasKey(x => x.OwnerId);
            builder.Property(x => x.OwnerId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Pets)
                   .WithOne(x => x.Owner)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Contacts)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("OwnerContacts"));

            builder.HasMany(x => x.Notes)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("OwnerNotes"));
        }
    }
}

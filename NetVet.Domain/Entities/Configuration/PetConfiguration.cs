using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace NetVet.Domain.Entities.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pets");
            builder.HasKey(x => x.PetId);
            builder.Property(x => x.PetId).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Owner)
                   .WithMany(x => x.Pets)
                   .HasForeignKey("OwnerId");

            builder.HasOne(x => x.Animal)
                   .WithMany()
                   .HasForeignKey("AnimalId");

            builder.HasMany(x => x.Notes)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("PetNotes"));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace NetVet.Domain.Entities.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(x => x.AppointmentId);
            builder.Property(x => x.AppointmentId).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Pet)
                   .WithMany()
                   .HasForeignKey("PetId");

            builder.HasMany(x => x.Notes)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("AppointmentNotes"));
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.EntityConfigurations;

public class ParkingTemplateConfiguration : IEntityTypeConfiguration<ParkingTemplate>
{
    public void Configure(EntityTypeBuilder<ParkingTemplate> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Name);
        builder.Property(x => x.FloorsCount);
        builder.Property(x => x.SlotsCount);
        builder.Property(x => x.Price);

        builder
            .HasMany(x => x.Parkings)
            .WithOne(x => x.ParkingTemplate)
            .HasForeignKey(x => x.ParkingTemplateId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
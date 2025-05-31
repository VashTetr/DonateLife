using Microsoft.EntityFrameworkCore;
using DonateLife.Common.Entity;
using DonateLife.Common.Interface;

namespace DonateLife.Infrastructure.Data;

public static class DataBaseModelConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Account)
            .WithOne();
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Patient)
            .WithOne();
        modelBuilder.Entity<Person>()
            .HasIndex(p => p.EMail)
            .IsUnique();
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Role)
            .WithMany();
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.BloodType)
            .WithMany();
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Sex)
            .WithMany();
        modelBuilder.Entity<Illness>();
        modelBuilder.Entity<PreviousIllness>()
            .HasOne<Illness>()
            .WithMany();
        modelBuilder.Entity<PreviousIllness>()
            .HasOne<Patient>()
            .WithMany();

        // Lookup Tables
        modelBuilder.Entity<Sex>();
        modelBuilder.Entity<BloodType>();
        modelBuilder.Entity<Organ>();
    }
}
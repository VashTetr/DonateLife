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
			.WithMany()
			.HasForeignKey(p => p.AccountID);
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Patient)
            .WithMany()
			.HasForeignKey(p => p.PatientID);
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



using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using DonateLife.Common.Entity;
using DonateLife.Common.Interface;

namespace DonateLife.Infrastructure;

public class DataBase : DbContext, IDataBase
{
    public string DbPath { get; }
    public string connectionString { get; }

    public DataBase()
    {
        return;
        DbPath = "./DataBase/DonateLife.db";
        connectionString = $"Data Source={DbPath};Version=3;";
        if (!File.Exists(DbPath))
        {
            File.WriteAllText(DbPath, string.Empty);
            var sql = File.ReadAllText("DataBase/Create/setup.sql");
            ExecuteSqlQuery(sql);
        }

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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

    public DataTable ExecuteSqlQuery(string sqlQuery)
    {
        // Create an empty DataTable to hold the query results
        DataTable dataTable = new DataTable();

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            conn.Close();
        }

        return dataTable;
    }

    public T? SingleOrDefault<T>(Guid id) where T : DBEntity
    {
        return Find<T>(id);
    }

    public T Single<T>(Guid id) where T : DBEntity
    {
        var entity = Find<T>(id);
        if (entity is null)
        {
            throw new Exception($"Not found - {typeof(T).Name} {id}");
        }
        return entity;
    }

    public IEnumerable<T> GetAll<T>() where T : DBEntity
    {
        return Set<T>();
    }

    public IEnumerable<T> ExecuteSqlQuery<T>(string sqlQuery) where T : DBEntity, new()
    {
        return ExecuteSqlQuery(sqlQuery)
            .AsEnumerable()
            .Select(x => x.ConvertDataRowToType(new T()));
    }

    IEnumerable<T> IDataBase.Set<T>() => base.Set<T>();

    void IDataBase.Add<T>(T t) => base.Add(t);
}
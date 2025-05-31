

using System.Data;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using DonateLife.Common.Entity;
using DonateLife.Common.Interface;
using DonateLife.Infrastructure.Data;

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
        // optionsBuilder.UseLazyLoadingProxies(); // TODO: consider whether to use this or not
		optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=mydb;User Id=myuser;Password=mypassword;");
		optionsBuilder.UseLowerCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
		optionsBuilder.UseLowerCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataBaseModelConfiguration.Configure(modelBuilder);
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

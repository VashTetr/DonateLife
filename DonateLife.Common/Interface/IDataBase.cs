using System.Data;
using DonateLife.Common.Entity;

namespace DonateLife.Common.Interface;

public interface IDataBase
{
    string DbPath { get; }
    string connectionString { get; }

    DataTable ExecuteSqlQuery(string sqlQuery);
    IEnumerable<T> ExecuteSqlQuery<T>(string sqlQuery) where T : DBEntity, new();
    IEnumerable<T> GetAll<T>() where T : DBEntity;
    T Single<T>(Guid id) where T : DBEntity;
    T? SingleOrDefault<T>(Guid id) where T : DBEntity;

    // base class methods
    IEnumerable<T> Set<T>() where T : DBEntity;
    void Add<T>(T t) where T : DBEntity;
}
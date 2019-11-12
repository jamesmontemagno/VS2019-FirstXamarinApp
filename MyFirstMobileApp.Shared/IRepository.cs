using MyFirstMobileApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstMobileApp.Models
{
    public interface IRepository<T>
    {
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Remove(string key);
        Task<T> Get(string id);
        Task<IEnumerable<Item>> GetAll();
    }
}

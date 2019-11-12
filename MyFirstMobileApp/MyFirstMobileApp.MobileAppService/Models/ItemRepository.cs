using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using MyFirstMobileApp.Shared.Models;
using System.Threading.Tasks;

namespace MyFirstMobileApp.Models
{
    public class ItemRepository : IRepository<Item>
    {
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        public ItemRepository()
        {
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 1", Description = "This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" });
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 2", Description = "This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" });
            Add(new Item { Id = Guid.NewGuid().ToString(), Text = "Item 3", Description = "This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" });
        }

        public Task<Item> Get(string id)
        {
            return Task.FromResult(items[id]);
        }

        public Task<IEnumerable<Item>> GetAll()
        {
            return Task.FromResult(items.Values as IEnumerable<Item>);
        }

        public Task<bool> Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
            return Task.FromResult(true);
        }

        public Item Find(string id)
        {
            items.TryGetValue(id, out Item item);

            return item;
        }

        public Task<bool> Remove(string id)
        {
            items.TryRemove(id, out Item item);

            return Task.FromResult(true);
        }

        public Task<bool> Update(Item item)
        {
            items[item.Id] = item;
            return Task.FromResult(true);
        }
    }
}

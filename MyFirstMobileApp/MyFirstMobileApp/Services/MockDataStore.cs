using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstMobileApp.Models;
using MyFirstMobileApp.Shared.Models;

namespace MyFirstMobileApp.Services
{
    public class MockDataStore : IRepository<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description.", Icon = "https://raw.githubusercontent.com/jamesmontemagno/VS2019-FirstXamarinApp/master/advocado.png" }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> Add(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> Update(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> Remove(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> Get(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await Task.FromResult(items);
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstMobileApp.Models;
using MyFirstMobileApp.Shared.Models;

namespace MyFirstMobileApp.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        private readonly IRepository<Item> ItemRepository;

        public ItemController(IRepository<Item> itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(value: await ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<Item> GetItem(string id)
        {
            Item item = await ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                await ItemRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                await ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await ItemRepository.Remove(id);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.Application.Interfaces;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task AddAsync(Item item)
        {
            await _itemRepository.AddAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            await _itemRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item != null)
            {
                await _itemRepository.DeleteAsync(item);
            }
        }
    }
}

using Restaurant.Models;
using Restaurant.Models.DTOs;

namespace Restaurant.Services.Interface
{
    public interface IAdminService
    {
        public Task<MenuItem> CreateMenuItemAsync(MenuItemDto model);
        public Task<Table> CreateTableAsync(TableDto model);
    }
}

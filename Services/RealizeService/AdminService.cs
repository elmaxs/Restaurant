using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.Interface;

namespace Restaurant.Services.RealizeService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AdminService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItemDto model)
        {
            if (model == null)
                throw new InvalidOperationException("Bad data");

            var menuItem = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                UrlImageAdress = model.ImageUrl,
                Price = model.Price
            };

            await applicationDbContext.MenuItems.AddAsync(menuItem);
            await applicationDbContext.SaveChangesAsync();

            return menuItem;
        }

        public async Task<Table> CreateTableAsync(TableDto model)
        {
            if (model == null)
                throw new InvalidOperationException("Bad data");

            var table = new Table
            {
                Id = Guid.NewGuid(),
                Number = model.TableNumber,
                Capacity = model.Capacity,
                PricePerHour = model.PricePerHour,
                Status = model.Status
            };

            await applicationDbContext.Tables.AddAsync(table);
            await applicationDbContext.SaveChangesAsync();

            return table;
        }
    }
}

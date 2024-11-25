﻿namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DishService : IDishService
    {
        private readonly IRepository repository;

        public DishService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(Dish dish)
        {
            if (!await ExistsByIdAsync(dish.Id))
            {
                await repository.AddAsync(dish);
                await repository.SaveChangesAsync();

                return "Dish added Successfully!";
            }

            return "Dish already exists!";
        }

        public async Task<IEnumerable<Dish>> AllAsync()
        {
            return await repository.AllAsync<Dish>();
        }

        public async Task<bool> ExistsByIdAsync(int dishId)
        {
            return await repository.AllAsReadOnly<Dish>().AnyAsync(d => d.Id == dishId);
        }

        public async Task<Dish> GetByIdAsync(int dishId)
        {
            return await repository.AllAsReadOnly<Dish>().FirstOrDefaultAsync(d => d.Id == dishId);
        }

        public async Task<Dish> GetByNameAsync(string dishName)
        {
            return await repository.AllAsReadOnly<Dish>().FirstOrDefaultAsync(d => d.Name == dishName);
        }

        public async Task<string> Remove(int id)
        {
            if (await ExistsByIdAsync(id))
            {
                await repository.RemoveAsync<Dish>(id);
                await repository.SaveChangesAsync();
                return "Dish removed successfully!";
            }
            return "Dish doesn't exists!";
        }
    }
}
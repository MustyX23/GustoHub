﻿namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;

    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;
        private readonly ICategoryService categoryService;

        public DishController(
            IDishService dishService,
            ICategoryService categoryService)
        {
            this.dishService = dishService;
            this.categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDishs()
        {
            var allDishes = await dishService.AllAsync();
            return Ok(allDishes);
        }
        [HttpGet("{dishName}")]
        public async Task<IActionResult> GetDishByName(string dishName)
        {
            var dish = await dishService.GetByNameAsync(dishName);

            if (dish == null)
            {
                return NotFound("Dish not found!");
            }

            return Ok(dishName);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPost]
        public async Task<IActionResult> PostDish([FromBody] POSTDishDto dishDto)
        {
            if (!await categoryService.ExistsByIdAsync(dishDto.CategoryId))
            {
                return NotFound("Category not found!");
            }
            string responseMessage = await dishService.AddAsync(dishDto);
            return Ok(responseMessage);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(PUTDishDto dish, int id)
        {
            if (!await dishService.ExistsByIdAsync(id))
            {
                return NotFound("Dish not found!");
            }
            return Ok(await dishService.UpdateAsync(dish, id));
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDish(int id)
        {
            if (!await dishService.ExistsByIdAsync(id))
            {
                return NotFound("Dish not found!");
            }

            return Ok(await dishService.Remove(id));
        }
    }
}

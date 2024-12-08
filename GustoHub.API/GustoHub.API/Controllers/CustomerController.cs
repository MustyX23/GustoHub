﻿namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Services.Services;
    using GustoHub.Infrastructure.Attributes;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var allCustomers = await customerService.AllAsync();
            return Ok(allCustomers);
        }
        [HttpGet("{customerName}")]
        public async Task<IActionResult> GetCustomerByName(string customerName)
        {            
            var customer
                = await customerService.GetByNameAsync(customerName);

            if (customer == null)
            {
                return NotFound("Cutomer not found!");
            }

            return Ok(customerName);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] POSTCustomerDto customerDto)
        {
            string responseMessage = await customerService.AddAsync(customerDto);
            return Ok(responseMessage);
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCusomer(PUTCustomerDto customer, string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Cutomer not found!");
            }

            return Ok(await customerService.UpdateAsync(customer, id));
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Cutomer not found!");
            }

            return Ok(await customerService.Remove(Guid.Parse(id)));
        }
    }
}

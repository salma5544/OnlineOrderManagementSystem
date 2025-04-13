using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Data;
using OnlineOrderManagementSystem.DTOs;
using OnlineOrderManagementSystem.Models;

namespace OnlineOrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly OrderManagementDbContext _context;
        public CustomersController(OrderManagementDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Customers.AsNoTracking().ToListAsync());

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound("Customer not found");
            return Ok(customer);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

       
    }
}

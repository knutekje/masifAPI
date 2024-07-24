using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using masifAPI.Data;
using masifAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace masifAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly MasifContext _context;

        public FoodItemController(MasifContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodItem>>> GetFoodItems()
        {
            return await _context.FoodItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItem>> GetFoodItem(long id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(long id, FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<FoodItem>> PostFoodItem(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodItem", new { id = foodItem.Id }, foodItem);
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(long id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(long id)
        {
            return _context.FoodItems.Any(e => e.Id == id);
        }
    }
}

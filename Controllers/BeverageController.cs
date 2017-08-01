using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BeverageAPI.Models;
using System.Linq;

namespace BeverageAPI.Controllers
{
    [Route("api/beverages")]
    public class BeverageController : Controller
    {
        private readonly BeverageContext _context;

        public BeverageController(BeverageContext context)
        {
            _context = context;

            if (_context.BeverageItems.Count() == 0)
            {
                _context.BeverageItems.Add(new BeverageItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<BeverageItem> GetAll()
        {
            return _context.BeverageItems.ToList();
        }

        [HttpGet("{id}", Name = "GetBeverage")]
        public IActionResult GetById(long id)
        {
            var item = _context.BeverageItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BeverageItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.BeverageItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBeverage", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BeverageItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var Beverage = _context.BeverageItems.FirstOrDefault(t => t.Id == id);
            if (Beverage == null)
            {
                return NotFound();
            }

            Beverage.inStock = item.inStock;
            Beverage.Name = item.Name;
            Beverage.Description = item.Description;

            _context.BeverageItems.Update(Beverage);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var Beverage = _context.BeverageItems.First(t => t.Id == id);
            if (Beverage == null)
            {
                return NotFound();
            }

            _context.BeverageItems.Remove(Beverage);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}

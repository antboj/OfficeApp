using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/Office")]
    public class OfficeController : Controller
    {
        private static OfficeContext _context;

        public OfficeController(OfficeContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allOffices = _context.Offices;

            var foundOffice = allOffices.Select(n => new
                { Id = n.Id, Name = n.Description, Persons = n.Persons.Select(y => y.FirstName + " " + y.LastName) });

            if (foundOffice.Any())
            {
                return Ok(foundOffice.ToList());
            }

            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allOffices = _context.Offices;

            var foundOffice = allOffices.Where(x => x.Id == id).Select(n => new
                {Id = n.Id, Name = n.Description, Persons = n.Persons.Select(y => y.FirstName + " " + y.LastName)});

            if (foundOffice != null)
            {
                return Ok(foundOffice);
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Office input)
        {
            if (input != null)
            {
                _context.Offices.Add(input);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Office input)
        {
            var foundOffice = _context.Offices.Find(id);

            if (foundOffice != null)
            {
                foundOffice.Description = input.Description;
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var office = _context.Offices.Find(id);

            if (office != null)
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();
                return Ok(office);
            }

            return NotFound();
        }
    }
}

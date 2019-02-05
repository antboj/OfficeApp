using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

namespace OfficeApp.Controllers
{
    [Route("api/Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static OfficeContext _context;

        public PersonController(OfficeContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var allPersons = _context.Persons;

            if (allPersons.Any())
            {
                return Ok(allPersons);
            }

            return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allPersons = _context.Persons;

            var foundOffice = allPersons.Where(x => x.Id == id);

            if (allPersons != null)
            {
                return Ok(allPersons);
            }

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Person input)
        {
            if (input != null)
            {
                _context.Persons.Add(input);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Person input)
        {
            var all = _context.Persons.Include(o => o.Office);
            var officeKey = all.Select(x => x.OfficeId).ToList();
            var foundPerson = _context.Persons.Find(id);

            if (foundPerson != null)
            {
                foundPerson.FirstName = input.FirstName;
                foundPerson.LastName = input.LastName;
                if (foundPerson.OfficeId != 0)
                {
                    foundPerson.OfficeId = input.OfficeId;
                }
                else
                {
                    foundPerson.OfficeId = officeKey[0];
                }
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _context.Persons.Find(id);

            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
                return Ok(person);
            }

            return NotFound();
        }
    }

}

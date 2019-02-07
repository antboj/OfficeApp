using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Dto;
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
        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var allPersons = _context.Persons;

            var query = allPersons.Select(x => new
                {Name = x.FirstName + " " + x.LastName, Office = x.Office.Description, Devices = x.Devices.Select(y => y.Name)});

            if (query.Any())
            {
                return Ok(query);
            }

            return NotFound();
        }

        // GET api/values/5
        /// <summary>
        /// Return person by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allPersons = _context.Persons;

            var foundPerson = allPersons.Where(x => x.Id == id).Select(x => new
                { Name = x.FirstName + " " + x.LastName, Office = x.Office.Description, Devices = x.Devices.Select(y => y.Name) });

            if (foundPerson != null)
            {
                return Ok(foundPerson);
            }

            return NotFound();
        }

        // POST api/values
        /// <summary>
        /// Insert new person
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public IActionResult Post(PersonDto input)
        {
            if (input != null)
            {
                var person = new Person
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    OfficeId = input.OfficeId,
                };
                _context.Persons.Add(person);
                _context.SaveChanges();

                var lastPerson = _context.Persons.Last();
                var LastPersonOffice = lastPerson.OfficeId;
                
                var officeName = _context.Offices.Where(o => o.Id == LastPersonOffice).FirstOrDefault();

                var lista = officeName.Persons;
                lista.Add(person);



                return Ok();
            }

            return BadRequest();
        }

        // PUT api/values/5
        /// <summary>
        /// Update person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, PersonDto input)
        {
            var all = _context.Persons.Include(o => o.Office);
            var officeKey = all.Where(x => x.Id == id).Select(c => c.OfficeId).FirstOrDefault();
            var foundPerson = _context.Persons.Find(id);

            if (foundPerson != null)
            {
                foundPerson.FirstName = input.FirstName;
                foundPerson.LastName = input.LastName;
                if (input.OfficeId != 0)
                {
                    foundPerson.OfficeId = input.OfficeId;
                }
                else
                {
                    foundPerson.OfficeId = officeKey;
                }
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/values/5
        /// <summary>
        /// Delete person by Id
        /// </summary>
        /// <param name="id"></param>
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

        // GET api/values/5
        /// <summary>
        /// Get all persons from same office
        /// </summary>
        /// <param name="officeName"></param>
        [HttpGet("GetByOffice{officeName}")]
        public IActionResult GetByOffice(string officeName)
        {
            var allPersons = _context.Persons;

            var query = allPersons.Where(o => o.Office.Description == officeName).GroupBy(x => x.Office.Description)
                .Select(y => new {Office = y.Key, Persons = y.Select(p => p.FirstName + " " + p.LastName)});

            if (query != null)
            {
                return Ok(query);
            }

            return NotFound();
        }

    }

}

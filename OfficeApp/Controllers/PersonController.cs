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
    public class PersonController : BaseController<Person>
    {
        //private static OfficeContext _context;

        /// <inheritdoc />
        public PersonController(OfficeContext context) : base(context)
        {
        }

        /*
        // GET api/values
        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        */

        /*
        // GET api/values/5
        /// <summary>
        /// Return person by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        */

        
        // POST api/values
        /// <inheritdoc />
        /// <summary>
        /// Insert new person
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public override IActionResult Post(Person input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input != null)
                    {
                        // Add new person
                        var person = new Person
                        {
                            FirstName = input.FirstName,
                            LastName = input.LastName,
                            OfficeId = input.OfficeId,
                        };
                        _context.Persons.Add(person);
                        _context.SaveChanges();

                        // Find last added person ofice id
                        var lastPerson = _context.Persons.Last();
                        var lastPersonOffice = lastPerson.OfficeId;
                
                        // Find office where is last added person
                        var officeName = _context.Offices.FirstOrDefault(o => o.Id == lastPersonOffice);

                        // Add person into office list
                        var personList = officeName.Persons;
                        personList.Add(person);
                        transaction.Commit();

                        return Ok();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }
        
        // PUT api/values/5
        /// <inheritdoc />
        /// <summary>
        /// Update person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public override IActionResult Put(int id, Person input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var all = _context.Persons.Include(o => o.Office);
                    // Find office id for person
                    var officeKey = all.Where(x => x.Id == id).Select(c => c.OfficeId).FirstOrDefault();
                    // Find person
                    var foundPerson = _context.Persons.Find(id);

                    if (foundPerson != null)
                    {
                        foundPerson.FirstName = input.FirstName;
                        foundPerson.LastName = input.LastName;
                        if (input.OfficeId != 0)
                        {
                            // Change office if specified
                            foundPerson.OfficeId = input.OfficeId;
                        }
                        else
                        {
                            // Keep the same ofice id if not specified
                            foundPerson.OfficeId = officeKey;
                        }
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }

        /*
        // DELETE api/values/5
        /// <summary>
        /// Delete person by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var person = _context.Persons.Find(id);

            if (person != null)
            {
                try
                {
                    _context.Persons.Remove(person);
                    _context.SaveChanges();
                    return Ok(person);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
        */

        // GET api/values/5
        /// <summary>
        /// Get all persons from same office
        /// </summary>
        /// <param name="officeName"></param>
        [HttpGet("GetByOffice{officeName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Dto;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/Office")]
    public class OfficeController : BaseController<Office>
    {
        /// <inheritdoc />
        public OfficeController(OfficeContext context) : base(context)
        {
        }

        /*
        // GET api/values/5
        /// <summary>
        /// Return all offices
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var allOffices = _context.Offices;

            var foundOffice = allOffices.Select(n => new
                { Name = n.Description, Persons = n.Persons.Select(y => y.FirstName + " " + y.LastName) });

            if (foundOffice.Any())
            {
                return Ok(foundOffice.ToList());
            }

            return NotFound();
        }
        */

        /*
        // GET api/values/5
        /// <summary>
        /// Return office by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var allOffices = _context.Offices;

            var foundOffice = allOffices.Where(x => x.Id == id).Select(n => new
                { Name = n.Description, Persons = n.Persons.Select(y => y.FirstName + " " + y.LastName)});

            if (foundOffice != null)
            {
                return Ok(foundOffice);
            }

            return NotFound();
        }
        */

        /*
        // POST api/<controller>
        /// <summary>
        /// Insert new office
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post(Office input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input != null)
                    {
                        _context.Offices.Add(input);
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }
        */

        /*
        // PUT api/<controller>/5
        /// <summary>
        /// Update office
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, OfficeDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var foundOffice = _context.Offices.Find(id);

                    if (foundOffice != null)
                    {
                        foundOffice.Description = input.Description;
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
        */

        /*
        // DELETE api/<controller>/5
        /// <summary>
        /// Delete office by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var office = _context.Offices.Find(id);

            if (office != null)
            {
                try
                {
                    _context.Offices.Remove(office);
                    _context.SaveChanges();
                    return Ok(office);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/Usage")]
    public class UsageController : Controller
    {
        private static OfficeContext _context;

        public UsageController(OfficeContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allUsages = _context.Usages;

            var query = allUsages.Select(s => new
            {
                Name = s.Person.FirstName + " " + s.Person.LastName,
                Device = s.Device.Name,
                Start = s.UsedFrom,
                End = s.UsedTo
            });

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allUsages = _context.Usages;

            var query = allUsages.Where(x => x.Person.Id == id).Select(s => new
            {
                Name = s.Person.FirstName + " " + s.Person.LastName,
                Device = s.Device.Name,
                Start = s.UsedFrom,
                End = s.UsedTo
            });

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

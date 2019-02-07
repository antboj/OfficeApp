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

        // GET api/values/5
        /// <summary>
        /// Return all usages
        /// </summary>
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

        // GET api/values/5
        /// <summary>
        /// Return usage by person
        /// </summary>
        /// <param name="id"></param>
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

        // GET api/values/5
        /// <summary>
        /// Return usage by device
        /// </summary>
        /// <param name="deviceId"></param>
        [HttpGet("AllByDevice/{deviceId}")]
        public IActionResult AllByDevice(int deviceId)
        {
            var allUsages = _context.Usages;

            var query = allUsages.Where(d => d.DeviceId == deviceId).Select(x => new
            {
                Name = x.Person.FirstName + " " + x.Person.LastName, Device = x.Device.Name, UsedFrom = x.UsedFrom,
                UsedTo = x.UsedTo
            });

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }

        // GET api/values/5
        /// <summary>
        /// Return all usages by person
        /// </summary>
        /// <param name="personId"></param>
        [HttpGet("AllByPerson/{personId}")]
        public IActionResult AllByPerson(int personId)
        {
            var allUsages = _context.Usages;

            var query = allUsages.Where(p => p.PersonId == personId).GroupBy(d => d.Device.Name).Select(x =>
                 new {Device = x.Key, Usages = x.Select(y => new {UsedFrom = y.UsedFrom, UsedTo = y.UsedTo}).OrderBy(d => d.UsedFrom)});

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

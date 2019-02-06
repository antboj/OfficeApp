using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeApp.Dto;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/Device")]
    public class DeviceController : Controller
    {
        private static OfficeContext _context;

        public DeviceController(OfficeContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allDevices = _context.Devices;

            var query = allDevices.Select(x => new
            {
                Id = x.Id, Name = x.Name, PersonId = x.PersonId, Person = x.Person.FirstName + " " + x.Person.LastName
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
            var allDevices = _context.Devices;

            var foundDevice = allDevices.Where(x => x.Id == id);

            if (foundDevice != null)
            {
                return Ok(foundDevice);
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]DeviceDto input)
        {
            if (input != null)
            {
                var device = new Device
                {
                    Name = input.Name,
                    PersonId = input.PersonId
                };
                _context.Devices.Add(device);
                _context.SaveChanges();

                var person = _context.Persons.FirstOrDefault(x => x.Id == device.PersonId);

                var lista = person.Devices;
                lista.Add(device);
                

                var usage = new Usage
                {
                    PersonId = person.Id,
                    DeviceId = device.Id,
                    UsedFrom = DateTime.Now
                };

                _context.Usages.Add(usage);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        // PUT api/<controller>/5
        [HttpPut("ChangeDeviceUser/{userId}/{deviceId}")]
        public IActionResult ChangeDeviceUser(int deviceId, int userId)
        {
            var foundDevice = _context.Devices.Find(deviceId);

            if (foundDevice != null)
            {
                foundDevice.PersonId = userId;
                _context.SaveChanges();

                var usageRecord = _context.Usages.Where(u => u.DeviceId == deviceId && u.UsedTo == null).FirstOrDefault();

                usageRecord.UsedTo = DateTime.Now;

                _context.SaveChanges();

                var newUsageRecord = new Usage
                {
                    PersonId = userId,
                    DeviceId = deviceId,
                    UsedFrom = DateTime.Now

                };

                _context.Usages.Add(newUsageRecord);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var device = _context.Devices.Find(id);

            if (device != null)
            {
                _context.Devices.Remove(device);
                _context.SaveChanges();
                return Ok(device);
            }

            return NotFound();
        }
    }
}

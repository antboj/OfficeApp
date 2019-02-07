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

        // GET api/values/5
        /// <summary>
        /// Return all devices
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var allDevices = _context.Devices;

            var query = allDevices.Select(x => new
            {
                Id = x.Id, Name = x.Name, Person = x.Person.FirstName + " " + x.Person.LastName
            });

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }

        // GET api/values/5
        /// <summary>
        /// Return device by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var allDevices = _context.Devices;

            var foundDevice = allDevices.Where(x => x.Id == id).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Person = x.Person.FirstName + " " + x.Person.LastName
            });

            if (foundDevice != null)
            {
                return Ok(foundDevice);
            }

            return NotFound();
        }

        // POST api/<controller>
        /// <summary>
        /// Insert new device
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public IActionResult Post([FromBody]DeviceDto input)
        {
            if (input != null)
            {
                var device = new Device
                {
                    Name = input.Name,
                };
                _context.Devices.Add(device);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Use new device by person
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deviceId"></param>
        [HttpPut("UseDevice/{userId}/{deviceId}")]
        public IActionResult UseDevice(int userId, int deviceId)
        {
            var foundDevice = _context.Devices.Find(deviceId);

            var isCurrentlyUsed = _context.Usages
                .Where(x => x.DeviceId == deviceId && x.UsedTo == null).Any();

            if (foundDevice != null && !isCurrentlyUsed)
            {
                foundDevice.PersonId = userId;
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
        /// <summary>
        /// Change which person use device
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deviceId"></param>
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
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceId"></param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, string deviceName)
        {
            var foundDevice = _context.Devices.Find(id);

            if (foundDevice != null)
            {
                foundDevice.Name = deviceName;
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete device by ID
        /// </summary>
        /// <param name="id"></param>
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

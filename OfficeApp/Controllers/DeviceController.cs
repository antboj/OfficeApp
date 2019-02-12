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
    public class DeviceController : BaseController<Device>
    {
        //private static OfficeContext _context;

        public DeviceController(OfficeContext context) : base(context)
        {
        }

        /*
        // GET api/values/5
        /// <summary>
        /// Return all devices
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var allDevices = _context.Devices;

            var query = allDevices.Select(x => new
            {
                Name = x.Name, Using = x.Person.FirstName + " " + x.Person.LastName
            });

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }
        */
        /*
        // GET api/values/5
        /// <summary>
        /// Return device by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var allDevices = _context.Devices;

            var foundDevice = allDevices.Where(x => x.Id == id).Select(x => new
            {
                Name = x.Name,
                Using = x.Person.FirstName + " " + x.Person.LastName
            });

            if (foundDevice != null)
            {
                return Ok(foundDevice);
            }

            return NotFound();
        }
        */
        /*
        // POST api/<controller>
        /// <summary>
        /// Insert new device
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post(DevicePostDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input != null)
                    {
                        var device = new Device
                        {
                            Name = input.Name,
                        };
                        _context.Devices.Add(device);
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
        // PUT api/<controller>/5
        /// <summary>
        /// Use new device by person
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="deviceId"></param>
        [HttpPut("UseDevice/{personId}/{deviceId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UseDevice(int personId, int deviceId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var foundDevice = _context.Devices.Find(deviceId);

                    // Check if device is currently used by any person
                    var isCurrentlyUsed = _context.Usages
                        .Where(x => x.DeviceId == deviceId && x.UsedTo == null).Any();

                    if (foundDevice != null && !isCurrentlyUsed)
                    {
                        // Assign user to device
                        foundDevice.PersonId = personId;
                        _context.SaveChanges();

                        // Make new record of using device
                        var newUsageRecord = new Usage
                        {
                            PersonId = personId,
                            DeviceId = deviceId,
                            UsedFrom = DateTime.Now
                        };

                        _context.Usages.Add(newUsageRecord);
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

        // PUT api/<controller>/5
        /// <summary>
        /// Change which person use device
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="deviceId"></param>
        [HttpPut("ChangeDeviceUser/{personId}/{deviceId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult ChangeDeviceUser(int personId, int deviceId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var foundDevice = _context.Devices.Find(deviceId);

                    if (foundDevice != null && foundDevice.PersonId != personId)
                    {
                        // Change person using device
                        foundDevice.PersonId = personId;
                        _context.SaveChanges();

                        // Find who is using device currently
                        var usageRecord = _context.Usages.Where(u => u.DeviceId == deviceId && u.UsedTo == null).FirstOrDefault();
                
                        // Stop using device
                        usageRecord.UsedTo = DateTime.Now;

                        _context.SaveChanges();

                        // Make new record for new person
                        var newUsageRecord = new Usage
                        {
                            PersonId = personId,
                            DeviceId = deviceId,
                            UsedFrom = DateTime.Now
                        };

                        _context.Usages.Add(newUsageRecord);
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
        /*
        // PUT api/<controller>/5
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceName"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, string deviceName)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var foundDevice = _context.Devices.Find(id);

                    if (foundDevice != null)
                    {
                        foundDevice.Name = deviceName;
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
        /// Delete device by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var device = _context.Devices.Find(id);

            if (device != null)
            {
                try
                {
                    _context.Devices.Remove(device);
                    _context.SaveChanges();
                    return Ok(device);
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

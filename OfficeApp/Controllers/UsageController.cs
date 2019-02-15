using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using OfficeApp.Dto;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/Usage")]
    public class UsageController : Controller
    {
        private static OfficeContext _context;
        private IMapper _mapper;

        public UsageController(OfficeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET api/values/5
        /// <summary>
        /// Return all usages
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var allUsages = _context.Usages;

            //var query = allUsages.Select(s => new
            //{
            //    Name = s.Person.FirstName + " " + s.Person.LastName,
            //    Device = s.Device.Name,
            //    Start = s.UsedFrom,
            //    End = s.UsedTo
            //});
            
            var query = allUsages.ProjectTo<UsageDtoGet>(_mapper.ConfigurationProvider);
            
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
        /// <param name="personId"></param>
        [HttpGet("{personId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int personId)
        {
            var allUsages = _context.Usages;

            //var query = allUsages.Where(x => x.Person.Id == personId).Select(s => new
            //{
            //    Name = s.Person.FirstName + " " + s.Person.LastName,
            //    Device = s.Device.Name,
            //    Start = s.UsedFrom,
            //    End = s.UsedTo
            //});

            var query = allUsages.Where(u => u.PersonId == personId).ProjectTo<UsageDtoGet>(_mapper.ConfigurationProvider);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AllByDevice(int deviceId)
        {
            var allUsages = _context.Usages;

            //var query = allUsages.Where(d => d.DeviceId == deviceId).Select(x => new
            //{
            //    Name = x.Person.FirstName + " " + x.Person.LastName, Device = x.Device.Name, UsedFrom = x.UsedFrom,
            //    UsedTo = x.UsedTo
            //});

            var query = allUsages.Where(d => d.DeviceId == deviceId)
                .ProjectTo<UsageDtoGet>(_mapper.ConfigurationProvider);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult AllByPerson(int personId)
        {
            var allUsages = _context.Usages;

            //var query = allUsages.Where(p => p.PersonId == personId).GroupBy(d => d.Device.Name).Select(x =>
            //     new { Device = x.Key, Usages = x.Select(y => new { UsedFrom = y.UsedFrom, UsedTo = y.UsedTo }).OrderBy(d => d.UsedFrom) });

            //var query = allUsages.Where(p => p.PersonId == personId)
            //    .ProjectTo<UsageAllByPersonDtoGet>(_mapper.ConfigurationProvider).OrderBy(x => x.Device);

            var query = allUsages.Where(p => p.PersonId == personId).GroupBy(d => d.Device.Name)
                .Select(x => new UsageAllByPersonDtoGet{Device = x.Key, Usages = x.Select(y => new UsageTimeDtoGet{UsedFrom = y.UsedFrom, UsedTo = y.UsedTo})});

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
        [HttpGet("TimeUsedByPerson/{personId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult TimeUsedByPerson(int personId)
        {
            var allUsages = _context.Usages;

            //var query = allUsages.Where(p => p.PersonId == personId && p.UsedTo != null).GroupBy(x => x.Device.Name)
            //    .Select(y => new
            //    {
            //        Device = y.Key,
            //        TimeUsed =
            //            new TimeSpan(y.Sum(u => u.UsedTo.Value.Ticks - u.UsedFrom.Ticks)).ToString(@"dd\.hh\:mm\:ss")
            //    });

            //var query = allUsages.Where(p => p.PersonId == personId && p.UsedTo != null).ProjectTo<TimeUsedByPersonDtoGet>(_mapper.ConfigurationProvider);

            var query = allUsages.Where(p => p.PersonId == personId).GroupBy(x => x.Device.Name)
                .Select(y => new TimeUsedByPersonDtoGet{Device = y.Key,
                    TimeUsed = new TimeSpan(y.Sum(u => (u.UsedTo.Value != null ? u.UsedTo.Value.Ticks : DateTime.Now.Ticks) - u.UsedFrom.Ticks)).ToString(@"dd\.hh\:mm\:ss")});

            if (query.Any())
            {
                return Ok(query.ToList());
            }

            return NotFound();
        }
    }
}

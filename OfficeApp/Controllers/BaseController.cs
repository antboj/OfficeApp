using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Dto;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<Tentity, TDtoGet, TDtoPost, TDtoPut> : Controller where Tentity : class where TDtoGet : class where TDtoPost : class where TDtoPut : class
    {
        protected readonly OfficeContext _context;
        private readonly DbSet<Tentity> _dbSet;
        private readonly IMapper _mapper;

        protected BaseController(OfficeContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<Tentity>();
            _mapper = mapper;
        }

        // GET api/values/5
        /// <summary>
        /// Return all
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public virtual IActionResult Get()
        {
            var all = _dbSet.ProjectTo<TDtoGet>(_mapper.ConfigurationProvider);

            //var otr = _mapper.Map<IEnumerable<TDtoGet>>(all);

            if (all != null)
            {
                return Ok(all.ToList());
            }

            return NotFound();
        }

        // GET api/values/5
        /// <summary>
        /// Return by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public virtual IActionResult Get(int id)
        {
            var found = _dbSet.Find(id);

            var otr = _mapper.Map<TDtoGet>(found);

            if (otr != null)
            {
                return Ok(otr);
            }

            return NotFound();
        }

        // POST api/<controller>
        /// <summary>
        /// Insert new
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public virtual IActionResult Post(TDtoPost input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var otr = _mapper.Map<Tentity>(input);

                    _dbSet.Add(otr);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public virtual IActionResult Put(int id, TDtoPut input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var found = _dbSet.Find(id);
                    _mapper.Map<TDtoPut, Tentity>(input, found);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.ToString());
                }
            }

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete by ID
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual IActionResult Delete(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var found = _dbSet.Find(id);

                if (found != null)
                {
                    try
                    {
                        _dbSet.Remove(found);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }

                return NotFound();
            }
        }
    }
}
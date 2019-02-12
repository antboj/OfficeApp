using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfficeApp.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller where T : class
    {
        protected readonly OfficeContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseController(OfficeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
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
            var all = _dbSet.Select(x => x);

            if (all != null)
            {
                return Ok(all);
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

            if (found != null)
            {
                return Ok(found);
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
        public virtual IActionResult Post(T input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbSet.Add(input);
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
        public virtual IActionResult Put(int id, T input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var updated = _context.Attach(input).Entity;
                    _context.Entry(updated).State = EntityState.Modified;
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
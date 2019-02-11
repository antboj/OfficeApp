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
        private readonly DbContext _context;
        protected DbSet<T> _dbSet;

        protected BaseController(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var found = _dbSet.Find(id);

            return Ok(found);
        }

        // GET api/values/5
        [HttpGet]
        public virtual IActionResult Get()
        {
            var all = _dbSet.Select(x => x);

            return Ok(all.ToList());
        }

        // POST api/<controller>
        [HttpPost]
        public virtual IActionResult Post(T input)
        {
            _dbSet.Add(input);
            _context.SaveChanges();
            return Ok();
        }
        /*
        // PUT api/<controller>/5
        [HttpPut]
        public virtual void Put(int id, T input)
        {
            

        }
        */
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            var found = _dbSet.Find(id);

            if (found != null)
            {
                try
                {
                    _dbSet.Remove(found);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public IList<Person> Persons { get; set; }
    }
}

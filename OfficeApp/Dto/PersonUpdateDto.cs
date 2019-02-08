using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeApp.Models;

namespace OfficeApp.Dto
{
    public class PersonUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OfficeId { get; set; }
    }
}

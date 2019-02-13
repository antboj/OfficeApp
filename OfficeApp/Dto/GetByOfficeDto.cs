using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeApp.Models;

namespace OfficeApp.Dto
{
    public class GetByOfficeDto
    {
        public string OfficeName { get; set; }
        public List<Person> Persons { get; set; }
    }
}

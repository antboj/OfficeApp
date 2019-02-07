using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeApp.Models;

namespace OfficeApp.Dto
{
    public class DeviceDto
    {
        //public int Id { get; set; }
        
        public string Name { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}

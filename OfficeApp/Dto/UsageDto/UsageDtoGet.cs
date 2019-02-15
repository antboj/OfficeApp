using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Dto
{
    public class UsageDtoGet
    {
        public string Person { get; set; }
        public string Device { get; set; }
        public DateTime UsedFrom { get; set; }
        public DateTime? UsedTo { get; set; }
    }
}

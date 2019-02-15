using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Dto
{
    public class UsageAllByPersonDtoGet
    {
        public string Device { get; set; }
        //public DateTime UsedFrom { get; set; }
        //public DateTime UsedTo { get; set; }
        public IEnumerable<UsageTimeDtoGet> Usages { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Dto
{
    public class TimeUsedByPersonDtoGet
    {
        public string Device { get; set; }
        public string TimeUsed { get; set; }
        //public IList<UsageTimeDtoGet> Usages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeApp.Models
{
    public class Usage
    {
        public int Id { get; set; }
        
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        public DateTime UsedFrom { get; set; }
        public DateTime? UsedTo { get; set; }


    }
}

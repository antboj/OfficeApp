using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OfficeApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public Office Office { get; set; }

        public IList<Usage> Devices { get; set; }
    }
}

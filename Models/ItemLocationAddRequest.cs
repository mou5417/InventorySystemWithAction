using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class ItemLocationAddRequest
    {


        public Guid LocationId { get; set; }

        public string? LocationName { get; set; }

        public string? LocationDetail { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}

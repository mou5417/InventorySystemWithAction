using Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Entities
{
    public class ItemLocation 
    {
        
        [Key]
        public Guid LocationId { get; set; }
        [StringLength(40)]
        [Required]
        public string? LocationName { get; set; }
        [StringLength(200)]
        public string? LocationDetail { get; set; }

        public virtual List<Item> Items { get; set; } = new List<Item>();


    }
}

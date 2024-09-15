using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Database.API.Models
{

    public class User:IdentityUser<Guid>
    {
      
       
        [Required]
        public int RoleLevel { get; set; }

        [Required]
        public string PersonName { get; set; }
        [Required]
        public string Department { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
    }
}

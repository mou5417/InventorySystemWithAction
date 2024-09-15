using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Entities
{

    public class User :IdentityUser<Guid>
    {

       
        [Required]
        public int RoleLevel { get; set; } 
        public string Department { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
    }
}

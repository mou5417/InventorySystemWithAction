using Microsoft.EntityFrameworkCore;
using Entities.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Database.API.Models
{
    public class Item 
    {
      
  
        [Required]
        [Key]
        public Guid ItemId { get; set; }
        [Required(ErrorMessage ="名稱不可空白")]
        [StringLength(40)]
        public string ItemName { get; set; }
        public int PurchaseId { get; set; }
       

        [Required(ErrorMessage = "所在位置不可空白")]
        [StringLength(40)]
        public string LocationName {  get; set; }

        public Guid ItemLocationId { get; set; }

        [ForeignKey("ItemLocationId")]
        public virtual ItemLocation ItemLocation { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
       
        public Guid CreateUserId { get; set; }
            
        [ForeignKey("CreateUserId")]
        public virtual User CreateUser { get; set; } 
        public string Status { get; set; }

      

    }
   
    
   
}

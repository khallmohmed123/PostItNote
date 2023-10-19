using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostItNote.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string user_id { get; set; }
        [ForeignKey("user_id")]
        [ValidateNever]
        public IdentityUser User { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

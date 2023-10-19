using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostItNote.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
        public string? User_id { get; set;}
        [ForeignKey("User_id")]
        [ValidateNever]
        public IdentityUser User { get; set; }
        public int? Category_id { get; set; }
        [ForeignKey("Category_id")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}

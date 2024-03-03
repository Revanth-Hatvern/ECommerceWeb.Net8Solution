using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Net8.Models
{
    public class Category
    {
        [Key]
        public int C_Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="The value must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}

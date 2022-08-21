using System.ComponentModel.DataAnnotations;

namespace CatalogMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [StringLength(50, ErrorMessage = "Длинна {0} должна быть более {2} и не больше {1}.", MinimumLength = 2)]
        [Display(Name="Название")]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}

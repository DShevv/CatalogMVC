using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Наименование продукта")]
        [Required(ErrorMessage = "Поле пусто")]
        [StringLength(50, ErrorMessage = "Длинна {0} должна быть более {2} и не больше {1}.", MinimumLength = 2)]
        public string? Name { get; set; }
        [ForeignKey("Category")]
        [Display(Name="Категория")]
        [Required(ErrorMessage = "Выберите категорию")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required(ErrorMessage = "Описание пусто")]
        [Display(Name="Описание")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name="Стоимость в рублях")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Примечание пусто")]
        [Display(Name="Примечание общее")]
        public string? PublicNote { get; set; }
        [Required(ErrorMessage = "Примечание пусто")]
        [Display(Name= "Примечание специальное")]
        public string? PrivateNote { get; set; }

    }
}

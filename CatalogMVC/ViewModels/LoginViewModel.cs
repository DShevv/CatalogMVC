using System.ComponentModel.DataAnnotations;

namespace CatalogMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password, ErrorMessage ="Неверный пароль")]
        public string Password { get; set; }
        public bool PasswordIsCorrect { get; set; }
    }
}

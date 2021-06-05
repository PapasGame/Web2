using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(35, ErrorMessage = "Превышает 35 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не email")]
        [StringLength(35, ErrorMessage = "Превышает 35 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [StringLength(35, ErrorMessage = "Превышает 35 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        
    }
}
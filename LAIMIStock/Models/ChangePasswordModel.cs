using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LAIMIStock.Models
{
    public class ChangePasswordModel
    {
        [Key]
        public int userID { get; set; }

        [Required(ErrorMessage = "El  campo de vieja contraseña es obligatorio")]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "El campo de nueva contraseña es obligatorio")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "El campo de confirmación de contraseña es obligatorio")]
        [Compare("newPassword", ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class User
    {
        public User()
        {
        }
        [Key]
        public int ID_User { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        public string Firdname { get; set; }

        [Required(ErrorMessage = "Не указан СНИЛС")]
        [MinLength(11, ErrorMessage = "в СНИЛС должно быть 11 символов")]
        [MaxLength(11, ErrorMessage = "в СНИЛС должно быть 11 символов")]
        public string Snils { get; set; }

        [Required(ErrorMessage = "Не указан ИНН")]
        [MinLength(11, ErrorMessage = "в ИНН должно быть 11 символов")]
        [MaxLength(11, ErrorMessage = "в ИНН должно быть 11 символов")]
        public string INN { get; set; }

        [Required(ErrorMessage = "Не указана серия паспорта")]
        [MinLength(4, ErrorMessage = "в серии паспорта должно быть 4 символов")]
        [MaxLength(4, ErrorMessage = "в серии паспорта должно быть 4 символов")]
        public string SeriaPass { get; set; }

        [Required(ErrorMessage = "Не указан номер паспорта")]
        [MinLength(6, ErrorMessage = "в номере паспорта должно быть 6 символов")]
        [MaxLength(6, ErrorMessage = "в номере паспорта должно быть 6 символов")]
        public string NumberPass { get; set; }
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public int ID_Role { get; set; }

    }
}

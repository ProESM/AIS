using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO
{
    /// <summary>
    /// Класс DTO для регистрации пользователя.
    /// </summary>
    [DataContract]
    [Serializable]
    public class RegistrationUserDto : BaseDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин пользователя")]
        [DataMember]
        [Required]
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Пароль пользователя")]
        [DataMember]
        [Required]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        [DataMember]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [Display(Name = "Телефон")]
        [DataMember]
        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Display(Name = "Фамилия")]
        [DataMember]
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Display(Name = "Имя")]
        [DataMember]
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [Display(Name = "Отчество")]
        [DataMember]
        [JsonProperty(PropertyName = "Patronymic")]
        public string Patronymic { get; set; }
        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataMember]
        [JsonProperty(PropertyName = "BirthDate")]
        public DateTime BirthDate { get; set; }        
    }
}

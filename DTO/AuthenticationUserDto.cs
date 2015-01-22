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
    /// Класс DTO для аутентификации пользователя.
    /// </summary>
    [DataContract]
    [Serializable]
    public class AuthenticationUserDto : BaseDto
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
    }
}

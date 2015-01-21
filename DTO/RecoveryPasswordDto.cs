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
    public class RecoveryPasswordDto: BaseDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Пароль пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
    }
}

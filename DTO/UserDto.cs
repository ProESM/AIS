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
    /// Класс DTO для хранения информации о пользователях.
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserDto : BaseTreeDto
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
        /// <summary>
        /// Соль для удлинения строки пароля
        /// </summary>
        [Display(Name = "Соль для удлинения строки пароля")]
        [DataMember]
        [JsonProperty(PropertyName = "Salt")]
        public string Salt { get; set; }
        /// <summary>
        /// Id группы пользователя, к которой относится пользователь
        /// </summary>
        [Display(Name = "Id группы пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "UserGroupId")]
        public Guid UserGroupId { get; set; }
        /// <summary>
        /// Группа пользователя, к которой относится пользователь
        /// </summary>
        [Display(Name = "Группа пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "UserGroupName")]
        public string UserGroupName { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        [DataMember]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Телефон")]
        [DataMember]
        [JsonProperty(PropertyName = "Phone")]        
        public string Phone { get; set; }

        /// <summary>
        /// Ссылка на человека, которому принадлежит данная учетная запись
        /// </summary>
        [Display(Name = "Id человека")]
        [DataMember]
        [JsonProperty(PropertyName = "PersonId")]
        public Guid? PersonId { get; set; }        
    } 
}

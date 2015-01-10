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
    /// Класс DTO для пользователя, расширенный данными о человеке.
    /// </summary>
    [DataContract]
    [Serializable]
    public class PersonalUserDto : BaseTreeDto
    {        
        /// <summary>
        /// Наименование состояния пользователя
        /// </summary>
        [Display(Name = "Наименование состояния пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "StateName")]
        public string StateName { get; set; } 
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }        
        /// <summary>
        /// Id группы пользователя, к которой относится пользователь
        /// </summary>
        [Display(Name = "Id группы пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "UserGroupId")]
        public Guid UserGroupId { get; set; }
        /// <summary>
        /// Наименование группы пользователя, к которой относится пользователь
        /// </summary>
        [Display(Name = "Наименование группы пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Login")]
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
        public Guid PersonId { get; set; }
        /// <summary>
        /// Фамилия человека
        /// </summary>
        [Display(Name = "Фамилия человека")]
        [DataMember]
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        /// <summary>
        /// Имя человека
        /// </summary>
        [Display(Name = "Имя человека")]
        [DataMember]
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество человека
        /// </summary>
        [Display(Name = "Отчество человека")]
        [DataMember]
        [JsonProperty(PropertyName = "Patronymic")]
        public string Patronymic { get; set; }
        /// <summary>
        /// День рождения человека
        /// </summary>
        [Display(Name = "День рождения человека")]
        [DataMember]
        [JsonProperty(PropertyName = "BirthDate")]
        public DateTime BirthDate { get; set; }
    }
}

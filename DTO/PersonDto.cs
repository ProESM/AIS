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
    /// Класс DTO для хранения информации о людях.
    /// </summary>
    [DataContract]
    [Serializable]
    public class PersonDto : BaseTreeDto
    {
        /// <summary>
        /// Фамилия человека
        /// </summary>
        [Display(Name = "Фамилия")]
        [DataMember]
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        /// <summary>
        /// Имя человека
        /// </summary>
        [Display(Name = "Имя")]
        [DataMember]
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество человека
        /// </summary>
        [Display(Name = "Отчество")]
        [DataMember]
        [JsonProperty(PropertyName = "Patronymic")]
        public string Patronymic { get; set; }
        /// <summary>
        /// День рождения человека
        /// </summary>
        [Display(Name = "День рождения")]
        [DataMember]
        [JsonProperty(PropertyName = "BirthDate")]
        public DateTime BirthDate { get; set; }
    }
}

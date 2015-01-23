using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO.Web
{
    /// <summary>
    /// Облегченный класс DTO для изменения типа отчетов через Web
    /// </summary>
    public class WebUpdateReportTypeDto : BaseDto
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Id группы типа отчетов
        /// </summary>
        [Display(Name = "Id группы типа отчетов")]
        [DataMember]
        [JsonProperty(PropertyName = "GroupId")]
        public Guid GroupId { get; set; }
    }
}

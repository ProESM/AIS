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
    /// Облегченный класс DTO для создания типа отчетов через Web
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebCreateReportTypeDto : BaseDto
    {
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

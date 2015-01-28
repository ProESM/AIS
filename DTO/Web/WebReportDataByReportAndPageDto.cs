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
    /// Облегченный класс DTO для задания критериев поиска данных в отчетах через Web
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebReportDataByReportAndPageDto : BaseDto
    {
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        [Display(Name = "Id отчета-владельца данных")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportId")]
        public Guid ReportId { get; set; }        
        /// <summary>
        /// Номер страницы
        /// </summary>
        [Display(Name = "Номер страницы")]
        [DataMember]
        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }        
    }
}

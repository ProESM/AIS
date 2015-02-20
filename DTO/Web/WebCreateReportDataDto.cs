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
    /// Облегченный класс DTO для добавления данных в отчет через Web
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebCreateReportDataDto : BaseDto
    {        
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        [Display(Name = "Id отчета-владельца данных")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportId")]
        public Guid ReportId { get; set; }
        /// <summary>
        /// Номер столбца
        /// </summary>
        [Display(Name = "Номер столбца")]
        [DataMember]
        [JsonProperty(PropertyName = "Column")]
        public int Column { get; set; }
        /// <summary>
        /// Номер строки
        /// </summary>
        [Display(Name = "Номер строки")]
        [DataMember]
        [JsonProperty(PropertyName = "Row")]
        public int Row { get; set; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        [Display(Name = "Номер страницы")]
        [DataMember]
        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }
        /// <summary>
        /// Значение в ячейке
        /// </summary>
        [Display(Name = "Значение в ячейке")]
        [DataMember]
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
}

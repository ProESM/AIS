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
    /// Класс DTO ячейки данных отчета.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportDataDto : BaseDto
    {
        /// <summary>
        /// Id ячейки с данными
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        [Display(Name = "ReportId")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportId")]
        public Guid ReportId { get; set; }                
        /// <summary>
        /// Номер столбца
        /// </summary>
        [Display(Name = "Column")]
        [DataMember]
        [JsonProperty(PropertyName = "Column")]
        public int Column { get; set; }
        /// <summary>
        /// Номер строки
        /// </summary>
        [Display(Name = "Row")]
        [DataMember]
        [JsonProperty(PropertyName = "Row")]
        public int Row { get; set; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        [Display(Name = "Page")]
        [DataMember]
        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }
        /// <summary>
        /// Значение в ячейке
        /// </summary>
        [Display(Name = "Value")]
        [DataMember]
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
}

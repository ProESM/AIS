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
    /// Облегченный класс DTO для создания отчета через Web
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebCreateReportDto : BaseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Id типа отчета
        /// </summary>
        [Display(Name = "Id типа отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportTypeId")]
        public Guid ReportTypeId { get; set; }
        /// <summary>
        /// Id адресата отчета
        /// </summary>
        [Display(Name = "Id адресата отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "RecipientId")]
        public Guid RecipientId { get; set; }
        /// <summary>
        /// Дата начала заполнения отчёта
        /// </summary>
        [Display(Name = "Дата начала заполнения отчёта")]
        [DataMember]
        [JsonProperty(PropertyName = "FillingDate")]
        public DateTime FillingDate { get; set; }
        /// <summary>
        /// Дата окончания заполнения
        /// </summary>
        [Display(Name = "Дата окончания заполнения")]
        [DataMember]
        [JsonProperty(PropertyName = "ExpiryFillingDate")]
        public DateTime ExpiryFillingDate { get; set; }
        /// <summary>
        /// Примечания
        /// </summary>
        [Display(Name = "Примечания")]
        [DataMember]
        [JsonProperty(PropertyName = "Notes")]
        public string Notes { get; set; }
    }
}

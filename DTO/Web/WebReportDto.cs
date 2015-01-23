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
    /// Web-класс DTO для хранения уточняющей информации о таком типе документов как отчет.
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebReportDto : ReportDto
    {
        /// <summary>
        /// Id последнего пользователя, которыл вносил изменения
        /// </summary>
        [Display(Name = "Id последнего пользователя, которыл вносил изменения")]
        [DataMember]
        [JsonProperty(PropertyName = "LastChangeUserId")]
        public Guid LastChangeUserId { get; set; }
        /// <summary>
        /// Имя последнего пользователя, которыл вносил изменения
        /// </summary>
        [Display(Name = "Имя последнего пользователя, которыл вносил изменения")]
        [DataMember]
        [JsonProperty(PropertyName = "LastChangeUserName")]
        public string LastChangeUserName { get; set; }
        /// <summary>
        /// Дата и время последнего изменения отчета
        /// </summary>
        [Display(Name = "Дата начала заполнения отчёта")]
        [DataMember]
        [JsonProperty(PropertyName = "LastChangeDateTime")]
        public DateTime LastChangeDateTime { get; set; }
    }
}

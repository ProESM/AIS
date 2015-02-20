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
    /// Класс DTO для хранения информации о типах отчетов.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportTypeDto : BaseTreeDto
    {
        /// <summary>
        /// Id группы типа отчетов
        /// </summary>
        [Display(Name = "Id группы типа отчетов")]
        [DataMember]
        [JsonProperty(PropertyName = "GroupId")]
        public Guid GroupId { get; set; }
        /// <summary>
        /// Группа типа отчетов
        /// </summary>
        [Display(Name = "Группа типа отчетов")]
        [DataMember]
        [JsonProperty(PropertyName = "GroupName")]
        public string GroupName { get; set; }   
    }
}

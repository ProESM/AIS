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
    /// Класс DTO для хранения уточняющей информации о таком типе документов как отчет.     
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportDto : DocumentDto
    {
        /// <summary>
        /// Id типа отчета
        /// </summary>
        [Display(Name = "Id типа отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportTypeId")]
        public Guid ReportTypeId { get; set; }
        /// <summary>
        /// Тип отчета
        /// </summary>
        [Display(Name = "Тип отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "ReportTypeName")]
        public string ReportTypeName { get; set; }
        /// <summary>
        /// Id адресата отчета
        /// </summary>
        [Display(Name = "Id адресата отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "RecipientId")]
        public Guid RecipientId { get; set; }
        /// <summary>
        /// Адресат отчета - это огранизация, подразделение или конкретный сотрудник, для
        /// которого предназначен документ (к заполнению, ознакомлению и т.п.)
        /// </summary>
        [Display(Name = "Адресат отчета")]
        [DataMember]
        [JsonProperty(PropertyName = "RecipientName")]
        public string RecipientName { get; set; }
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
    }

    public static class ReportDtoHelper
    {
        public static bool CompareFull(this ReportDto sourceDto, ReportDto targetDto)
        {            
            var isDifferent = (sourceDto.Name != targetDto.Name) ||
                          (sourceDto.ParentId != targetDto.ParentId) ||
                          (sourceDto.Name != targetDto.Name) ||
                          (sourceDto.StateId != targetDto.StateId) ||
                          (sourceDto.DocumentParentId != targetDto.DocumentParentId) ||
                          (sourceDto.DocumentTypeId != targetDto.DocumentTypeId) ||
                          (sourceDto.DocumentStateId != targetDto.DocumentStateId) ||
                          (sourceDto.DocumentUserId != targetDto.DocumentUserId) ||
                          (sourceDto.Notes != targetDto.Notes) ||
                          (sourceDto.ReportTypeId != targetDto.ReportTypeId) ||
                          (sourceDto.RecipientId != targetDto.RecipientId) ||
                          (sourceDto.FillingDate != targetDto.FillingDate) ||
                          (sourceDto.ExpiryFillingDate != targetDto.ExpiryFillingDate);

            return !isDifferent;
        }

        public static bool CompareDetails(this ReportDto sourceDto, ReportDto targetDto)
        {
            var isDifferent = (sourceDto.Name != targetDto.Name) ||
                          (sourceDto.ParentId != targetDto.ParentId) ||
                          (sourceDto.Name != targetDto.Name) ||
                          (sourceDto.StateId != targetDto.StateId) ||
                          (sourceDto.DocumentParentId != targetDto.DocumentParentId) ||
                          (sourceDto.DocumentTypeId != targetDto.DocumentTypeId) ||                          
                          (sourceDto.DocumentUserId != targetDto.DocumentUserId) ||
                          (sourceDto.Notes != targetDto.Notes) ||
                          (sourceDto.ReportTypeId != targetDto.ReportTypeId) ||
                          (sourceDto.RecipientId != targetDto.RecipientId) ||
                          (sourceDto.FillingDate != targetDto.FillingDate) ||
                          (sourceDto.ExpiryFillingDate != targetDto.ExpiryFillingDate);

            return !isDifferent;
        }

        public static bool CompareState(this ReportDto sourceDto, ReportDto targetDto)
        {
            var isDifferent = (sourceDto.DocumentStateId != targetDto.DocumentStateId);

            return !isDifferent;
        }
    }
}

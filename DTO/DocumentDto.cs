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
    /// Класс DTO для хранения общей информации о документе. 
    /// Более детальная информация хранится в уточняющих классах DTO документов определенных типов.
    /// </summary>
    [DataContract]
    [Serializable]
    public class DocumentDto : BaseTreeDto
    {
        /// <summary>
        /// Id родительского документа
        /// </summary>
        [Display(Name = "Id родительского документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentParentId")]
        public Guid? DocumentParentId { get; set; }
        /// <summary>
        /// Наименование родительского документа
        /// </summary>
        [Display(Name = "Наименование родительского документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentParentName")]
        public string DocumentParentName { get; set; }
        /// <summary>
        /// Id типа документа
        /// </summary>
        [Display(Name = "Id типа документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentTypeId")]
        public Guid DocumentTypeId { get; set; }
        /// <summary>
        /// Наименование типа документа
        /// </summary>
        [Display(Name = "Наименование типа документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentTypeName")]
        public string DocumentTypeName { get; set; }
        /// <summary>
        /// Id состояния документа
        /// </summary>
        [Display(Name = "Id состояния документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentStateId")]
        public Guid DocumentStateId { get; set; }
        /// <summary>
        /// Наименование состояния документа
        /// </summary>
        [Display(Name = "Наименование состояния документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentStateName")]
        public string DocumentStateName { get; set; }
        /// <summary>
        /// Id пользователя документа
        /// </summary>
        [Display(Name = "Id состояния документа")]
        [DataMember]
        [JsonProperty(PropertyName = "DocumentStateId")]
        public Guid DocumentUserId { get; set; }
        
        //public UserDao DocumentUser { get; set; }

        public string Notes { get; set; }
    }
}

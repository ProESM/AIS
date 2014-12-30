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
    /// Базовый класс DTO объекта общего дереве.
    /// </summary>
    [DataContract]
    [Serializable]
    public class BaseTreeDto : BaseDto
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Id родительского объекта
        /// </summary>
        [Display(Name = "Id родителя")]
        [DataMember]
        [JsonProperty(PropertyName = "ParentId")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Наименование объекта
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Короткое имя объекта
        /// </summary>
        [Display(Name = "Короткое имя")]
        [DataMember]
        [JsonProperty(PropertyName = "ShortName")]
        public string ShortName { get; set; }
        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Display(Name = "Состояние")]
        [DataMember]
        [JsonProperty(PropertyName = "StateId")]
        public Guid StateId { get; set; }
        /// <summary>
        /// ТИп объекта
        /// </summary>
        [Display(Name = "Тип объекта")]
        [DataMember]
        [JsonProperty(PropertyName = "TypeId")]
        public Guid TypeId { get; set; }
        /// <summary>
        /// Дата и время создание объекта
        /// </summary>
        [Display(Name = " Дата и время создание")]
        [DataMember]
        [JsonProperty(PropertyName = "CreateDateTime")]
        public DateTime CreateDateTime { get; set; }
    }
}

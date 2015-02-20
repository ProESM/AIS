using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DTO.TreeTypeDtos;
using Newtonsoft.Json;

namespace DTO
{
    /// <summary>
    /// Класс DTO для хранения уточняющей информации об образовательных учреждениях.
    /// </summary>
    [DataContract]
    [Serializable]
    public class InstituteDto : JuridicalPersonDto
    {
        /// <summary>
        /// Id населенного пункта, района, региона
        /// </summary>
        [Display(Name = "Id населенного пункта, района, региона")]
        [DataMember]
        [JsonProperty(PropertyName = "LocationId")]
        public Guid LocationId { get; set; }
        /// <summary>
        /// Населенный пункт, район, регион
        /// </summary>
        [Display(Name = "Населенный пункт, район, регион")]
        [DataMember]
        [JsonProperty(PropertyName = "LocationName")]
        public string LocationName { get; set; }
        /// <summary>
        /// Id уровня профессионального образования
        /// </summary>
        [Display(Name = "Id уровня профессионального образования")]
        [DataMember]
        [JsonProperty(PropertyName = "EducationLevelId")]
        public Guid EducationLevelId { get; set; }
        /// <summary>
        /// Уровень профессионального образования
        /// </summary>
        [Display(Name = "Уровень профессионального образования")]
        [DataMember]
        [JsonProperty(PropertyName = "EducationLevelName")]
        public string EducationLevelName { get; set; }
        /// <summary>
        /// Id типа населенного пункта (город, село)
        /// </summary>
        [Display(Name = "Id типа населенного пункта (город, село)")]
        [DataMember]
        [JsonProperty(PropertyName = "LocalityTypeId")]
        public Guid LocalityTypeId { get; set; }
        /// <summary>
        /// Тип населенного пункта (город, село)
        /// </summary>
        [Display(Name = "Тип населенного пункта (город, село)")]
        [DataMember]
        [JsonProperty(PropertyName = "LocalityTypeName")]
        public string LocalityTypeName { get; set; }
    }
}

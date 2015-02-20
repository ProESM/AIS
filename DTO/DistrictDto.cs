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
    /// Класс DTO для хранения информации о районах.
    /// </summary>
    [DataContract]
    [Serializable]
    public class DistrictDto : BaseTreeDto
    {
        /// <summary>
        /// Id региона
        /// </summary>
        [Display(Name = "Id регионав")]
        [DataMember]
        [JsonProperty(PropertyName = "RegionId")]
        public Guid RegionId { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        [Display(Name = "Регион")]
        [DataMember]
        [JsonProperty(PropertyName = "RegionName")]
        public string RegionName { get; set; }
    }
}

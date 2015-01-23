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
    public class VirtualTreeDto : TreeDto
    {
        /// <summary>
        /// Имеются ли дочерние элементы
        /// </summary>
        [Display(Name = "Имеются дети")]
        [DataMember]
        [JsonProperty(PropertyName = "HasChildren")]
        public bool HasChildren { get; set; }
    }
}

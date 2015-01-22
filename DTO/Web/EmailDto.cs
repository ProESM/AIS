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
    /// Облегченный класс DTO для передачи одного лишь email
    /// </summary>
    [DataContract]
    [Serializable]
    public class EmailDto : BaseDto
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Display(Name = "Электронная почта")]
        [DataMember]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }
}

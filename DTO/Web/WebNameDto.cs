﻿using System;
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
    /// Облегченный класс DTO для передачи одного лишь Name
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebNameDto : BaseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }
}
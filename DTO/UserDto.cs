﻿using System;
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
    /// Расширенный класс DTO объекта дерева s_tree.
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserDto : BaseTreeDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Пароль пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Соль для удлинения строки пароля
        /// </summary>
        [Display(Name = "Соль для удлинения строки пароля")]
        [DataMember]
        [JsonProperty(PropertyName = "Salt")]
        public string Salt { get; set; }
        /// <summary>
        /// Группа пользователей, к которой относится пользователь
        /// </summary>
        [Display(Name = "Группа пользователей")]
        [DataMember]
        [JsonProperty(PropertyName = "UserGroupId")]
        public Guid UserGroupId { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        [DataMember]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    } 
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// Класс DTO ячейки данных отчета.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportDataDto : BaseDto
    {
        /// <summary>
        /// Id ячейки с данными
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        public Guid ReportId { get; set; }                
        /// <summary>
        /// Номер столбца
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// Номер строки
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Значение в ячейке
        /// </summary>
        public string Value { get; set; }
    }
}

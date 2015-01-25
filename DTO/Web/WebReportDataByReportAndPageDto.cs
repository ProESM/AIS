using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Web
{    
    /// <summary>
    /// Облегченный класс DTO для задания критериев поиска данных в отчетах через Web
    /// </summary>
    [DataContract]
    [Serializable]
    public class WebReportDataByReportAndPageDto : BaseDto
    {
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        public Guid ReportId { get; set; }        
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; set; }        
    }
}

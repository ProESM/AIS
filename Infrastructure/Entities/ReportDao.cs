using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ReportDao : DocumentDao
    {        
        /// <summary>
        /// Id типа отчета
        /// </summary>
        public virtual Guid ReportTypeId
        {
            get { return ReportType.Id; }
        }
        /// <summary>
        /// Тип отчета
        /// </summary>
        public virtual TreeDao ReportType { get; set; }
        /// <summary>
        /// Id организации-владельца отчёта
        /// </summary>
        public virtual Guid ReportEnterpriseId
        {
            get { return ReportEnterprise.Id; }
        }
        /// <summary>
        /// Организация-владелец отчёта
        /// </summary>
        public virtual TreeDao ReportEnterprise { get; set; }
        /// <summary>
        /// Дата начала заполнения отчёта
        /// </summary>
        public virtual DateTime FillingDate { get; set; }
        /// <summary>
        /// Дата окончания заполнения
        /// </summary>
        public virtual DateTime ExpiryFillingDate { get; set; }
    }
}

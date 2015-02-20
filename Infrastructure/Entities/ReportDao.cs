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
        /// Id адресата отчета
        /// </summary>
        public virtual Guid RecipientId
        {
            get { return Recipient.Id; }
        }
        /// <summary>
        /// Адресат отчета - это огранизация, подразделение или конкретный сотрудник, для
        /// которого предназначен документ (к заполнению, ознакомлению и т.п.)
        /// </summary>
        public virtual TreeDao Recipient { get; set; }
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

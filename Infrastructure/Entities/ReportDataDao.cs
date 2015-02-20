using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;

namespace Infrastructure.Entities
{
    public class ReportDataDao : IEntity
    {
        /// <summary>
        /// Id ячейки с данными
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// Id отчета-владельца данных
        /// </summary>
        public virtual Guid ReportId
        {
            get { return Report.Id; }
        }
        /// <summary>
        /// Отчет-владелец данных
        /// </summary>
        public virtual ReportDao Report { get; set; }
        /// <summary>
        /// Номер столбца
        /// </summary>
        public virtual int Column { get; set; }
        /// <summary>
        /// Номер строки
        /// </summary>
        public virtual int Row { get; set; }
        /// <summary>
        /// Номер страницы
        /// </summary>
        public virtual int Page { get; set; }
        /// <summary>
        /// Значение в ячейке
        /// </summary>
        public virtual string Value { get; set; }
    }
}

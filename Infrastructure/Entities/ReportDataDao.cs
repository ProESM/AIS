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
        public virtual Guid Id { get; set; }

        public virtual Guid ReportId
        {
            get { return Report.Id; }
        }
        public virtual ReportDao Report { get; set; }

        public virtual int Column { get; set; }

        public virtual int Row { get; set; }

        public virtual int Page { get; set; }

        public virtual string Value { get; set; }
    }
}

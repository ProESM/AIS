using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ReportDao : DocumentDao
    {        
        public virtual Guid ReportTypeId
        {
            get { return ReportType.Id; }
        }

        public virtual TreeDao ReportType { get; set; }

        public virtual Guid ReportEnterpriseId
        {
            get { return ReportEnterprise.Id; }
        }

        public virtual TreeDao ReportEnterprise { get; set; }

        public virtual DateTime FillingDate { get; set; }

        public virtual DateTime ExpiryFillingDate { get; set; }
    }
}

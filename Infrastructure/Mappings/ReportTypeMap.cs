using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class ReportTypeMap : SubclassMap<ReportTypeDao>
    {
        public ReportTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otReportType.ToString());

            Join("L_REPORT_TYPES", j =>
            {
                j.Fetch.Select();
                j.Table("L_REPORT_TYPES");
                j.KeyColumn("ID");
                j.References(x => x.Group)
                    .Column("GROUP_ID");
            });
        }
    }
}

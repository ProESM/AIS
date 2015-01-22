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
    public class ReportMap : SubclassMap<ReportDao>
    {
        public ReportMap()
        {
            DiscriminatorValue(ObjectTypes.otDocument.ToString());

            Join("L_REPORTS", j =>
            {
                j.Fetch.Select();
                j.Table("L_REPORTS");
                j.KeyColumn("ID");
                j.References(x => x.ReportType)
                    .Column("TYPE_ID");
                j.References(x => x.ReportEnterprise)
                    .Column("ENTERPRISE_ID");
                j.Map(x => x.FillingDate)
                    .Column("FILLING_DATE")
                    .Nullable();
                j.Map(x => x.ExpiryFillingDate)
                    .Column("EXPIRY_FILLING_DATE")
                    .Nullable();
            });
        }
    }
}

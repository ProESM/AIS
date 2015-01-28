using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class ReportDataMap : ClassMap<ReportDataDao>
    {
        public ReportDataMap()
        {
            Table("L_REPORT_DATA");
            Id(x => x.Id)
                .Column("ID")
                .Length(36)
                .Not.Nullable();
            References(x => x.Report)
                .Column("REPORT_ID");
            Map(x => x.Column)
                .Column("CELL_COLUMN")
                .Not.Nullable();
            Map(x => x.Row)
                .Column("CELL_ROW")
                .Not.Nullable();
            Map(x => x.Page)
                .Column("PAGE")
                .Not.Nullable();
            Map(x => x.Value)
                .Column("CELL_VALUE")
                .Nullable();
            Cache.ReadWrite();
        }
    }
}

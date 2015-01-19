using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class ReportTypeGroupMap : SubclassMap<ReportTypeGroupDao>
    {
        public ReportTypeGroupMap()
        {
            DiscriminatorValue(ObjectTypes.otReportTypeGroup.ToString());
        }
    }
}

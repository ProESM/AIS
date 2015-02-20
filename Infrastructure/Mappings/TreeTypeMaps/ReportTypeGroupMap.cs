using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class ReportTypeGroupMap : SubclassMap<ReportTypeGroupDao>
    {
        public ReportTypeGroupMap()
        {
            DiscriminatorValue(ObjectTypes.otReportTypeGroup.ToString());
        }
    }
}

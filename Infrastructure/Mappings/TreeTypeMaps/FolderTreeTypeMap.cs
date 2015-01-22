using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class FolderTreeTypeMap : SubclassMap<FolderTreeTypeDao>
    {
        public FolderTreeTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otFolder.ToString());
        }
    }
}

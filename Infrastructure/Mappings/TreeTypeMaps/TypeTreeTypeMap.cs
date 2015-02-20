using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class TypeTreeTypeMap : SubclassMap<TypeTreeTypeDao>
    {
        public TypeTreeTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otType.ToString());
        }
    }
}

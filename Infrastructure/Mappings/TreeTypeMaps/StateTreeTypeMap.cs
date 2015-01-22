using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class StateTreeTypeMap : SubclassMap<StateTreeTypeDao>
    {
        public StateTreeTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otState.ToString());
        }
    }
}

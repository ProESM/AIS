using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDao;

namespace Infrastructure.Mappings.TreeTypeMap
{
    public class StateTreeTypeMap : SubclassMap<StateTreeTypeDao>
    {
        public StateTreeTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otState.ToString());
        }
    }
}

using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class JuridicalPersonMap : SubclassMap<JuridicalPersonDao>
    {
        public JuridicalPersonMap()
        {
            DiscriminatorValue(ObjectTypes.otJuridicalPerson.ToString());
        }
    }
}

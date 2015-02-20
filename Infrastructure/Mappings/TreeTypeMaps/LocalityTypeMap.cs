using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class LocalityTypeMap : SubclassMap<LocalityTypeDao>
    {
        public LocalityTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otLocalityType.ToString());
        }
    }
}

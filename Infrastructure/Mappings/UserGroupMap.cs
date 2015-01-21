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
    public class UserGroupMap : SubclassMap<UserGroupDao>
    {
        public UserGroupMap()
        {
            DiscriminatorValue(ObjectTypes.otUserGroup.ToString());
        }
    }
}

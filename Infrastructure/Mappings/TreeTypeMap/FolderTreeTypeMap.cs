using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDao;

namespace Infrastructure.Mappings.TreeTypeMap
{
    public class FolderTreeTypeMap : SubclassMap<FolderTreeTypeDao>
    {
        public FolderTreeTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otFolder.ToString());
        }
    }
}

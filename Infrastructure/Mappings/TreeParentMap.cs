using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class TreeParentMap : ClassMap<TreeParentDao>
    {
        public TreeParentMap()
        {
            Table("L_TREE_PARENTS");
            Id(x => x.Id)
                .Column("ID")
                .Length(36);            
            References(x => x.TreeParent)
                .Column("TREE_PARENT_ID");
            References(x => x.TreeChild)
                .Column("TREE_CHILD_ID");
            Map(x => x.Level)
                .Column("LEVEL")                
                .Not.Nullable();
            References(x => x.TreeParentType)
                .Column("TREE_PARENT_TYPE_ID");            
            //Polymorphism.Explicit();
            //Polymorphism.Implicit();
            Cache.ReadWrite();
        }
    }
}

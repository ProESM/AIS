using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using Infrastructure.Entities;
using NHibernate.Type;

namespace Infrastructure.Mappings
{
    public class TreeMap : ClassMap<TreeDao>
    {
        public TreeMap()
        {
            Table("L_TREE");
            Id(x => x.Id)
                .Column("ID")
                .Length(36)
                .Not.Nullable();             
            References(x => x.Parent)
                .Column("PARENT_ID")                
                .Nullable();
            Map(x => x.Name)
                .Column("NAME")
                .Length(255)
                .Not.Nullable();
            Map(x => x.ShortName)
                .Column("SHORT_NAME")
                .Length(100)
                .Nullable();
            References(x => x.Type)
                .Column("TYPE_ID")
                .Not.Nullable();
            References(x => x.State)
                .Column("STATE_ID")
                .Not.Nullable();
            Map(x => x.CreateDateTime)
                .Column("CREATE_DATETIME")
                .Not.Nullable();
            Polymorphism.Explicit();                        
            //Polymorphism.Implicit();
            Cache.ReadWrite();
            DiscriminateSubClassesOnColumn("").Formula("TYPE_ID");
        }        
    }
}

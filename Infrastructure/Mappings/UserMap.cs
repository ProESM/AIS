using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class UserMap : SubclassMap<UserDao>
    {
        public UserMap()
        {
            DiscriminatorValue(ObjectTypes.otUser.ToString());

            Join("L_USERS", j =>
            {
                j.Fetch.Select();
                j.Table("L_USERS");
                j.KeyColumn("ID");
                j.Map(x => x.Login)
                    .Column("LOGIN")
                    .Length(50)
                    .Not.Nullable();
                j.Map(x => x.Password)
                    .Column("PASSWORD")
                    .Length(50)
                    .Nullable();
                j.Map(x => x.Salt)
                    .Column("SALT")
                    .Length(50)
                    .Nullable();
                j.References(x => x.UserGroup)
                    .Column("USER_GROUP_ID");
                j.Map(x => x.Email)
                    .Column("EMAIL")
                    .Length(50)
                    .Not.Nullable();
                j.Map(x => x.Phone)
                    .Column("PHONE")
                    .Length(20)
                    .Nullable();
                j.References(x => x.Person)
                    .Column("PERSON_ID")
                    .Nullable();                
            });            
        }
    }
}

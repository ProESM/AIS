using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;
using NHibernate.Mapping;

namespace Infrastructure.Mappings
{
    public class PersonMap : SubclassMap<PersonDao>
    {
        public PersonMap()
        {
            DiscriminatorValue(ObjectTypes.otIndividualPerson.ToString());            
            Join("L_PEOPLE", j =>
            {
                j.Fetch.Select();
                j.KeyColumn("ID");
                j.Map(x => x.Surname)
                    .Column("SURNAME")
                    .Length(80)
                    .Nullable();
                j.Map(x => x.FirstName)
                    .Column("FIRST_NAME")
                    .Length(80)
                    .Nullable();
                j.Map(x => x.Patronymic)
                    .Column("PATRONYMIC")
                    .Length(100)
                    .Nullable();
                j.Map(x => x.BirthDate)
                    .Column("BIRTHDATE")
                    .Nullable();
            });               
        }
    }
}

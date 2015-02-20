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
    public class InstituteMap : SubclassMap<InstituteDao>
    {
        public InstituteMap()
        {
            DiscriminatorValue(ObjectTypes.otJuridicalPerson.ToString());

            Join("L_INSTITUTIONS", j =>
            {
                j.Fetch.Select();
                j.Table("L_INSTITUTIONS");
                j.KeyColumn("ID");
                j.References(x => x.Location)
                    .Column("LOCATION_ID");
                j.References(x => x.EducationLevel)
                    .Column("EDUCATION_LEVEL_ID");
                j.References(x => x.LocalityType)
                    .Column("LOCALITY_TYPE_ID");
            });
        }
    }
}

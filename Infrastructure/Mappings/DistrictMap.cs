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
    public class DistrictMap : SubclassMap<DistrictDao>
    {
        public DistrictMap()
        {
            DiscriminatorValue(ObjectTypes.otDistrict.ToString());

            Join("L_DISTRICTS", j =>
            {
                j.Fetch.Select();
                j.Table("L_DISTRICTS");
                j.KeyColumn("ID");
                j.References(x => x.Region)
                    .Column("REGION_ID");
            });
        }
    }
}

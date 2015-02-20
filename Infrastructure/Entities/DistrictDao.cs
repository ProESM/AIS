using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Entities
{
    /// <summary>
    /// DAO районов.
    /// </summary>
    [DataContract]
    public class DistrictDao : TreeDao
    {
        public virtual Guid RegionId
        {
            get
            {
                return Region.Id;
            }
        }

        public virtual RegionDao Region { get; set; }
    }
}

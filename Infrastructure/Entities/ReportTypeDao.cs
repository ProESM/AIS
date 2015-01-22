using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Entities
{
    [DataContract]
    public class ReportTypeDao : TreeDao
    {
        public virtual Guid GroupId
        {
            get
            {
                return Group.Id;
            }
        }

        public virtual ReportTypeGroupDao Group { get; set; }
    }
}

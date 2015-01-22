using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace Infrastructure.Entities
{
    public class VirtualTreeDao : TreeDao
    {
        public virtual bool HasChildren { get; set; }
    }
}

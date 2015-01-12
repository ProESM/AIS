using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate.Mapping.Attributes;

namespace Infrastructure.Entities
{
    [DataContract]
    [HibernateMapping(0)]
    [Class(1, Table = "L_TREE")]
    [Cache(2, Usage = CacheUsage.ReadWrite)]
    public class TreeDao : ITreeDao
    {
        [Id(-2, Name = "_Id", Type = "String", Length = 36)]
        [Column(-1, Name = "ID")]
        public virtual string _Id { get; set; }
        public virtual Guid Id {
            get { return new Guid(_Id); }
            set { _Id = value.ToString(); }
        }

        public virtual Guid? ParentId {
            get { return Parent == null ? (Guid?) null : Parent.Id; }
        }

        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "PARENT_ID")]
        public virtual TreeDao Parent { get; set; }
        [Property(Name = "Name", Column = "NAME", Type = "String", Length = 255, NotNull = true)]
        public virtual string Name { get; set; }
        [Property(Name = "ShortName", Column = "SHORT_NAME", Type = "String", Length = 100, NotNull = false)]
        public virtual string ShortName { get; set; }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "TYPE_ID")]
        public virtual TreeDao Type { get; set; }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "STATE_ID")]
        public virtual TreeDao State { get; set; }
        [Property(Name = "CreateDateTime", Column = "CREATE_DATETIME", Type = "DateTime", NotNull = true)]
        public virtual DateTime CreateDateTime { get; set; }
    }
}

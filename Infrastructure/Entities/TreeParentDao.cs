using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using NHibernate.Mapping.Attributes;

namespace Infrastructure.Entities
{
    [DataContract]
    [HibernateMapping(0)]
    [Class(1, Table = "L_TREE_PARENTS")]
    [Cache(2, Usage = CacheUsage.ReadWrite)]
    public class TreeParentDao : IEntity
    {
        [Id(-2, Name = "_Id", Type = "String", Length = 36)]
        [Column(-1, Name = "ID")]
        public virtual string _Id { get; set; }
        public virtual Guid Id
        {
            get { return new Guid(_Id); }
            set { _Id = value.ToString(); }
        }

        public virtual Guid? TreeParentId
        {
            get { return TreeParent == null ? (Guid?)null : TreeParent.Id; }
        }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "TREE_PARENT_ID")]
        public virtual TreeDao TreeParent { get; set; }

        public virtual Guid? TreeChildId
        {
            get { return TreeChild == null ? (Guid?)null : TreeChild.Id; }
        }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "TREE_CHILD_ID")]
        public virtual TreeDao TreeChild { get; set; }

        [Property(Name = "Level", Column = "LEVEL", Type = "int", NotNull = true)]
        public virtual int Level { get; set; }

        public virtual Guid? TreeParentTypeId
        {
            get { return TreeParentType == null ? (Guid?)null : TreeParentType.Id; }
        }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "TREE_PARENT_TYPE_ID")]
        public virtual TreeDao TreeParentType { get; set; }
    }
}

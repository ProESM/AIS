using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;

namespace Infrastructure.Entities
{
    [DataContract]
    [HibernateMapping(0)]
    [JoinedSubclass(1, Name = "L_USERS", Table = "L_USERS", NameType = typeof(UserDao), ExtendsType = typeof(TreeDao), Lazy = true)]    
    //[Subclass(1, DiscriminatorValue = "TYPE_ID", NameType = typeof(UserDao), ExtendsType = typeof(TreeDao), Lazy = true)]
    [Key(2, Column = "ID")]    
    [Cache(3, Usage = CacheUsage.ReadWrite)]
    public class UserDao : TreeDao
    {
        [Property(Name = "Login", Column = "LOGIN", Type = "String", Length = 50, NotNull = true)]
        public virtual string Login { get; set; }
        [Property(Name = "Password", Column = "PASSWORD", Type = "String", Length = 50, NotNull = false)]
        public virtual string Password { get; set; }
        [Property(Name = "Salt", Column = "SALT", Type = "String", Length = 50, NotNull = false)]
        public virtual string Salt { get; set; }
        [ManyToOne(0, ClassType = typeof(TreeDao), Column = "USER_GROUP_ID")]
        public virtual TreeDao UserGroup { get; set; }        
        [Property(Name = "Email", Column = "EMAIL", Type = "String", Length = 50, NotNull = true)]
        public virtual string Email { get; set; }
        [Property(Name = "Phone", Column = "PHONE", Type = "String", Length = 20, NotNull = false)]
        public virtual string Phone { get; set; }

        public virtual Guid? PersonId
        {
            get
            {
                return Person == null ? (Guid?)null : Person.Id;
            }
        }

        [ManyToOne(0, ClassType = typeof(PersonDao), Column = "PERSON_ID", NotNull = false)]
        public virtual PersonDao Person { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace Infrastructure.Entities
{
    [DataContract]
    [HibernateMapping(0)]
    [JoinedSubclass(1, Name = "L_PEOPLE", Table = "L_PEOPLE", NameType = typeof(PersonDao), ExtendsType = typeof(TreeDao))]
    [Key(2, Column = "ID")]
    [Cache(3, Usage = CacheUsage.ReadWrite)]
    public class PersonDao : TreeDao
    {
        [Property(Name = "Surname", Column = "SURNAME", Type = "String", Length = 80, NotNull = false)]
        public virtual string Surname { get; set; }
        [Property(Name = "FirstName", Column = "FIRST_NAME", Type = "String", Length = 80, NotNull = false)]
        public virtual string FirstName { get; set; }
        [Property(Name = "Patronymic", Column = "PATRONYMIC", Type = "String", Length = 100, NotNull = false)]
        public virtual string Patronymic { get; set; }
        [Property(Name = "BirthDate", Column = "BIRTHDATE", Type = "DateTime", NotNull = false)]
        public virtual DateTime BirthDate { get; set; }
    }
}

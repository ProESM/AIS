using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities;

namespace Infrastructure.Mappings
{
    public class DocumentMap : SubclassMap<DocumentDao>
    {
        public DocumentMap()
        {
            DiscriminatorValue(ObjectTypes.otDocument.ToString());

            Join("L_DOCS", j =>
            {
                j.Fetch.Select();
                j.Table("L_DOCS");
                j.KeyColumn("ID");
                j.References(x => x.DocumentParent)
                    .Column("PARENT_ID")
                    .Nullable();
                j.References(x => x.DocumentType)
                    .Column("TYPE_ID");
                j.References(x => x.DocumentState)
                    .Column("STATE_ID");
                j.References(x => x.DocumentUser)
                    .Column("USER_ID");
                j.Map(x => x.Notes)
                    .Column("NOTES")
                    .Length(255)
                    .Nullable();
            });
        }
    }
}

using Common.Base;
using FluentNHibernate.Mapping;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Mappings.TreeTypeMaps
{
    public class DocumentTypeMap : SubclassMap<DocumentTypeDao>
    {
        public DocumentTypeMap()
        {
            DiscriminatorValue(ObjectTypes.otDocumentType.ToString());
        }
    }
}

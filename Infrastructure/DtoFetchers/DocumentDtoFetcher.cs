using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Infrastructure.Entities;

namespace Infrastructure.DtoFetchers
{
    /// <summary>
    /// Фетчер DTO документа.
    /// </summary>
    public class DocumentDtoFetcher : BaseDtoFetcher<DocumentDao, DocumentDto>
    {
        static DocumentDtoFetcher()
        {
            CreateMapForIndex();
            CreateMapForList();
            CreateMapForCard();
        }

        private static void CreateMapForIndex()
        {
            var map = CreateFetchDtoMap(FetchAim.Index);
            map.Map(d => d.Id, e => e.Id)
                .Map(d => d.Name, e => e.Name);
        }

        private static void CreateMapForList()
        {
            var map = CreateFetchDtoMap(FetchAim.List);

            map.Map(d => d.Id, e => e.Id)
                .Map(d => d.ParentId, e => e.ParentId)
                .Map(d => d.Name, e => e.Name)
                .Map(d => d.ShortName, e => e.ShortName)
                .Map(d => d.TypeId, e => e.Type.Id)
                .Map(d => d.StateId, e => e.State.Id)
                .Map(d => d.CreateDateTime, e => e.CreateDateTime)
                .Map(d => d.DocumentParentId, e => e.DocumentParentId)
                .Map(d => d.DocumentParentName, e => e.DocumentParent.Name)
                .Map(d => d.DocumentTypeId, e => e.DocumentTypeId)
                .Map(d => d.DocumentTypeName, e => e.DocumentType.Name)
                .Map(d => d.DocumentStateId, e => e.DocumentStateId)
                .Map(d => d.DocumentStateName, e => e.DocumentState.Name)
                .Map(d => d.DocumentUserId, e => e.DocumentUserId)
                .Map(d => d.DocumentUserName, e => e.DocumentUser.Name)
                .Map(d => d.Notes, e => e.Notes)
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<DocumentDao, DocumentDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //   .Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        private static void CreateMapForCard()
        {
            var map = CreateFetchDtoMap(FetchAim.Card);

            map.Map(d => d.Id, e => e.Id)
                .Map(d => d.ParentId, e => e.ParentId)
                .Map(d => d.Name, e => e.Name)
                .Map(d => d.ShortName, e => e.ShortName)
                .Map(d => d.TypeId, e => e.Type.Id)
                .Map(d => d.StateId, e => e.State.Id)
                .Map(d => d.CreateDateTime, e => e.CreateDateTime)
                .Map(d => d.DocumentParentId, e => e.DocumentParentId)
                .Map(d => d.DocumentParentName, e => e.DocumentParent.Name)
                .Map(d => d.DocumentTypeId, e => e.DocumentTypeId)
                .Map(d => d.DocumentTypeName, e => e.DocumentType.Name)
                .Map(d => d.DocumentStateId, e => e.DocumentStateId)
                .Map(d => d.DocumentStateName, e => e.DocumentState.Name)
                .Map(d => d.DocumentUserId, e => e.DocumentUserId)
                .Map(d => d.DocumentUserName, e => e.DocumentUser.Name)
                .Map(d => d.Notes, e => e.Notes)
                ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<DocumentDao, DocumentDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public DocumentDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

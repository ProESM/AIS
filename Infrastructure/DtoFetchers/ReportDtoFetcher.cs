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
    /// Фетчер DTO отчета.
    /// </summary>
    public class ReportDtoFetcher : BaseDtoFetcher<ReportDao, ReportDto>
    {
        static ReportDtoFetcher()
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
                .Map(d => d.DocumentParentName, e => e.DocumentParentName)
                .Map(d => d.DocumentTypeId, e => e.DocumentTypeId)
                .Map(d => d.DocumentTypeName, e => e.DocumentType.Name)
                .Map(d => d.DocumentStateId, e => e.DocumentStateId)
                .Map(d => d.DocumentStateName, e => e.DocumentState.Name)
                .Map(d => d.DocumentUserId, e => e.DocumentUserId)
                .Map(d => d.DocumentUserName, e => e.DocumentUser.Name)
                .Map(d => d.Notes, e => e.Notes)

                .Map(d => d.ReportTypeId, e => e.ReportTypeId)
                .Map(d => d.ReportTypeName, e => e.ReportType.Name)
                .Map(d => d.RecipientId, e => e.RecipientId)
                .Map(d => d.RecipientName, e => e.Recipient.Name)
                .Map(d => d.FillingDate, e => e.FillingDate)
                .Map(d => d.ExpiryFillingDate, e => e.ExpiryFillingDate)
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<ReportDao, ReportDto> map)
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
                .Map(d => d.DocumentParentName, e => e.DocumentParentName)
                .Map(d => d.DocumentTypeId, e => e.DocumentTypeId)
                .Map(d => d.DocumentTypeName, e => e.DocumentType.Name)
                .Map(d => d.DocumentStateId, e => e.DocumentStateId)
                .Map(d => d.DocumentStateName, e => e.DocumentState.Name)
                .Map(d => d.DocumentUserId, e => e.DocumentUserId)
                .Map(d => d.DocumentUserName, e => e.DocumentUser.Name)
                .Map(d => d.Notes, e => e.Notes)

                .Map(d => d.ReportTypeId, e => e.ReportTypeId)
                .Map(d => d.ReportTypeName, e => e.ReportType.Name)
                .Map(d => d.RecipientId, e => e.RecipientId)
                .Map(d => d.RecipientName, e => e.Recipient.Name)
                .Map(d => d.FillingDate, e => e.FillingDate)
                .Map(d => d.ExpiryFillingDate, e => e.ExpiryFillingDate)
                ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<ReportDao, ReportDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public ReportDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

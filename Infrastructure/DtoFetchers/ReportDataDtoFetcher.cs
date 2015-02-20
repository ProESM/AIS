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
    public class ReportDataDtoFetcher : BaseDtoFetcher<ReportDataDao, ReportDataDto>
    {
        static ReportDataDtoFetcher()
        {
            CreateMapForIndex();
            CreateMapForList();
            CreateMapForCard();
        }

        private static void CreateMapForIndex()
        {
            var map = CreateFetchDtoMap(FetchAim.Index);
            map.Map(d => d.Id, e => e.Id);
        }

        private static void CreateMapForList()
        {
            var map = CreateFetchDtoMap(FetchAim.List);

            map.Map(d => d.Id, e => e.Id)
                .Map(d => d.ReportId, e => e.ReportId)
                .Map(d => d.Column, e => e.Column)
                .Map(d => d.Row, e => e.Row)
                .Map(d => d.Page, e => e.Page)
                .Map(d => d.Value, e => e.Value)                
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<ReportDataDao, ReportDataDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //   .Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        private static void CreateMapForCard()
        {
            var map = CreateFetchDtoMap(FetchAim.Card);

            map.Map(d => d.Id, e => e.Id)
                .Map(d => d.ReportId, e => e.ReportId)
                .Map(d => d.Column, e => e.Column)
                .Map(d => d.Row, e => e.Row)
                .Map(d => d.Page, e => e.Page)
                .Map(d => d.Value, e => e.Value)
                ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<ReportDataDao, ReportDataDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public ReportDataDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

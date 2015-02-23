using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.TreeTypeDtos;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.DtoFetchers.TreeTypeDtoFetchers
{
    /// <summary>
    /// Фетчер DTO уровня профессионального образования.
    /// </summary>
    public class EducationLevelDtoFetcher : BaseDtoFetcher<EducationLevelDao, EducationLevelDto>
    {
        static EducationLevelDtoFetcher()
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
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<EducationLevelDao, EducationLevelDto> map)
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
               ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<EducationLevelDao, EducationLevelDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public EducationLevelDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

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
    /// Фетчер DTO образовательных учреждений.
    /// </summary>
    public class InstiteDtoFetcher : BaseDtoFetcher<InstituteDao, InstituteDto>
    {
        static InstiteDtoFetcher()
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

                .Map(d => d.LocationId, e => e.LocationId)
                .Map(d => d.LocationName, e => e.Location.Name)
                .Map(d => d.EducationLevelId, e => e.EducationLevelId)
                .Map(d => d.EducationLevelName, e => e.EducationLevel.Name)
                .Map(d => d.LocalityTypeId, e => e.LocalityTypeId)
                .Map(d => d.LocalityTypeName, e => e.LocalityType.Name)                
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<InstituteDao, InstituteDto> map)
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

                .Map(d => d.LocationId, e => e.LocationId)
                .Map(d => d.LocationName, e => e.Location.Name)
                .Map(d => d.EducationLevelId, e => e.EducationLevelId)
                .Map(d => d.EducationLevelName, e => e.EducationLevel.Name)
                .Map(d => d.LocalityTypeId, e => e.LocalityTypeId)
                .Map(d => d.LocalityTypeName, e => e.LocalityType.Name)
                ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<InstituteDao, InstituteDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public InstiteDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Infrastructure.Entities;

namespace Infrastructure.DtoFetchers
{    
    /// <summary>
    /// Фетчер DTO районов.
    /// </summary>
    public class DistrictDtoFetcher : BaseDtoFetcher<DistrictDao, DistrictDto>
    {
        static DistrictDtoFetcher()
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
                .Map(d => d.RegionId, e => e.RegionId)
                .Map(d => d.RegionName, e => e.Region.Name)
                ;

            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchDtoMap<DistrictDao, DistrictDto> map)
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
               .Map(d => d.RegionId, e => e.RegionId)
               .Map(d => d.RegionName, e => e.Region.Name)
               ;

            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства объекта дерева для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchDtoMap<DistrictDao, DistrictDto> map)
        {
            //map.Map(d => d.Id, e => e.C_Tree)
            //.Map(d => d.ParentTreeId, e => e.C_Parent_Tree);
        }

        public DistrictDtoFetcher(IRepository repository)
            : base(repository)
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;

namespace Domain
{    
    public interface IDomainTreeService
    {
        List<Guid> GetSystemObjects();

        List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false);

        /// <summary>
        /// Получаем список родительских объектов для указанного дочернего объекта
        /// </summary>
        /// <param name="parent">Родительский объект, в пределах которого будут отобраны остальные родительские объекты</param>
        /// <param name="child">Дочерний объект, для которого будут получены родительские объекты</param>
        /// <param name="treeParentType">Тип виртуального дерева, в пределах которого будут отобраны родительские объекты</param>
        /// <param name="includeChild">Флаг - включать ли в результаты сам дочерний объект</param>
        /// <param name="includeDeleted">Флаг - включать ли в результаты удаленные объекты</param>
        /// <returns>Список родительских объектов</returns>
        List<VirtualTreeDto> GetTreeParents(Guid? parent, Guid child, Guid treeParentType, bool includeChild = false, bool includeDeleted = false);

        /// <summary>
        /// Функция поиска объектов по заданному критерию поиска
        /// </summary>
        /// <param name="searchText">Критерий поиска (строка, вхождение которой в наименование объектов будет производиться)</param>
        /// <param name="treeParentType">Тип виртуального дерева, в пределах которого будет производиться поиск</param>
        /// <param name="typeIds">Типы объектов, которые попадут в область поиска</param>
        /// <param name="ignoreTypeIds">Типы объектов, которые будут проигнорированы в процессе поиска</param>
        /// <param name="parent">Родительский узел, в пределах которого будет производиться поиск</param>
        /// <returns>Возвращает список объектов, удовлетворяющих критериям поиска</returns>
        List<VirtualTreeDto> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds, List<Guid> ignoreTypeIds, Guid? parent = null);

        TreeDto CreateTree(TreeDto treeDto);

        TreeDto GetTree(Guid treeId);

        void UpdateTree(TreeDto treeDto);

        void DeleteTree(TreeDto treeDto);

        UserDto CreateUser(UserDto userDto);

        UserDto GetUser(Guid userId);

        void UpdateUser(UserDto userDto);

        UserDto FindUserByLogin(string login);

        UserDto FindUserByEmail(string email);

        UserDto AuthenticateUser(string login, string password);

        UserDto RegisterUser(RegistrationUserDto userDto);

        PersonDto CreatePerson(PersonDto personDto);

        PersonDto GetPerson(Guid personId);

        void UpdatePerson(PersonDto personDto);
    }

}

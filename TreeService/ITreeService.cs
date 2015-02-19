using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DTO;
using DTO.TreeTypeDtos;

namespace TreeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITreeService" in both code and config file together.
    [ServiceContract]
    public interface ITreeService
    {
        [OperationContract]
        List<string> GetSystemObjects();

        [OperationContract]
        List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false, bool includeSubChildren = false);

        /// <summary>
        /// Получаем список родительских объектов для указанного дочернего объекта
        /// </summary>
        /// <param name="parent">Родительский объект, в пределах которого будут отобраны остальные родительские объекты</param>
        /// <param name="child">Дочерний объект, для которого будут получены родительские объекты</param>
        /// <param name="treeParentType">Тип виртуального дерева, в пределах которого будут отобраны родительские объекты</param>
        /// <param name="includeChild">Флаг - включать ли в результаты сам дочерний объект</param>
        /// <param name="includeDeleted">Флаг - включать ли в результаты удаленные объекты</param>
        /// <returns>Список родительских объектов</returns>
        [OperationContract]
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
        [OperationContract]
        List<VirtualTreeDto> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds, List<Guid> ignoreTypeIds, Guid? parent = null);

        [OperationContract]
        TreeDto CreateTree(TreeDto treeDto);

        [OperationContract]
        TreeDto GetTree(Guid treeId);

        [OperationContract]
        void UpdateTree(TreeDto treeDto);

        [OperationContract]
        void DeleteTree(TreeDto treeDto);

        [OperationContract]
        UserDto CreateUser(UserDto userDto);

        [OperationContract]
        UserDto GetUser(Guid userId);

        [OperationContract]
        void UpdateUser(UserDto userDto);

        [OperationContract]
        UserDto FindUserByLogin(string login);

        [OperationContract]
        UserDto AuthenticateUser(string login, string password);

        [OperationContract]
        PersonDto CreatePerson(PersonDto personDto);

        [OperationContract]
        PersonDto GetPerson(Guid personId);

        [OperationContract]
        void UpdatePerson(PersonDto personDto);

        [OperationContract]
        DocumentTypeDto CreateDocumentType(DocumentTypeDto documentTypeDto);

        [OperationContract]
        DocumentTypeDto GetDocumentType(Guid documentTypeId);

        [OperationContract]
        List<DocumentTypeDto> GetDocumentTypes();

        [OperationContract]
        void UpdateDocumentType(DocumentTypeDto documentTypeDto);

        [OperationContract]
        DocumentDto CreateDocument(DocumentDto documentDto);

        [OperationContract]
        DocumentDto GetDocument(Guid documentId);

        [OperationContract]
        List<DocumentDto> GetDocuments();

        [OperationContract]
        void UpdateDocument(DocumentDto documentDto);

        [OperationContract]
        ReportTypeGroupDto CreateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto);

        [OperationContract]
        ReportTypeGroupDto GetReportTypeGroup(Guid reportTypeGroupId);

        [OperationContract]
        List<ReportTypeGroupDto> GetReportTypeGroups();

        [OperationContract]
        void UpdateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto);

        [OperationContract]
        ReportTypeDto CreateReportType(ReportTypeDto reportTypeDto);

        [OperationContract]
        ReportTypeDto GetReportType(Guid reportTypeId);

        [OperationContract]
        List<ReportTypeDto> GetReportTypes();

        [OperationContract]
        void UpdateReportType(ReportTypeDto reportTypeDto);

        [OperationContract]
        ReportDto CreateReport(ReportDto reportDto);

        [OperationContract]
        ReportDto GetReport(Guid reportId);

        [OperationContract]
        List<ReportDto> GetReports();

        [OperationContract]
        void UpdateReport(ReportDto reportDto);

        [OperationContract]
        JuridicalPersonDto CreateJuridicalPerson(JuridicalPersonDto juridicalPersonDto);

        [OperationContract]
        JuridicalPersonDto GetJuridicalPerson(Guid juridicalPersonId);

        [OperationContract]
        List<JuridicalPersonDto> GetJuridicalPersons();

        [OperationContract]
        void UpdateJuridicalPerson(JuridicalPersonDto juridicalPersonDto);
    }
}

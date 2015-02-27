using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure
{
    public interface ITreeRepository : IRepository
    {
        List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false, bool includeSubChildren = false);

        /// <summary>
        /// Получаем список родительских объектов для указанного дочернего объекта
        /// </summary>
        /// <param name="parent">Родительский объект, в пределах которого будут отобраны остальные родительские объекты</param>
        /// <param name="child">Дочерний объект, для которого будут получены родительские объекты</param>
        /// <param name="treeParentType">Тип виртуального дерева, в пределах которого будут отобраны родительские объекты</param>
        /// <param name="includeChild">Флаг - включать ли в результаты сам дочерний объект</param>
        /// <param name="includeDeleted">Флаг - включать ли в результаты удаленные объекты</param>
        /// <returns>Список родительских объектов</returns>
        List<VirtualTreeDao> GetTreeParents(Guid? parent, Guid child, Guid treeParentType, bool includeChild = false, bool includeDeleted = false);

        /// <summary>
        /// Функция поиска объектов по заданному критерию поиска
        /// </summary>
        /// <param name="searchText">Критерий поиска (строка, вхождение которой в наименование объектов будет производиться)</param>
        /// <param name="treeParentType">Тип виртуального дерева, в пределах которого будет производиться поиск</param>
        /// <param name="typeIds">Типы объектов, которые попадут в область поиска</param>
        /// <param name="ignoreTypeIds">Типы объектов, которые будут проигнорированы в процессе поиска</param>
        /// <param name="parent">Родительский узел, в пределах которого будет производиться поиск</param>
        /// <returns>Возвращает список объектов, удовлетворяющих критериям поиска</returns>
        List<VirtualTreeDao> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds, List<Guid> ignoreTypeIds, Guid? parent = null);

        TreeDao CreateTree(TreeDao treeDao);

        TreeDao GetTree(Guid treeId);

        void UpdateTree(TreeDao treeDao);

        UserDao CreateUser(UserDao userDao);

        UserDao GetUser(Guid userId);

        void UpdateUser(UserDao userDao);

        UserDao FindUserByLogin(string login);

        UserDao FindUserByEmail(string email);

        PersonDao CreatePerson(PersonDao personDao);

        PersonDao GetPerson(Guid personId);

        void UpdatePerson(PersonDao personDao);

        DocumentTypeDao CreateDocumentType(DocumentTypeDao documentTypeDao);

        DocumentTypeDao GetDocumentType(Guid documentTypeId);

        List<DocumentTypeDao> GetDocumentTypes();

        void UpdateDocumentType(DocumentTypeDao documentTypeDao);

        DocumentDao CreateDocument(DocumentDao documentDao);

        DocumentDao GetDocument(Guid documentId);

        List<DocumentDao> GetDocuments();

        DocumentDao GetLastDocumentChange(Guid documentId);

        void UpdateDocument(DocumentDao reportDao);

        ReportTypeGroupDao CreateReportTypeGroup(ReportTypeGroupDao reportTypeGroupDao);

        ReportTypeGroupDao GetReportTypeGroup(Guid reportTypeGroupId);

        List<ReportTypeGroupDao> GetReportTypeGroups();

        void UpdateReportTypeGroup(ReportTypeGroupDao reportTypeGroupDao);

        ReportTypeDao CreateReportType(ReportTypeDao reportTypeDao);

        ReportTypeDao GetReportType(Guid reportTypeId);

        List<ReportTypeDao> GetReportTypes();

        void UpdateReportType(ReportTypeDao reportTypeDao);

        ReportDao CreateReport(ReportDao reportDao);

        ReportDao GetReport(Guid reportId);

        List<ReportDao> GetReports();

        List<ReportDao> GetReportsByType(Guid reportTypeId);

        void UpdateReport(ReportDao reportDao);

        JuridicalPersonDao CreateJuridicalPerson(JuridicalPersonDao juridicalPersonDao);

        JuridicalPersonDao GetJuridicalPerson(Guid juridicalPersonId);

        List<JuridicalPersonDao> GetJuridicalPersons();

        void UpdateJuridicalPerson(JuridicalPersonDao juridicalPersonDao);

        ReportDataDao CreateReportData(ReportDataDao reportDataDao);

        ReportDataDao GetReportData(Guid reportDataId);

        List<ReportDataDao> GetReportDataByReportAndPage(Guid reportId, int page);

        List<ReportDataDao> GetReportDataByReport(Guid reportId);

        int GetReportDataPageCountByReportId(Guid reportId);

        void UpdateReportData(ReportDataDao reportDataDao);

        void DeleteReportData(Guid reportDataId);

        void DeleteReportDataByReport(Guid reportId);

        RegionDao CreateRegion(RegionDao regionDao);

        RegionDao GetRegion(Guid regionId);

        List<RegionDao> GetRegions();

        void UpdateRegion(RegionDao regionDao);

        DistrictDao CreateDistrict(DistrictDao districtDao);

        DistrictDao GetDistrict(Guid districtId);

        List<DistrictDao> GetDistricts();

        void UpdateDistrict(DistrictDao districtDao);

        InstituteDao CreateInstitute(InstituteDao instituteDao);

        InstituteDao GetInstitute(Guid instituteId);

        List<InstituteDao> GetInstitutes();

        void UpdateInstitute(InstituteDao instituteDao);

        EducationLevelDao CreateEducationLevel(EducationLevelDao educationLevelDao);

        EducationLevelDao GetEducationLevel(Guid educationLevelId);

        List<EducationLevelDao> GetEducationLevels();

        void UpdateEducationLevel(EducationLevelDao educationLevelDao);

        LocalityTypeDao CreateLocalityType(LocalityTypeDao localityTypeDao);

        LocalityTypeDao GetLocalityType(Guid localityTypeId);

        List<LocalityTypeDao> GetLocalityTypes();

        void UpdateLocalityType(LocalityTypeDao localityTypeDao);
    }
}

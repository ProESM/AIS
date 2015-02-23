using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using Common.Base;
using Domain;
using Domain.Implementation;
using DTO;
using DTO.TreeTypeDtos;
using Ninject;
using Ninject.Activation;

namespace TreeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TreeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TreeService.svc or TreeService.svc.cs at the Solution Explorer and start debugging.
    public class TreeService : ITreeService
    {
        private IKernel _kernel;
        private IDomainTreeService _domainTreeService;

        public TreeService()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _domainTreeService = _kernel.Get<IDomainTreeService>();

            var current = HttpContext.Current;

            string authHeader = current.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var userLogin = CryptHelper.GetUserFromHttpHeader(authHeader);
                var user = _domainTreeService.FindUserByLogin(userLogin[0]);
                if (user != null)
                {
                    SystemUser.Id = user.Id;
                }
            }
        }

        public List<string> GetSystemObjects()
        {
            var systemObjectsGuids = _domainTreeService.GetSystemObjects();

            return systemObjectsGuids.Select(systemObjectsGuid => systemObjectsGuid.ToString()).ToList();
            //return _domainTreeService.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false, bool includeSubChildren = false)
        {
            return _domainTreeService.GetTrees(parent, treeParentType, includeParent, includeDeleted, includeSubChildren);
        }

        public List<VirtualTreeDto> GetTreeParents(Guid? parent, Guid child, Guid treeParentType,
            bool includeChild = false, bool includeDeleted = false)
        {
            return _domainTreeService.GetTreeParents(parent, child, treeParentType, includeChild, includeDeleted);
        }

        public List<VirtualTreeDto> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds,
            List<Guid> ignoreTypeIds, Guid? parent = null)
        {
            return _domainTreeService.SearchTreesByText(searchText, treeParentType, typeIds, ignoreTypeIds, parent);
        }

        public TreeDto CreateTree(TreeDto treeDto)
        {
            return _domainTreeService.CreateTree(treeDto);
        }

        public TreeDto GetTree(Guid treeId)
        {
            return _domainTreeService.GetTree(treeId);
        }

        public void UpdateTree(TreeDto treeDto)
        {
            _domainTreeService.UpdateTree(treeDto);
        }

        public void DeleteTree(TreeDto treeDto)
        {
            _domainTreeService.DeleteTree(treeDto);
        }

        public UserDto CreateUser(UserDto userDto)
        {
            return _domainTreeService.CreateUser(userDto);
        }

        public UserDto GetUser(Guid userId)
        {
            return _domainTreeService.GetUser(userId);
        }

        public void UpdateUser(UserDto userDto)
        {
            _domainTreeService.UpdateUser(userDto);
        }

        public UserDto FindUserByLogin(string login)
        {
            return _domainTreeService.FindUserByLogin(login);
        }

        public UserDto AuthenticateUser(string login, string password)
        {
            return _domainTreeService.AuthenticateUser(login, password);
        }

        public PersonDto CreatePerson(PersonDto personDto)
        {
            return _domainTreeService.CreatePerson(personDto);
        }

        public PersonDto GetPerson(Guid personId)
        {
            return _domainTreeService.GetPerson(personId);
        }

        public void UpdatePerson(PersonDto personDto)
        {
            _domainTreeService.UpdatePerson(personDto);
        }

        public DocumentTypeDto CreateDocumentType(DocumentTypeDto documentTypeDto)
        {
            return _domainTreeService.CreateDocumentType(documentTypeDto);
        }

        public DocumentTypeDto GetDocumentType(Guid documentTypeId)
        {
            return _domainTreeService.GetDocumentType(documentTypeId);
        }

        public List<DocumentTypeDto> GetDocumentTypes()
        {
            return _domainTreeService.GetDocumentTypes();
        }

        public void UpdateDocumentType(DocumentTypeDto documentTypeDto)
        {
            _domainTreeService.UpdateDocumentType(documentTypeDto);
        }

        public DocumentDto CreateDocument(DocumentDto documentDto)
        {
            return _domainTreeService.CreateDocument(documentDto);
        }

        public DocumentDto GetDocument(Guid documentId)
        {
            return _domainTreeService.GetDocument(documentId);
        }

        public List<DocumentDto> GetDocuments()
        {
            return _domainTreeService.GetDocuments();
        }

        public void UpdateDocument(DocumentDto documentDto)
        {
            _domainTreeService.UpdateDocument(documentDto);
        }

        public ReportTypeGroupDto CreateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto)
        {
            return _domainTreeService.CreateReportTypeGroup(reportTypeGroupDto);
        }

        public ReportTypeGroupDto GetReportTypeGroup(Guid reportTypeGroupId)
        {
            return _domainTreeService.GetReportTypeGroup(reportTypeGroupId);
        }

        public List<ReportTypeGroupDto> GetReportTypeGroups()
        {
            return _domainTreeService.GetReportTypeGroups();
        }

        public void UpdateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto)
        {
            _domainTreeService.UpdateReportTypeGroup(reportTypeGroupDto);
        }

        public ReportTypeDto CreateReportType(ReportTypeDto reportTypeDto)
        {
            return _domainTreeService.CreateReportType(reportTypeDto);
        }

        public ReportTypeDto GetReportType(Guid reportTypeId)
        {
            return _domainTreeService.GetReportType(reportTypeId);
        }

        public List<ReportTypeDto> GetReportTypes()
        {
            return _domainTreeService.GetReportTypes();
        }

        public void UpdateReportType(ReportTypeDto reportTypeDto)
        {
            _domainTreeService.UpdateReportType(reportTypeDto);
        }

        public ReportDto CreateReport(ReportDto reportDto)
        {
            return _domainTreeService.CreateReport(reportDto);
        }

        public ReportDto GetReport(Guid reportId)
        {
            return _domainTreeService.GetReport(reportId);
        }

        public List<ReportDto> GetReports()
        {
            return _domainTreeService.GetReports();
        }

        public void UpdateReport(ReportDto reportDto)
        {
            _domainTreeService.UpdateReport(reportDto);
        }

        public List<ReportDataDto> GetReportDataByReport(Guid reportId)
        {
            return _domainTreeService.GetReportDataByReport(reportId);
        }

        public JuridicalPersonDto CreateJuridicalPerson(JuridicalPersonDto juridicalPersonDto)
        {
            return _domainTreeService.CreateJuridicalPerson(juridicalPersonDto);
        }

        public JuridicalPersonDto GetJuridicalPerson(Guid juridicalPersonId)
        {
            return _domainTreeService.GetJuridicalPerson(juridicalPersonId);
        }

        public List<JuridicalPersonDto> GetJuridicalPersons()
        {
            return _domainTreeService.GetJuridicalPersons();
        }

        public void UpdateJuridicalPerson(JuridicalPersonDto juridicalPersonDto)
        {
            _domainTreeService.UpdateJuridicalPerson(juridicalPersonDto);
        }

        public RegionDto CreateRegion(RegionDto regionDto)
        {
            return _domainTreeService.CreateRegion(regionDto);
        }

        public RegionDto GetRegion(Guid regionId)
        {
            return _domainTreeService.GetRegion(regionId);
        }

        public void UpdateRegion(RegionDto regionDto)
        {
            _domainTreeService.UpdateRegion(regionDto);
        }

        public DistrictDto CreateDistrict(DistrictDto districtDto)
        {
            return _domainTreeService.CreateDistrict(districtDto);
        }

        public DistrictDto GetDistrict(Guid districtId)
        {
            return _domainTreeService.GetDistrict(districtId);
        }

        public void UpdateDistrict(DistrictDto districtDto)
        {
            _domainTreeService.UpdateDistrict(districtDto);
        }

        public InstituteDto CreateInstitute(InstituteDto instituteDto)
        {
            return _domainTreeService.CreateInstitute(instituteDto);
        }

        public InstituteDto GetInstitute(Guid instituteId)
        {
            return _domainTreeService.GetInstitute(instituteId);
        }

        public void UpdateInstitute(InstituteDto instituteDto)
        {
            _domainTreeService.UpdateInstitute(instituteDto);
        }

        public EducationLevelDto CreateEducationLevel(EducationLevelDto educationLevelDto)
        {
            return _domainTreeService.CreateEducationLevel(educationLevelDto);
        }

        public EducationLevelDto GetEducationLevel(Guid educationLevelId)
        {
            return _domainTreeService.GetEducationLevel(educationLevelId);
        }

        public void UpdateEducationLevel(EducationLevelDto educationLevelDto)
        {
            _domainTreeService.UpdateEducationLevel(educationLevelDto);
        }

        public LocalityTypeDto CreateLocalityType(LocalityTypeDto localityTypeDto)
        {
            return _domainTreeService.CreateLocalityType(localityTypeDto);
        }

        public LocalityTypeDto GetLocalityType(Guid localityTypeId)
        {
            return _domainTreeService.GetLocalityType(localityTypeId);
        }

        public void UpdateLocalityType(LocalityTypeDto localityTypeDto)
        {
            _domainTreeService.UpdateLocalityType(localityTypeDto);
        }
    }
}

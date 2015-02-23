using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using AISSPO.Attributes;
using Common.Base;
using Common.Base.Web.Formatters;
using Domain;
using Domain.Implementation;
using DTO;
using DTO.TreeTypeDtos;
using DTO.Web;
using Ninject;

namespace AISSPO.Controllers
{
    //[IntegrationAuthorize]    
    public class TreeController : ApiController
    {
        private IKernel _kernel;
        private IDomainTreeService _treeService;

        public TreeController()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();
            _treeService = _kernel.Get<IDomainTreeService>();
        }

        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Get2")]
        public int Get2()
        {
            
            
            return _treeService.GetReportDataPageCountByReportId(new Guid("d26c8417-016c-49a1-beda-a42d0151ed5c"));
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("AuthenticateUser")]
        public PersonalUserDto AuthenticateUser(AuthenticationUserDto authenticationUser)
        {
            var user = _treeService.AuthenticateUser(authenticationUser.Login, authenticationUser.Password);

            if (user == null)
            {
                //return new DataContractJsonResult(null, JsonRequestBehavior.AllowGet);                
                return null;
            }

            var personalUserDto = new PersonalUserDto
            {
                Id = user.Id,
                ParentId = user.ParentId,
                Name = user.Name,
                ShortName = user.ShortName,
                TypeId = user.TypeId,
                StateId = user.StateId,
                StateName = _treeService.GetTree(user.StateId).Name,
                CreateDateTime = user.CreateDateTime,

            };

            if (user.PersonId != null)
            {
                var person = _treeService.GetPerson((Guid)user.PersonId);
                personalUserDto.Surname = person.Surname;
                personalUserDto.Name = person.Name;
                personalUserDto.Patronymic = person.Patronymic;
                personalUserDto.BirthDate = person.BirthDate;
                personalUserDto.Login = user.Login;
                personalUserDto.UserGroupId = user.UserGroupId;
                personalUserDto.UserGroupName = user.UserGroupName;
                personalUserDto.Email = user.Email;
                personalUserDto.Phone = user.Phone;
                personalUserDto.PersonId = (Guid)user.PersonId;
            }

            return personalUserDto;

            //return new DataContractJsonResult(personalUserDto, JsonRequestBehavior.AllowGet);
            //return Json(personalUserDto);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RegisterUser")]
        public PersonalUserDto RegisterUser(RegistrationUserDto registrationUser)
        {
            var userByLogin = _treeService.FindUserByLogin(registrationUser.Login);
            var userByEmail = _treeService.FindUserByEmail(registrationUser.Email);

            if ((userByLogin == null) || (userByEmail == null))
            {
                return null;
            }

            var user = _treeService.RegisterUser(registrationUser);

            if (user == null)
            {
                return null;
            }

            var personalUserDto = new PersonalUserDto
            {
                Id = user.Id,
                ParentId = user.ParentId,
                Name = user.Name,
                ShortName = user.ShortName,
                TypeId = user.TypeId,
                StateId = user.StateId,
                StateName = _treeService.GetTree(user.StateId).Name,
                CreateDateTime = user.CreateDateTime
            };

            if (user.PersonId != null)
            {
                var person = _treeService.GetPerson((Guid)user.PersonId);
                personalUserDto.Surname = person.Surname;
                personalUserDto.FirstName = person.FirstName;
                personalUserDto.Patronymic = person.Patronymic;
                personalUserDto.BirthDate = person.BirthDate;
            }

            return personalUserDto;
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RecoveryPassword")]
        public RecoveryPasswordDto RecoveryPassword(EmailDto email)
        {
            var user = _treeService.FindUserByEmail(email.Email);

            if (user == null)
            {
                return null;
            }

            var newPassword = PasswordHelper.CreateRandomPassword(8);

            user.Password = newPassword;

            var recoveryPasswordDto = new RecoveryPasswordDto
            {
                Login = user.Login,
                Password = newPassword
            };

            _treeService.UpdateUser(user);

            return recoveryPasswordDto;
        }

        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Check")]
        public bool Check()
        {
            //var u = CreateReportTypeGroup("Тестовая группа отчетов");

            //var authenticationUserDto = new AuthenticationUserDto
            //{
            //    Login = "user",
            //    Password = "123456"
            //};

            //var u = AuthenticateUser(authenticationUserDto);

            return true;
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateReportTypeGroup")]
        public ReportTypeGroupDto CreateReportTypeGroup(WebNameDto webNameDto)
        {
            var reportTypeGroupDto = new ReportTypeGroupDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllReportTypeGroups,
                Name = webNameDto.Name,
                ShortName = "",
                TypeId = ObjectTypes.otReportTypeGroup,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now
            };

            return _treeService.CreateReportTypeGroup(reportTypeGroupDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportTypeGroup")]
        public ReportTypeGroupDto GetReportTypeGroup(WebIdDto webIdDto)
        {
            return _treeService.GetReportTypeGroup(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportTypeGroups")]
        public List<ReportTypeGroupDto> GetReportTypeGroups()
        {
            return _treeService.GetReportTypeGroups();
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("UpdateReportTypeGroup")]
        public ReportTypeGroupDto UpdateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto)
        {
            _treeService.UpdateReportTypeGroup(reportTypeGroupDto);
            return _treeService.GetReportTypeGroup(reportTypeGroupDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReportTypeGroup")]
        public ReportTypeGroupDto DeleteReportTypeGroup(WebIdDto webIdDto)
        {
            var reportTypeGroupDto = _treeService.GetReportTypeGroup(webIdDto.Id);
            reportTypeGroupDto.StateId = ObjectStates.osDeleted;
            _treeService.UpdateReportTypeGroup(reportTypeGroupDto);
            return _treeService.GetReportTypeGroup(reportTypeGroupDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateReportType")]
        public ReportTypeDto CreateReportType(WebCreateReportTypeDto reportTypeDto)
        {
            var rtDto = new ReportTypeDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllReportTypes,
                Name = reportTypeDto.Name,
                ShortName = "",
                TypeId = ObjectTypes.otReportType,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now,
                GroupId = reportTypeDto.GroupId,
                GroupName = ""
            };

            return _treeService.CreateReportType(rtDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportType")]
        public ReportTypeDto GetReportType(WebIdDto webIdDto)
        {
            return _treeService.GetReportType(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportTypes")]
        public List<ReportTypeDto> GetReportTypes()
        {
            return _treeService.GetReportTypes();
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("UpdateReportType")]
        public ReportTypeDto UpdateReportType(WebUpdateReportTypeDto reportTypeDto)
        {
            var rtDto = _treeService.GetReportType(reportTypeDto.Id);
            rtDto.Name = reportTypeDto.Name;
            rtDto.GroupId = reportTypeDto.GroupId;
            rtDto.GroupName = _treeService.GetReportTypeGroup(reportTypeDto.GroupId).Name;

            _treeService.UpdateReportType(rtDto);
            return _treeService.GetReportType(rtDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReportType")]
        public ReportTypeDto DeleteReportType(WebIdDto webIdDto)
        {
            var reportTypeDto = _treeService.GetReportType(webIdDto.Id);
            reportTypeDto.StateId = ObjectStates.osDeleted;
            _treeService.UpdateReportType(reportTypeDto);
            return _treeService.GetReportType(reportTypeDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateJuridicalPerson")]
        public JuridicalPersonDto CreateJuridicalPerson(WebCreateJuridicalPersonDto webJuridicalPersonDto)
        {
            var juridicalPersonDto = new JuridicalPersonDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllCommonJuridicalPersons,
                Name = webJuridicalPersonDto.Name,
                ShortName = "",
                TypeId = ObjectTypes.otJuridicalPerson,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now
            };

            return _treeService.CreateJuridicalPerson(juridicalPersonDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetJuridicalPerson")]
        public JuridicalPersonDto GetJuridicalPerson(WebIdDto webIdDto)
        {
            return _treeService.GetJuridicalPerson(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetJuridicalPersons")]
        public List<JuridicalPersonDto> GetJuridicalPersons()
        {
            return _treeService.GetJuridicalPersons();
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateReport")]
        public ReportDto CreateReport(WebCreateReportDto webReportDto)
        {
            var reportDto = new ReportDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllReports,
                Name = webReportDto.Name ?? "",
                ShortName = "",
                TypeId = ObjectTypes.otDocument,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now,

                //DocumentParentId = null,
                DocumentTypeId = DocumentTypes.dtReport,
                DocumentStateId = ReportStates.rsNew,
                DocumentUserId = SystemUser.Id,
                Notes = webReportDto.Notes,

                ReportTypeId = webReportDto.ReportTypeId,
                RecipientId = webReportDto.RecipientId,
                FillingDate = webReportDto.FillingDate,
                ExpiryFillingDate = webReportDto.ExpiryFillingDate
            };

            return _treeService.CreateReport(reportDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReport")]
        public WebReportDto GetReport(WebIdDto webIdDto)
        {
            var reportDto = _treeService.GetReport(webIdDto.Id);

            var lastDocumentChangeDto = _treeService.GetLastDocumentChange(webIdDto.Id);

            var r = new WebReportDto
            {
                Id = reportDto.Id,
                ParentId = reportDto.ParentId,
                Name = reportDto.Name,
                ShortName = reportDto.ShortName,
                TypeId = reportDto.TypeId,
                StateId = reportDto.StateId,
                CreateDateTime = reportDto.CreateDateTime,

                DocumentParentId = reportDto.DocumentParentId,
                DocumentParentName = reportDto.DocumentParentName,
                DocumentTypeId = reportDto.DocumentTypeId,
                DocumentTypeName = reportDto.DocumentTypeName,
                DocumentStateId = reportDto.DocumentStateId,
                DocumentStateName = reportDto.DocumentStateName,
                DocumentUserId = reportDto.DocumentUserId,
                DocumentUserName = reportDto.DocumentUserName,
                Notes = reportDto.Notes,

                ReportTypeId = reportDto.ReportTypeId,
                ReportTypeName = reportDto.ReportTypeName,
                RecipientId = reportDto.RecipientId,
                RecipientName = reportDto.RecipientName,
                FillingDate = reportDto.FillingDate,
                ExpiryFillingDate = reportDto.ExpiryFillingDate,
            };

            if (lastDocumentChangeDto != null)
            {
                r.LastChangeUserId = lastDocumentChangeDto.DocumentUserId;
                r.LastChangeUserName = lastDocumentChangeDto.DocumentUserName;
                r.LastChangeDateTime = lastDocumentChangeDto.CreateDateTime;
            }
            else
            {
                r.LastChangeUserId = reportDto.DocumentUserId;
                r.LastChangeUserName = reportDto.DocumentUserName;
                r.LastChangeDateTime = reportDto.CreateDateTime;
            }

            return r;
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportsByType")]
        public List<ReportDto> GetReportsByType(WebIdDto webIdDto)
        {
            return _treeService.GetReportsByType(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("UpdateReport")]
        public ReportDto UpdateReport(WebUpdateReportDto webReportDto)
        {
            var reportDto = _treeService.GetReport(webReportDto.Id);

            reportDto.Name = webReportDto.Name;

            reportDto.DocumentStateId = webReportDto.DocumentStateId;
            reportDto.Notes = webReportDto.Notes;

            reportDto.ReportTypeId = webReportDto.ReportTypeId;
            reportDto.RecipientId = webReportDto.RecipientId;
            reportDto.FillingDate = webReportDto.FillingDate;
            reportDto.ExpiryFillingDate = webReportDto.ExpiryFillingDate;

            _treeService.UpdateReport(reportDto);

            return _treeService.GetReport(reportDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReport")]
        public ReportDto DeleteReport(WebIdDto webIdDto)
        {
            _treeService.DeleteReport(webIdDto.Id);
            return _treeService.GetReport(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateReportData")]
        public ReportDataDto CreateReportData(WebCreateReportDataDto webReportDataDto)
        {
            var reportDataDto = new ReportDataDto
            {
                Id = Guid.NewGuid(),
                ReportId = webReportDataDto.ReportId,
                Column = webReportDataDto.Column,
                Row = webReportDataDto.Row,
                Page = webReportDataDto.Page,
                Value = webReportDataDto.Value
            };

            return _treeService.CreateReportData(reportDataDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportData")]
        public ReportDataDto GetReportData(WebIdDto webIdDto)
        {
            return _treeService.GetReportData(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportDataByReportAndPage")]
        public List<ReportDataDto> GetReportDataByReportAndPage(WebReportDataByReportAndPageDto webReportDataDto)
        {
            return _treeService.GetReportDataByReportAndPage(webReportDataDto.ReportId, webReportDataDto.Page);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportDataPageCountByReportId")]
        public int GetReportDataPageCountByReportId(WebIdDto webIdDto)
        {
            return _treeService.GetReportDataPageCountByReportId(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("UpdateReportData")]
        public void UpdateReportData(ReportDataDto reportDataDto)
        {
            _treeService.UpdateReportData(reportDataDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReportData")]
        public void DeleteReportData(WebIdDto webIdDto)
        {
            _treeService.DeleteReportData(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReportDataByReport")]
        public void DeleteReportDataByReport(WebIdDto webIdDto)
        {
            _treeService.DeleteReportDataByReport(webIdDto.Id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("InsertOrUpdateReportDataPacket")]
        public void InsertOrUpdateReportDataPacket(List<ReportDataDto> reportDataDtos)
        {
            _treeService.InsertOrUpdateReportDataPacket(reportDataDtos);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteReportDataPacket")]
        public void DeleteReportDataPacket(List<WebIdDto> reportDataIds)
        {
            var ids = reportDataIds.Select(reportDataId => reportDataId.Id).ToList();
            _treeService.DeleteReportDataPacket(ids);
        }
    }
}

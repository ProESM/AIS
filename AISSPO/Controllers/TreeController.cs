﻿using System;
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

        [IntegrationAuthentication]
        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Get")]        
        public IEnumerable<VirtualTreeDto> Get()        
        {
            var treeDtos = _treeService.GetTrees(SystemObjects.Root, SystemObjects.Root);
            var trees = treeDtos.Select(t => new VirtualTreeDto
            {
                Id = t.Id,
                ParentId = t.ParentId,
                Name = t.Name,
                ShortName = t.ShortName,
                TypeId = t.TypeId,
                StateId = t.StateId,
                CreateDateTime = t.CreateDateTime
            }).ToList();
            return trees.AsEnumerable();
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Get2")]        
        public string Get2()
        {

            //var treeDtos = _treeService.GetTrees();
            //var trees = treeDtos.Select(t => new TreeDto
            //{
            //    Id = t.Id,
            //    ParentId = t.ParentId,
            //    Name = t.Name,
            //    ShortName = t.ShortName,
            //    TypeId = t.TypeId,
            //    StateId = t.StateId,
            //    CreateDateTime = t.CreateDateTime
            //}).ToList();
            //return Json(trees.AsEnumerable());

            return "Hello, World2!";
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
            var u = CreateReportTypeGroup("Тестовая группа отчетов");

            //var authenticationUserDto = new AuthenticationUserDto
            //{
            //    Login = "user",
            //    Password = "123456"
            //};

            //var u = AuthenticateUser(authenticationUserDto);

            return (u != null);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("CreateReportTypeGroup")]
        public ReportTypeGroupDto CreateReportTypeGroup(string name)
        {
            var reportTypeGroupDto = new ReportTypeGroupDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllReportTypeGroups,
                Name = name,
                ShortName = "",
                TypeId = ObjectTypes.otReportTypeGroup,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now
            };

            return _treeService.CreateReportTypeGroup(reportTypeGroupDto);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportTypeGroup")]
        public ReportTypeGroupDto GetReportTypeGroup(Guid id)
        {
            return _treeService.GetReportTypeGroup(id);
        }

        [IntegrationAuthentication]
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("GetReportTypeGroups")]
        public IEnumerable<ReportTypeGroupDto> GetReportTypeGroups()
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
        public ReportTypeGroupDto DeleteReportTypeGroup(Guid id)
        {
            var reportTypeGroupDto = _treeService.GetReportTypeGroup(id);
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
        public ReportTypeDto GetReportType(Guid id)
        {
            return _treeService.GetReportType(id);
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
        public ReportTypeDto DeleteReportType(Guid id)
        {
            var reportTypeDto = _treeService.GetReportType(id);
            reportTypeDto.StateId = ObjectStates.osDeleted;
            _treeService.UpdateReportType(reportTypeDto);
            return _treeService.GetReportType(reportTypeDto.Id);
        }
    }
}

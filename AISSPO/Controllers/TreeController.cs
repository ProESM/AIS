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
using Domain;
using Domain.Implementation;
using DTO;
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

        // GET api/values
        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Get")]
        [IntegrationAuthentication]
        public JsonResult<IEnumerable<VirtualTreeDto>> Get()        
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
            return Json(trees.AsEnumerable());                       
        }

        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Get2")]
        //public JsonResult<IEnumerable<TreeDto>> Get()
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
        public JsonResult<PersonalUserDto> AuthenticateUser(AuthenticationUserDto authenticationUser)
        {            
            var user = _treeService.AuthenticateUser(authenticationUser.Login, authenticationUser.Password);

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
                personalUserDto.Name = person.Name;
                personalUserDto.Patronymic = person.Patronymic;
                personalUserDto.BirthDate = person.BirthDate;
            }

            return Json(personalUserDto);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RegisterUser")]
        public JsonResult<PersonalUserDto> RegisterUser(RegistrationUserDto registrationUser)
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
                personalUserDto.Name = person.Name;
                personalUserDto.Patronymic = person.Patronymic;
                personalUserDto.BirthDate = person.BirthDate;
            }

            return Json(personalUserDto);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RecoveryPassword")]
        public JsonResult<RecoveryPasswordDto> RecoveryPassword(string email)
        {
            var user = _treeService.FindUserByEmail(email);

            var newPassword = PasswordHelper.CreateRandomPassword(8);

            var newSalt = CryptHelper.GenerateSalt(user.Login, newPassword);

            var newMd5Password = CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(newPassword) + newSalt);

            user.Password = newMd5Password;
            user.Salt = newSalt;

            var recoveryPasswordDto = new RecoveryPasswordDto
            {
                Login = user.Login,
                Password = newPassword
            };

            return Json(recoveryPasswordDto);
        }

        [System.Web.Http.HttpGet, System.Web.Http.ActionName("Check")]
        public bool Check()
        {
            var authenticationUserDto = new AuthenticationUserDto
            {
                Login = "user",
                Password = "123456"
            };

            var u = AuthenticateUser(authenticationUserDto);

            return (u != null);
        }
    }
}

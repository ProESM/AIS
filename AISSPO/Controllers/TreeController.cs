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
        public JsonResult<UserDto> AuthenticateUser(AuthenticationUserDto authenticationUser)
        {            
            var user = _treeService.AuthenticateUser(authenticationUser.Login, authenticationUser.Password);

            return Json(user);
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RegisterUser")]
        public JsonResult<UserDto> RegisterUser(RegistrationUserDto registrationUser)
        {
            var user = _treeService.RegisterUser(registrationUser);

            return Json(user);
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

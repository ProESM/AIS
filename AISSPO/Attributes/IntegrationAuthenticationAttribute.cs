using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Common.Base;
using Domain;
using Domain.Implementation;
using Ninject;

namespace AISSPO.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationAuthenticationAttribute : BasicAuthenticationAttribute
    {
        private IKernel _kernel;
        private IDomainTreeService _domainTreeService;

        public IntegrationAuthenticationAttribute()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _domainTreeService = _kernel.Get<IDomainTreeService>();
        }

        public IntegrationAuthenticationAttribute(bool active)
            : base(active)
        {
        }

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var user = _domainTreeService.FindUserByLogin(username);

            if (user == null)
                return false;

            if (CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + user.Salt) != user.Password)
                return false;

            return true;
        }
    }
}
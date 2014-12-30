using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using Common.Base;

namespace AISSPO.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class IntegrationAuthenticationAttribute : BasicAuthenticationAttribute
    {
        public IntegrationAuthenticationAttribute()
        {
        }

        public IntegrationAuthenticationAttribute(bool active)
            : base(active)
        {
        }

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var treeServiceClient = new TreeServiceReference.TreeServiceClient();

            var user = treeServiceClient.FindUserByLogin(username);

            if (user == null)
                return false;

            if (CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + user.Salt) != user.Password)
                return false;

            return true;
        }
    }
}
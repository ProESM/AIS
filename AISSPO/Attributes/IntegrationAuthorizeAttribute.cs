using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain;
using Domain.Implementation;
using Ninject;

namespace AISSPO.Attributes
{
    /// <summary>
    /// Атрибут для проверки авторизации, а также установления в куки данных о пользователе в иных внешних системах: ais и т.п.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class IntegrationAuthorizeAttribute : AuthorizeAttribute
    {
        private IKernel _kernel;
        private IDomainTreeService _domainTreeService;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // проверяем, авторизован ли пользователь
            var isAuthenticated = base.AuthorizeCore(httpContext);

            if (isAuthenticated)
            {
                // получаем имя файла Cookie, который используется для сохранения билета проверки подлинности с помощью форм.
                var cookieName = FormsAuthentication.FormsCookieName;

                if (// в случае, 
                    // если в контексте текущего HTTP-запроса пользователь не авторизован
                    !httpContext.User.Identity.IsAuthenticated //||
                    // или отсутствуют куки
                    // httpContext.Request.Cookies == null || 
                    // или отсутствует куки с именем 'cookieName'
                    // httpContext.Request.Cookies[cookieName] == null
                    )
                {
                    // считаем, что пользователь не авторизован
                    return false;
                }

                if (
                    // или отсутствуют куки
                    httpContext.Request.Cookies == null ||
                    // или отсутствует куки с именем 'cookieName'
                    httpContext.Request.Cookies["aisUserId"] == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.AddBindings();

                    _domainTreeService = _kernel.Get<IDomainTreeService>();

                    var userDTO = _domainTreeService.FindUserByLogin(httpContext.User.Identity.Name);

                    var cookie = new HttpCookie("aisUserId")
                    {
                        Value = userDTO.Id.ToString(),
                        Expires = DateTime.Now.AddMinutes(30),
                    };

                    httpContext.Request.Cookies.Add(cookie);
                }

                //// получаем куки с именем 'cookieName'
                //var authCookie = httpContext.Request.Cookies[cookieName];                
                //// создаем объект FormsAuthenticationTicket на основе шифрованного билета проверки подлинности с помощью форм, переданных методу.
                //var authTicket = FormsAuthentication.Decrypt(authCookie.Value);              

                //// This is where you can read the userData part of the authentication
                //// cookie and fetch the token
                //string webServiceToken = authTicket.UserData;

                //IPrincipal userPrincipal = ... create some custom implementation
                //                               and store the web service token as property

                //// Inject the custom principal in the HttpContext
                //httpContext.User = userPrincipal;
            }
            return isAuthenticated;
        }
    }
}
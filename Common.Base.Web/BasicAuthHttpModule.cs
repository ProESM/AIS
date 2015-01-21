using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain;
using Domain.Implementation;
using Ninject;

namespace Common.Base.Web
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private IKernel _kernel;
        private IDomainTreeService _domainTreeService;

        public BasicAuthHttpModule()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _domainTreeService = _kernel.Get<IDomainTreeService>();
        }
        
        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.AuthenticateRequest += new
                EventHandler(this.OnAuthenticateRequest);
            application.EndRequest += new
                EventHandler(this.OnEndRequest);
        }

        public void OnAuthenticateRequest(object source, EventArgs
                            eventArgs)
        {
            HttpApplication app = (HttpApplication)source;

            //string text = "init";
            //System.IO.File.WriteAllText(@"D:\text.txt", text);

            string authHeader = app.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {                
                //System.IO.File.WriteAllText(@"D:\text.txt", "1");

                string authStr = app.Request.Headers["Authorization"];

                if (authStr == null || authStr.Length == 0)
                {
                    return;
                }

                //System.IO.File.WriteAllText(@"D:\text.txt", "2");

                authStr = authStr.Trim();
                if (authStr.IndexOf("Basic", 0) != 0)
                {
                    return;
                }

                //System.IO.File.WriteAllText(@"D:\text.txt", "3");

                authStr = authStr.Trim();

                string encodedCredentials = authStr.Substring(6);

                byte[] decodedBytes =
                Convert.FromBase64String(encodedCredentials);
                string s = new UTF8Encoding().GetString(decodedBytes);

                string[] userPass = s.Split(new char[] { ':' });
                string username = userPass[0];
                string password = userPass[1];

                //System.IO.File.WriteAllText(@"D:\text.txt", string.Format("4 - {0}:{1}", username, password));

                var user = _domainTreeService.FindUserByLogin(username);

                //System.IO.File.WriteAllText(@"D:\text.txt", string.Format("5 - User = {0}:{1}", user.Login, user.Password));

                if (user == null)
                {
                    DenyAccess(app);
                    return;
                }

                //System.IO.File.WriteAllText(@"D:\text.txt", string.Format("5 - User is not null = {0}:{1}; http user = {2}:{3}", user.Login, user.Password, username, password));

                var cryptPass = CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + user.Salt);
                if (CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + user.Salt) != user.Password)
                {
                    DenyAccess(app);
                    return;
                }

                //if (!MyUserValidator.Validate(username, password))
                //{
                //    DenyAccess(app);
                //    return;
                //}
            }
            else
            {
                app.Response.StatusCode = 401;
                app.Response.End();
            }
        }
        public void OnEndRequest(object source, EventArgs eventArgs)
        {
            if (HttpContext.Current.Response.StatusCode == 401)
            {
                HttpContext context = HttpContext.Current;
                context.Response.StatusCode = 401;
                context.Response.AddHeader("WWW-Authenticate", "Basic Realm");
            }
        }

        private void DenyAccess(HttpApplication app)
        {
            app.Response.StatusCode = 401;
            app.Response.StatusDescription = "Access Denied";
            app.Response.Write("401 Access Denied");
            app.CompleteRequest();
        }
    }
}

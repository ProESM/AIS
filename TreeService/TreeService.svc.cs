using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Domain;
using Domain.Implementation;
using DTO;
using Ninject;

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
        }       

        public List<TreeDto> GetTrees()
        {
            return _domainTreeService.GetTrees();
        }

        public UserDto FindUserByLogin(string login)
        {
            return _domainTreeService.FindUserByLogin(login);
        }

        public UserDto AuthenticateUser(string login, string password)
        {
            return _domainTreeService.AuthenticateUser(login, password);
        }
    }
}

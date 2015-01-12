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

        public List<Guid> GetSystemObjects()
        {
            return _domainTreeService.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            return _domainTreeService.GetTrees(parent, treeParentType, includeParent, includeDeleted);
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
    }
}

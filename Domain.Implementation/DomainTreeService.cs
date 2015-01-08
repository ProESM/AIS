using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Base;
using DTO;
using Infrastructure;
using Infrastructure.DtoFetchers;
using Infrastructure.Entities;
using Ninject;

namespace Domain.Implementation
{    
    public class DomainTreeService : IDomainTreeService
    {
        private IKernel _kernel;
        private ITreeRepository _treeRepository;

        private readonly IDtoFetcher<TreeDao, TreeDto> _treeDtoFetcher;
        private readonly IDtoFetcher<UserDao, UserDto> _userDtoFetcher;
        private readonly IDtoFetcher<VirtualTreeDao, VirtualTreeDto> _virtualTreeDtoFetcher;

        public DomainTreeService()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _treeRepository = _kernel.Get<ITreeRepository>();
            _treeDtoFetcher = new TreeDtoFetcher(_treeRepository);
            _userDtoFetcher = new UserDtoFetcher(_treeRepository);
            _virtualTreeDtoFetcher = new VirtualTreeDtoFetcher(_treeRepository);
        }

        public UserDto FindUserByLogin(string login)
        {
            var userDaos = new List<UserDao> { _treeRepository.FindUserByLogin(login) }.AsQueryable();

            return _userDtoFetcher.Fetch(userDaos, Page.All, FetchAim.Card).FirstOrDefault(); //ToList();
        }

        public UserDto AuthenticateUser(string login, string password)
        {
            var userDto = FindUserByLogin(login);

            return userDto == null
                ? null
                : (CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + userDto.Salt) == userDto.Password
                    ? userDto
                    : null);
        }

        public UserDto RegisterUser(RegistrationUserDto registrationUser)
        {
            var userDto = FindUserByLogin("user");
            return userDto;
        }

        public List<Guid> GetSystemObjects()
        {
            return BaseDataHelper.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false)
        {
            var virtualTreeDaos = _treeRepository.GetTrees(parent, treeParentType, includeParent);

            var virtualTreeDtos = _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();

            return virtualTreeDtos;
        }

        public TreeDto CreateTree(TreeDto treeDto)
        {
            TreeDao parentDao = null;
            if (treeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)treeDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(treeDto.TypeId);
            var stateDao = _treeRepository.GetTree(treeDto.StateId);

            var treeDao = new TreeDao
            {
                _Id = Guid.NewGuid().ToString().ToUpper(),
                Parent = parentDao,
                Name = treeDto.Name,
                ShortName = treeDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            _treeRepository.CreateTree(treeDao);

            treeDto.Id = treeDao.Id;
            treeDto.CreateDateTime = treeDao.CreateDateTime;

            return treeDto;
        }

        public TreeDto GetTree(Guid treeId)
        {
            return _treeDtoFetcher.Fetch(new List<TreeDao> { _treeRepository.GetTree(treeId) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();            
        }
    }    
}

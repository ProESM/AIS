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

        public DomainTreeService()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _treeRepository = _kernel.Get<ITreeRepository>();
            _treeDtoFetcher = new TreeDtoFetcher(_treeRepository);
            _userDtoFetcher = new UserDtoFetcher(_treeRepository);
        }

        public List<TreeDto> GetTrees()
        {
            return _treeDtoFetcher.Fetch(_treeRepository.GetTrees().AsQueryable(), Page.All, FetchAim.Card).ToList();
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
                : (CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(password) + userDto.Salt) != userDto.Password
                    ? userDto
                    : null);
        }
    }
}

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
        private readonly IDtoFetcher<PersonDao, PersonDto> _personDtoFetcher;
        private readonly IDtoFetcher<VirtualTreeDao, VirtualTreeDto> _virtualTreeDtoFetcher;

        public DomainTreeService()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _treeRepository = _kernel.Get<ITreeRepository>();
            _treeDtoFetcher = new TreeDtoFetcher(_treeRepository);
            _userDtoFetcher = new UserDtoFetcher(_treeRepository);
            _personDtoFetcher = new PersonDtoFetcher(_treeRepository);
            _virtualTreeDtoFetcher = new VirtualTreeDtoFetcher(_treeRepository);
        }
        
        public List<Guid> GetSystemObjects()
        {
            return BaseDataHelper.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            var virtualTreeDaos = _treeRepository.GetTrees(parent, treeParentType, includeParent, includeDeleted);

            var virtualTreeDtos = _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();

            return virtualTreeDtos;
        }

        public List<VirtualTreeDto> GetTreeParents(Guid? parent, Guid child, Guid treeParentType,
            bool includeChild = false, bool includeDeleted = false)
        {
            var virtualTreeDaos = _treeRepository.GetTreeParents(parent, child, treeParentType, includeChild, includeDeleted);

            var virtualTreeDtos = _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();

            return virtualTreeDtos;
        }

        public List<VirtualTreeDto> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds,
            List<Guid> ignoreTypeIds, Guid? parent = null)
        {
            var virtualTreeDaos = _treeRepository.SearchTreesByText(searchText, treeParentType, typeIds, ignoreTypeIds, parent);

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
            var stateDao = _treeRepository.GetTree(ObjectStates.osInDevelopment);

            var treeDao = new TreeDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = treeDto.Name,
                ShortName = treeDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };            

            treeDao = _treeRepository.CreateTree(treeDao);

            treeDto.Id = treeDao.Id;
            treeDto.CreateDateTime = treeDao.CreateDateTime;

            return treeDto;
        }

        public TreeDto GetTree(Guid treeId)
        {
            var treeDao = _treeRepository.GetTree(treeId);
            return _treeDtoFetcher.Fetch(new List<TreeDao> { _treeRepository.GetTree(treeId) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public void UpdateTree(TreeDto treeDto)
        {
            var treeDao = _treeRepository.GetTree(treeDto.Id);
            TreeDao parentDao = null;
            if (treeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)treeDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(treeDto.TypeId);
            var stateDao = _treeRepository.GetTree(treeDto.StateId);

            treeDao.Parent = parentDao;
            treeDao.Name = treeDto.Name;
            treeDao.ShortName = treeDto.ShortName;
            treeDao.Type = typeDao;
            treeDao.State = stateDao;

            _treeRepository.UpdateTree(treeDao);
        }

        public void DeleteTree(TreeDto treeDto)
        {
            var treeDao = _treeRepository.GetTree(treeDto.Id);

            var stateDao = _treeRepository.GetTree(ObjectStates.osDeleted);
            
            treeDao.State = stateDao;

            _treeRepository.UpdateTree(treeDao);
        }

        public UserDto CreateUser(UserDto userDto)
        {
            TreeDao parentDao = null;
            if (userDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)userDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otUser);
            var stateDao = _treeRepository.GetTree(ObjectStates.osInDevelopment);

            var newSalt = CryptHelper.GenerateSalt(userDto.Login, userDto.Password);

            var newMd5Password = CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(userDto.Password) + newSalt);

            PersonDao personDao = null;
            if (userDto.PersonId != null)
            {
                personDao = _treeRepository.GetPerson((Guid)userDto.PersonId);
            }

            var userDao = new UserDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = userDto.Name,
                ShortName = userDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                Login = userDto.Login,
                Password = newMd5Password,
                Salt = newSalt,
                UserGroup = _treeRepository.GetTree(userDto.UserGroupId),
                Email = userDto.Email,
                Phone = userDto.Phone,
                Person = personDao
            };

            //var treeDao = _treeRepository.CreateTree(userDao);

            //userDto.Id = treeDao.Id;
            //userDto.CreateDateTime = treeDao.CreateDateTime;

            return _userDtoFetcher.Fetch(new List<UserDao> { _treeRepository.CreateUser(userDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public UserDto GetUser(Guid userId)
        {
            return _userDtoFetcher.Fetch(new List<UserDao> { _treeRepository.GetUser(userId) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public void UpdateUser(UserDto userDto)
        {
            var userDao = _treeRepository.GetUser(userDto.Id);

            TreeDao parentDao = null;
            if (userDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)userDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(userDto.TypeId);
            var stateDao = _treeRepository.GetTree(userDto.StateId);

            userDao.Parent = parentDao;
            userDao.Name = userDto.Name;
            userDao.ShortName = userDto.ShortName;
            userDao.Type = typeDao;
            userDao.State = stateDao;

            string newSalt;
            string newMd5Password;

            if (userDto.Password.Length == 0)
            {
                newMd5Password = userDao.Password;
                newSalt = userDao.Salt;
            }
            else
            {
                newSalt = CryptHelper.GenerateSalt(userDto.Login, userDto.Password);
                newMd5Password = CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(userDto.Password) + newSalt);    
            }            

            userDao.Login = userDto.Login;
            userDao.Password = newMd5Password;
            userDao.Salt = newSalt;
            userDao.UserGroup = _treeRepository.GetTree(userDto.UserGroupId);
            userDao.Email = userDto.Email;
            userDao.Phone = userDto.Phone;

            PersonDao personDao = null;
            if (userDto.PersonId != null)
            {
                personDao = _treeRepository.GetPerson((Guid)userDto.PersonId);
            }
            userDao.Person = personDao;

            _treeRepository.UpdateUser(userDao);
        }

        public UserDto FindUserByLogin(string login)
        {            
            var userDaos = new List<UserDao> { _treeRepository.FindUserByLogin(login) }.AsQueryable();

            return !userDaos.Any() ? null : _userDtoFetcher.Fetch(userDaos, Page.All, FetchAim.Card).FirstOrDefault();
        }

        public UserDto FindUserByEmail(string email)
        {
            var userDaos = new List<UserDao> { _treeRepository.FindUserByEmail(email) }.AsQueryable();

            return !userDaos.Any() ? null : _userDtoFetcher.Fetch(userDaos, Page.All, FetchAim.Card).FirstOrDefault();
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
            var personDao = _treeRepository.CreatePerson(new PersonDao
            {
                Id = Guid.NewGuid(),
                Parent = _treeRepository.GetTree(SystemObjects.AllPeople),
                Name = string.Format("{0} {1} {2}", registrationUser.Surname, registrationUser.FirstName, registrationUser.Patronymic),
                Type = _treeRepository.GetTree(ObjectTypes.otIndividualPerson),
                State = _treeRepository.GetTree(ObjectStates.osActive),
                CreateDateTime = DateTime.Now,
                Surname = registrationUser.Surname,
                FirstName = registrationUser.FirstName,
                Patronymic = registrationUser.Patronymic,
                BirthDate = registrationUser.BirthDate
            });

            var newSalt = CryptHelper.GenerateSalt(registrationUser.Login, registrationUser.Password);

            var newMd5Password = CryptHelper.GetMd5Hash(CryptHelper.GetMd5Hash(registrationUser.Password) + newSalt);

            var userDao = _treeRepository.CreateUser(new UserDao
            {
                Id = Guid.NewGuid(),
                Parent = _treeRepository.GetTree(SystemObjects.SystemUsers),
                Name = string.Format("{0} {1} {2}", registrationUser.Surname, registrationUser.FirstName, registrationUser.Patronymic),
                Type = _treeRepository.GetTree(ObjectTypes.otUser),
                State = _treeRepository.GetTree(ObjectStates.osActive),
                CreateDateTime = DateTime.Now,
                Login = registrationUser.Login,
                Password = newMd5Password,
                Salt = newSalt,
                UserGroup = _treeRepository.GetTree(UserGroups.ugCommonUserGroup),
                Email = registrationUser.Email,
                Phone = registrationUser.Phone,
                Person = personDao
            });

            return (personDao == null) && (userDao == null)
                ? null
                : _userDtoFetcher.Fetch(new List<UserDao> {userDao}.AsQueryable(), Page.All, FetchAim.Card)
                    .FirstOrDefault();
        }

        public PersonDto CreatePerson(PersonDto personDto)
        {
            TreeDao parentDao = null;
            if (personDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)personDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otIndividualPerson);
            var stateDao = _treeRepository.GetTree(ObjectStates.osInDevelopment);

            var personDao = new PersonDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = personDto.Name,
                ShortName = personDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                Surname = personDto.Surname,
                FirstName = personDto.FirstName,
                Patronymic = personDto.Patronymic,
                BirthDate = personDto.BirthDate                
            };

            //var treeDao = _treeRepository.CreateTree(personDao);

            //personDto.Id = treeDao.Id;
            //personDto.CreateDateTime = treeDao.CreateDateTime;

            return _personDtoFetcher.Fetch(new List<PersonDao> { _treeRepository.CreatePerson(personDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public PersonDto GetPerson(Guid personId)
        {
            return _personDtoFetcher.Fetch(new List<PersonDao> { _treeRepository.GetPerson(personId) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public void UpdatePerson(PersonDto personDto)
        {
            var personDao = _treeRepository.GetPerson(personDto.Id);

            TreeDao parentDao = null;
            if (personDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)personDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(personDto.TypeId);
            var stateDao = _treeRepository.GetTree(personDto.StateId);

            personDao.Parent = parentDao;
            personDao.Name = personDto.Name;
            personDao.ShortName = personDto.ShortName;
            personDao.Type = typeDao;
            personDao.State = stateDao;

            personDao.Surname = personDto.Surname;
            personDao.FirstName = personDto.FirstName;
            personDao.Patronymic = personDto.Patronymic;
            personDao.BirthDate = personDto.BirthDate;

            _treeRepository.UpdatePerson(personDao);
        }
    }    
}

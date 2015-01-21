using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Base;
using DTO;
using DTO.TreeTypeDtos;
using Infrastructure;
using Infrastructure.DtoFetchers;
using Infrastructure.DtoFetchers.TreeTypeDtoFetchers;
using Infrastructure.Entities;
using Infrastructure.Entities.TreeTypeDaos;
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
        private readonly IDtoFetcher<ReportTypeGroupDao, ReportTypeGroupDto> _reportTypeGroupDtoFetcher;
        private readonly IDtoFetcher<ReportTypeDao, ReportTypeDto> _reportTypeDtoFetcher;
        private readonly IDtoFetcher<DocumentTypeDao, DocumentTypeDto> _documentTypeDtoFetcher;
        private readonly IDtoFetcher<DocumentDao, DocumentDto> _documentDtoFetcher;

        public DomainTreeService()
        {
            _kernel = new StandardKernel();
            _kernel.AddBindings();

            _treeRepository = _kernel.Get<ITreeRepository>();
            _treeDtoFetcher = new TreeDtoFetcher(_treeRepository);
            _userDtoFetcher = new UserDtoFetcher(_treeRepository);
            _personDtoFetcher = new PersonDtoFetcher(_treeRepository);
            _virtualTreeDtoFetcher = new VirtualTreeDtoFetcher(_treeRepository);
            _reportTypeGroupDtoFetcher = new ReportTypeGroupDtoFetcher(_treeRepository);
            _reportTypeDtoFetcher = new ReportTypeDtoFetcher(_treeRepository);
            _documentTypeDtoFetcher = new DocumentTypeDtoFetcher(_treeRepository);
            _documentDtoFetcher = new DocumentDtoFetcher(_treeRepository);
        }

        public List<Guid> GetSystemObjects()
        {
            return BaseDataHelper.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            var virtualTreeDaos = _treeRepository.GetTrees(parent, treeParentType, includeParent, includeDeleted);

            return virtualTreeDaos == null ? null : _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public List<VirtualTreeDto> GetTreeParents(Guid? parent, Guid child, Guid treeParentType,
            bool includeChild = false, bool includeDeleted = false)
        {
            var virtualTreeDaos = _treeRepository.GetTreeParents(parent, child, treeParentType, includeChild, includeDeleted);

            return virtualTreeDaos == null ? null : _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public List<VirtualTreeDto> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds,
            List<Guid> ignoreTypeIds, Guid? parent = null)
        {
            var virtualTreeDaos = _treeRepository.SearchTreesByText(searchText, treeParentType, typeIds, ignoreTypeIds, parent);

            return virtualTreeDaos == null ? null : _virtualTreeDtoFetcher.Fetch(virtualTreeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
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

            return treeDao == null ? null : _treeDtoFetcher.Fetch(new List<TreeDao> { treeDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
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
            var userDao = _treeRepository.GetUser(userId);
            return userDao == null ? null : _userDtoFetcher.Fetch(new List<UserDao> { userDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
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

            if ((userDto.Password.Length == 0) && (userDto.Password.Length == 0))
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
            var userDao = _treeRepository.FindUserByLogin(login);

            return userDao == null ? null : _userDtoFetcher.Fetch(new List<UserDao> { userDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public UserDto FindUserByEmail(string email)
        {
            var userDao = _treeRepository.FindUserByEmail(email);

            return userDao == null ? null : _userDtoFetcher.Fetch(new List<UserDao> { userDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
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
                : _userDtoFetcher.Fetch(new List<UserDao> { userDao }.AsQueryable(), Page.All, FetchAim.Card)
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
            var personDao = _treeRepository.GetPerson(personId);

            return personDao == null ? null : _personDtoFetcher.Fetch(new List<PersonDao> { personDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
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

        public DocumentTypeDto CreateDocumentType(DocumentTypeDto documentTypeDto)
        {
            TreeDao parentDao = null;
            if (documentTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)documentTypeDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otDocumentType);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var documentTypeDao = new DocumentTypeDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = documentTypeDto.Name,
                ShortName = documentTypeDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _documentTypeDtoFetcher.Fetch(new List<DocumentTypeDao> { _treeRepository.CreateDocumentType(documentTypeDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public DocumentTypeDto GetDocumentType(Guid documentTypeId)
        {
            var documentTypeDao = _treeRepository.GetDocumentType(documentTypeId);

            return documentTypeDao == null ? null : _documentTypeDtoFetcher.Fetch(new List<DocumentTypeDao> { documentTypeDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();            
        }

        public List<DocumentTypeDto> GetDocumentTypes()
        {
            var documentTypes = _treeRepository.GetDocumentTypes();

            return documentTypes == null ? null : _documentTypeDtoFetcher.Fetch(documentTypes.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateDocumentType(DocumentTypeDto documentTypeDto)
        {
            var documentTypeDao = _treeRepository.GetDocumentType(documentTypeDto.Id);

            TreeDao parentDao = null;
            if (documentTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)documentTypeDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(documentTypeDto.TypeId);
            var stateDao = _treeRepository.GetTree(documentTypeDto.StateId);

            documentTypeDao.Parent = parentDao;
            documentTypeDao.Name = documentTypeDto.Name;
            documentTypeDao.ShortName = documentTypeDto.ShortName;
            documentTypeDao.Type = typeDao;
            documentTypeDao.State = stateDao;

            _treeRepository.UpdateDocumentType(documentTypeDao);
        }

        public DocumentDto CreateDocument(DocumentDto documentDto)
        {
            TreeDao parentDao = null;
            if (documentDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)documentDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            DocumentDao documentParentDao = null;
            if (documentDto.DocumentParentId != null)
            {
                documentParentDao = _treeRepository.GetDocument((Guid)documentDto.DocumentParentId);
            }

            var documentTypeDao = _treeRepository.GetDocumentType(documentDto.DocumentTypeId);
            var documentStateDao = _treeRepository.GetTree(documentDto.DocumentStateId);
            var documentUserDao = _treeRepository.GetUser(documentDto.DocumentUserId);

            var documentDao = new DocumentDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = documentDto.Name,
                ShortName = documentDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                DocumentParent = documentParentDao ?? null,
                DocumentType = documentTypeDao,
                DocumentState = documentStateDao,
                DocumentUser = documentUserDao,
                Notes = documentDto.Notes
            };

            return _documentDtoFetcher.Fetch(new List<DocumentDao> { _treeRepository.CreateDocument(documentDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public DocumentDto GetDocument(Guid documentId)
        {
            var documentDao = _treeRepository.GetDocument(documentId);

            return documentDao == null ? null : _documentDtoFetcher.Fetch(new List<DocumentDao> { documentDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<DocumentDto> GetDocuments()
        {
            var documentDaos = _treeRepository.GetDocuments();

            return documentDaos == null ? null : _documentDtoFetcher.Fetch(documentDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateDocument(DocumentDto documentDto)
        {
            var documentDao = _treeRepository.GetDocument(documentDto.Id);

            TreeDao parentDao = null;
            if (documentDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)documentDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            DocumentDao documentParentDao = null;
            if (documentDto.DocumentParentId != null)
            {
                documentParentDao = _treeRepository.GetDocument((Guid)documentDto.DocumentParentId);
            }

            var documentTypeDao = _treeRepository.GetDocumentType(documentDto.DocumentTypeId);
            var documentStateDao = _treeRepository.GetTree(documentDto.DocumentStateId);
            var documentUserDao = _treeRepository.GetUser(documentDto.DocumentUserId);

            documentDao.Parent = parentDao;
            documentDao.Name = documentDto.Name;
            documentDao.ShortName = documentDto.ShortName;            
            documentDao.DocumentParent = documentParentDao ?? null;
            documentDao.DocumentType = documentTypeDao;
            documentDao.DocumentState = documentStateDao;
            documentDao.DocumentUser = documentUserDao;
            documentDao.Notes = documentDto.Notes;

            _treeRepository.UpdateDocument(documentDao);
        }

        public ReportTypeGroupDto CreateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto)
        {
            TreeDao parentDao = null;
            if (reportTypeGroupDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)reportTypeGroupDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otReportTypeGroup);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var reportTypeGroup = new ReportTypeGroupDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = reportTypeGroupDto.Name,
                ShortName = reportTypeGroupDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _reportTypeGroupDtoFetcher.Fetch(new List<ReportTypeGroupDao> { _treeRepository.CreateReportTypeGroup(reportTypeGroup) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public ReportTypeGroupDto GetReportTypeGroup(Guid reportTypeGroupId)
        {
            var reportTypeGroupDao = _treeRepository.GetReportTypeGroup(reportTypeGroupId);

            return reportTypeGroupDao == null ? null : _reportTypeGroupDtoFetcher.Fetch(new List<ReportTypeGroupDao> { reportTypeGroupDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<ReportTypeGroupDto> GetReportTypeGroups()
        {
            var reportTypeGroupDaos = _treeRepository.GetReportTypeGroups();

            return reportTypeGroupDaos == null ? null : _reportTypeGroupDtoFetcher.Fetch(reportTypeGroupDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateReportTypeGroup(ReportTypeGroupDto reportTypeGroupDto)
        {
            var reportTypeGroupDao = _treeRepository.GetReportTypeGroup(reportTypeGroupDto.Id);

            TreeDao parentDao = null;
            if (reportTypeGroupDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)reportTypeGroupDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(reportTypeGroupDto.TypeId);
            var stateDao = _treeRepository.GetTree(reportTypeGroupDto.StateId);

            reportTypeGroupDao.Parent = parentDao;
            reportTypeGroupDao.Name = reportTypeGroupDto.Name;
            reportTypeGroupDao.ShortName = reportTypeGroupDto.ShortName;
            reportTypeGroupDao.Type = typeDao;
            reportTypeGroupDao.State = stateDao;

            _treeRepository.UpdateReportTypeGroup(reportTypeGroupDao);
        }

        public ReportTypeDto CreateReportType(ReportTypeDto reportTypeDto)
        {
            TreeDao parentDao = null;
            if (reportTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)reportTypeDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otReportType);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);
            var groupDao = _treeRepository.GetReportTypeGroup(reportTypeDto.GroupId);

            var reportType = new ReportTypeDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = reportTypeDto.Name,
                ShortName = reportTypeDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                Group = groupDao
            };

            return _reportTypeDtoFetcher.Fetch(new List<ReportTypeDao> { _treeRepository.CreateReportType(reportType) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public ReportTypeDto GetReportType(Guid reportTypeId)
        {
            var reportTypeDao = _treeRepository.GetReportType(reportTypeId);

            return reportTypeDao == null ? null : _reportTypeDtoFetcher.Fetch(new List<ReportTypeDao> { reportTypeDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();            
        }

        public List<ReportTypeDto> GetReportTypes()
        {
            var reportTypeDaos = _treeRepository.GetReportTypes();

            return reportTypeDaos == null ? null : _reportTypeDtoFetcher.Fetch(reportTypeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateReportType(ReportTypeDto reportTypeDto)
        {
            var reportTypeDao = _treeRepository.GetReportType(reportTypeDto.Id);

            TreeDao parentDao = null;
            if (reportTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)reportTypeDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(reportTypeDto.TypeId);
            var stateDao = _treeRepository.GetTree(reportTypeDto.StateId);
            var groupDao = _treeRepository.GetReportTypeGroup(reportTypeDto.GroupId);

            reportTypeDao.Parent = parentDao;
            reportTypeDao.Name = reportTypeDto.Name;
            reportTypeDao.ShortName = reportTypeDto.ShortName;
            reportTypeDao.Type = typeDao;
            reportTypeDao.State = stateDao;
            reportTypeDao.Group = groupDao;

            _treeRepository.UpdateReportType(reportTypeDao);
        }

        public ReportDto CreateReport(ReportDto reportDto)
        {
            return null;
        }

        public ReportDto GetReport(Guid reportId)
        {
            return null;
        }

        public List<ReportDto> GetReports()
        {
            return null;
        }

        public void UpdateReport(ReportDto reportDto)
        {
            
        }
    }   
}

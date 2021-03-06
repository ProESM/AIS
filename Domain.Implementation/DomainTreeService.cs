﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Base;
using DTO;
using DTO.TreeTypeDtos;
using DTO.Web;
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
        private readonly IDtoFetcher<ReportDao, ReportDto> _reportDtoFetcher;
        private readonly IDtoFetcher<JuridicalPersonDao, JuridicalPersonDto> _juridicalPersonDtoFetcher;
        private readonly IDtoFetcher<ReportDataDao, ReportDataDto> _reportDataDtoFetcher;
        private readonly IDtoFetcher<RegionDao, RegionDto> _regionDtoFetcher;
        private readonly IDtoFetcher<DistrictDao, DistrictDto> _districtDtoFetcher;
        private readonly IDtoFetcher<InstituteDao, InstituteDto> _instituteDtoFetcher;
        private readonly IDtoFetcher<EducationLevelDao, EducationLevelDto> _educationLevelDtoFetcher;
        private readonly IDtoFetcher<LocalityTypeDao, LocalityTypeDto> _localityTypeDtoFetcher;

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
            _reportDtoFetcher = new ReportDtoFetcher(_treeRepository);
            _juridicalPersonDtoFetcher = new JuridicalPersonDtoFetcher(_treeRepository);
            _reportDataDtoFetcher = new ReportDataDtoFetcher(_treeRepository);
            _regionDtoFetcher = new RegionDtoFetcher(_treeRepository);
            _districtDtoFetcher = new DistrictDtoFetcher(_treeRepository);
            _instituteDtoFetcher = new InstiteDtoFetcher(_treeRepository);
            _educationLevelDtoFetcher = new EducationLevelDtoFetcher(_treeRepository);
            _localityTypeDtoFetcher = new LocalityTypeDtoFetcher(_treeRepository);
        }

        public List<Guid> GetSystemObjects()
        {
            return BaseDataHelper.GetSystemObjects();
        }

        public List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false, bool includeSubChildren = false)
        {
            var virtualTreeDaos = _treeRepository.GetTrees(parent, treeParentType, includeParent, includeDeleted, includeSubChildren);

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
                DocumentParent = documentParentDao,
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

        public DocumentDto GetLastDocumentChange(Guid documentId)
        {
            var documentDao = _treeRepository.GetLastDocumentChange(documentId);

            return documentDao == null ? null : _documentDtoFetcher.Fetch(new List<DocumentDao> { documentDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
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
            documentDao.DocumentParent = documentParentDao;
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
            TreeDao parentDao = null;
            if (reportDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)reportDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            DocumentDao documentParentDao = null;
            if (reportDto.DocumentParentId != null)
            {
                documentParentDao = _treeRepository.GetDocument((Guid)reportDto.DocumentParentId);
            }

            var documentTypeDao = _treeRepository.GetDocumentType(reportDto.DocumentTypeId);
            var documentStateDao = _treeRepository.GetTree(reportDto.DocumentStateId);
            var documentUserDao = _treeRepository.GetUser(reportDto.DocumentUserId);
            var reportTypeDao = _treeRepository.GetReportType(reportDto.ReportTypeId);
            var recipientDao = _treeRepository.GetTree(reportDto.RecipientId);

            var reportDao = new ReportDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = reportDto.Name,
                ShortName = reportDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                DocumentParent = null,//documentParentDao ?? null,
                DocumentType = documentTypeDao,
                DocumentState = documentStateDao,
                DocumentUser = documentUserDao,
                Notes = reportDto.Notes,
                ReportType = reportTypeDao,
                Recipient = recipientDao,
                FillingDate = reportDto.FillingDate,
                ExpiryFillingDate = reportDto.ExpiryFillingDate
            };

            return _reportDtoFetcher.Fetch(new List<ReportDao> { _treeRepository.CreateReport(reportDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public ReportDto GetReport(Guid reportId)
        {
            var reportDao = _treeRepository.GetReport(reportId);

            return reportDao == null ? null : _reportDtoFetcher.Fetch(new List<ReportDao> { reportDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<ReportDto> GetReports()
        {
            var reportDaos = _treeRepository.GetReports();

            return reportDaos == null ? null : _reportDtoFetcher.Fetch(reportDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public List<ReportDto> GetReportsByType(Guid reportTypeId)
        {
            var reportDaos = _treeRepository.GetReportsByType(reportTypeId);

            return reportDaos == null ? null : _reportDtoFetcher.Fetch(reportDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateReport(ReportDto reportDto)
        {
            var reportDao = _treeRepository.GetReport(reportDto.Id);

            var oldReportDto = _reportDtoFetcher.Fetch(new List<ReportDao> { reportDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();

            if (!oldReportDto.CompareFull(reportDto))
            {
                var canUpdateReport = true;

                if (!oldReportDto.CompareState(reportDto))//(reportDao.DocumentStateId != reportDto.DocumentStateId)
                {
                    // Подготавливаем запись об измении реквизитов отчетов. Для этого создаем документ с типом "Изменение реквизитов отчета"
                    var changeStateParentDao = _treeRepository.GetTree(SystemObjects.AllDocumentChanges);
                    var changeStateTypeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
                    var changeStateStateDao = _treeRepository.GetTree(ObjectStates.osActive);
                    var changeStateDocumentTypeDao = _treeRepository.GetDocumentType(DocumentTypes.dtReportChangeState);

                    var changeStateDao = new DocumentDao
                    {
                        Id = Guid.NewGuid(),
                        Parent = changeStateParentDao,
                        Name = string.Format("Изменение состояния отчета '{0}'", reportDao.Name),
                        ShortName = string.Empty,
                        Type = changeStateTypeDao,
                        State = changeStateStateDao,
                        CreateDateTime = DateTime.Now,
                        DocumentParent = reportDao,
                        DocumentType = changeStateDocumentTypeDao,
                        DocumentState = reportDao.DocumentState,
                        DocumentUser = reportDao.DocumentUser,
                        Notes = reportDao.Notes
                    };

                    canUpdateReport = _treeRepository.CreateDocument(changeStateDao) != null;

                    if (canUpdateReport)
                    {
                        var documentStateDao = _treeRepository.GetTree(reportDto.DocumentStateId);
                        reportDao.DocumentState = documentStateDao;
                    }
                }

                if (canUpdateReport)
                {
                    if (!oldReportDto.CompareDetails(reportDto))
                    {
                        TreeDao parentDao = null;
                        if (reportDto.ParentId != null)
                        {
                            parentDao = _treeRepository.GetTree((Guid)reportDto.ParentId);
                        }

                        var typeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
                        var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

                        DocumentDao documentParentDao = null;
                        if (reportDto.DocumentParentId != null)
                        {
                            documentParentDao = _treeRepository.GetDocument((Guid)reportDto.DocumentParentId);
                        }

                        var documentTypeDao = _treeRepository.GetDocumentType(reportDto.DocumentTypeId);
                        var documentUserDao = _treeRepository.GetUser(reportDto.DocumentUserId);
                        var reportTypeDao = _treeRepository.GetReportType(reportDto.ReportTypeId);
                        var recipientDao = _treeRepository.GetTree(reportDto.RecipientId);

                        reportDao.Parent = parentDao;
                        reportDao.Name = reportDto.Name;
                        reportDao.ShortName = reportDto.ShortName;
                        reportDao.DocumentParent = documentParentDao;
                        reportDao.DocumentType = documentTypeDao;

                        reportDao.DocumentUser = documentUserDao;
                        reportDao.Notes = reportDto.Notes;

                        reportDao.ReportType = reportTypeDao;
                        reportDao.Recipient = recipientDao;
                        reportDao.FillingDate = reportDto.FillingDate;
                        reportDao.ExpiryFillingDate = reportDto.ExpiryFillingDate;

                        // Подготавливаем запись об измении реквизитов отчетов. Для этого создаем документ с типом "Изменение реквизитов отчета"
                        var changeDetailsParentDao = _treeRepository.GetTree(SystemObjects.AllDocumentChanges);
                        var changeDetailsTypeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
                        var changeDetailsStateDao = _treeRepository.GetTree(ObjectStates.osActive);
                        var changeDetailsDocumentTypeDao =
                            _treeRepository.GetDocumentType(DocumentTypes.dtReportChangeDetails);

                        var changeDetailsDao = new DocumentDao
                        {
                            Id = Guid.NewGuid(),
                            Parent = changeDetailsParentDao,
                            Name = string.Format("Изменение реквизитов отчета '{0}'", reportDao.Name),
                            ShortName = string.Empty,
                            Type = changeDetailsTypeDao,
                            State = changeDetailsStateDao,
                            CreateDateTime = DateTime.Now,
                            DocumentParent = reportDao,
                            DocumentType = changeDetailsDocumentTypeDao,
                            DocumentState = reportDao.DocumentState,
                            DocumentUser = reportDao.DocumentUser,
                            Notes = reportDao.Notes
                        };

                        canUpdateReport = _treeRepository.CreateDocument(changeDetailsDao) != null;
                    }
                }

                if (canUpdateReport)
                {
                    _treeRepository.UpdateReport(reportDao);
                }
            }
        }

        public void UpdateReportState(Guid reportId, Guid newStateId)
        {
            var reportDao = _treeRepository.GetReport(reportId);

            if (reportDao.DocumentStateId != newStateId)
            {
                var newStateDao = _treeRepository.GetTree(newStateId);

                reportDao.DocumentState = newStateDao;

                // Подготавливаем запись об измении реквизитов отчетов. Для этого создаем документ с типом "Изменение реквизитов отчета"
                var cParentDao = _treeRepository.GetTree(SystemObjects.AllDocumentChanges);
                var cTypeDao = _treeRepository.GetTree(ObjectTypes.otDocument);
                var cStateDao = _treeRepository.GetTree(ObjectStates.osActive);

                var cDao = new DocumentDao
                {
                    Id = Guid.NewGuid(),
                    Parent = cParentDao,
                    Name = string.Format("Изменение состояния отчета '{0}'", reportDao.Name),
                    ShortName = string.Empty,
                    Type = cTypeDao,
                    State = cStateDao,
                    CreateDateTime = DateTime.Now,
                    DocumentParent = reportDao,
                    DocumentType = reportDao.DocumentType,
                    DocumentState = reportDao.DocumentState,
                    DocumentUser = reportDao.DocumentUser,
                    Notes = reportDao.Notes
                };

                if (_treeRepository.CreateDocument(cDao) != null)
                {
                    _treeRepository.UpdateReport(reportDao);
                }
            }
        }

        public void DeleteReport(Guid reportId)
        {
            var reportDao = _treeRepository.GetReport(reportId);
            var stateDao = _treeRepository.GetTree(ObjectStates.osDeleted);
            reportDao.State = stateDao;

            _treeRepository.UpdateReport(reportDao);
        }

        public JuridicalPersonDto CreateJuridicalPerson(JuridicalPersonDto juridicalPersonDto)
        {
            TreeDao parentDao = null;
            if (juridicalPersonDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)juridicalPersonDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otJuridicalPerson);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var juridicalPersonDao = new JuridicalPersonDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = juridicalPersonDto.Name,
                ShortName = juridicalPersonDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _juridicalPersonDtoFetcher.Fetch(new List<JuridicalPersonDao> { _treeRepository.CreateJuridicalPerson(juridicalPersonDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public JuridicalPersonDto GetJuridicalPerson(Guid juridicalPersonId)
        {
            var juridicalPersonDao = _treeRepository.GetJuridicalPerson(juridicalPersonId);

            return juridicalPersonDao == null ? null : _juridicalPersonDtoFetcher.Fetch(new List<JuridicalPersonDao> { juridicalPersonDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<JuridicalPersonDto> GetJuridicalPersons()
        {
            var juridicalPersonDaos = _treeRepository.GetJuridicalPersons();

            return juridicalPersonDaos == null ? null : _juridicalPersonDtoFetcher.Fetch(juridicalPersonDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateJuridicalPerson(JuridicalPersonDto juridicalPersonDto)
        {
            var juridicalPersonDao = _treeRepository.GetJuridicalPerson(juridicalPersonDto.Id);

            TreeDao parentDao = null;
            if (juridicalPersonDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)juridicalPersonDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(juridicalPersonDto.TypeId);
            var stateDao = _treeRepository.GetTree(juridicalPersonDto.StateId);

            juridicalPersonDao.Parent = parentDao;
            juridicalPersonDao.Name = juridicalPersonDto.Name;
            juridicalPersonDao.ShortName = juridicalPersonDto.ShortName;
            juridicalPersonDao.Type = typeDao;
            juridicalPersonDao.State = stateDao;

            _treeRepository.UpdateJuridicalPerson(juridicalPersonDao);
        }

        public ReportDataDto CreateReportData(ReportDataDto reportDataDto)
        {
            var reportDao = _treeRepository.GetReport(reportDataDto.ReportId);

            var reportDataDao = new ReportDataDao
            {
                Id = Guid.NewGuid(),
                Report = reportDao,
                Column = reportDataDto.Column,
                Row = reportDataDto.Row,
                Page = reportDataDto.Page,
                Value = reportDataDto.Value
            };

            return _reportDataDtoFetcher.Fetch(new List<ReportDataDao> { _treeRepository.CreateReportData(reportDataDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public ReportDataDto GetReportData(Guid reportDataId)
        {
            var reportDataDao = _treeRepository.GetReportData(reportDataId);

            return reportDataDao == null ? null : _reportDataDtoFetcher.Fetch(new List<ReportDataDao> { reportDataDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<ReportDataDto> GetReportDataByReportAndPage(Guid reportId, int page)
        {
            var reportDataDaos = _treeRepository.GetReportDataByReportAndPage(reportId, page);

            return reportDataDaos == null ? null : _reportDataDtoFetcher.Fetch(reportDataDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public List<ReportDataDto> GetReportDataByReport(Guid reportId)
        {
            var reportDataDaos = _treeRepository.GetReportDataByReport(reportId);

            return reportDataDaos == null ? null : _reportDataDtoFetcher.Fetch(reportDataDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public int GetReportDataPageCountByReportId(Guid reportId)
        {
            return _treeRepository.GetReportDataPageCountByReportId(reportId);
        }

        public void UpdateReportData(ReportDataDto reportDataDto)
        {
            var reportDataDao = _treeRepository.GetReportData(reportDataDto.Id);

            var reportDao = _treeRepository.GetReport(reportDataDto.ReportId);

            reportDataDao.Report = reportDao;
            reportDataDao.Column = reportDataDto.Column;
            reportDataDao.Row = reportDataDto.Row;
            reportDataDao.Page = reportDataDto.Page;
            reportDataDao.Value = reportDataDto.Value;

            _treeRepository.UpdateReportData(reportDataDao);
        }

        public void DeleteReportData(Guid reportDataId)
        {
            _treeRepository.DeleteReportData(reportDataId);
        }

        public void DeleteReportDataByReport(Guid reportId)
        {
            _treeRepository.DeleteReportDataByReport(reportId);
        }

        public void InsertOrUpdateReportDataPacket(List<ReportDataDto> reportDataDtos)
        {
            foreach (var reportDataDto in reportDataDtos)
            {
                if (reportDataDto.Id == Guid.Empty)
                {
                    var reportDao = _treeRepository.GetReport(reportDataDto.ReportId);

                    var reportDataDao = new ReportDataDao
                    {
                        Id = Guid.NewGuid(),
                        Report = reportDao,
                        Column = reportDataDto.Column,
                        Row = reportDataDto.Row,
                        Page = reportDataDto.Page,
                        Value = reportDataDto.Value
                    };

                    _treeRepository.CreateReportData(reportDataDao);
                }
                else
                {
                    var reportDataDao = _treeRepository.GetReportData(reportDataDto.Id);

                    var reportDao = _treeRepository.GetReport(reportDataDto.ReportId);

                    reportDataDao.Report = reportDao;
                    reportDataDao.Column = reportDataDto.Column;
                    reportDataDao.Row = reportDataDto.Row;
                    reportDataDao.Page = reportDataDto.Page;
                    reportDataDao.Value = reportDataDto.Value;

                    _treeRepository.UpdateReportData(reportDataDao);
                }
            }
        }

        public void DeleteReportDataPacket(List<Guid> reportDataIds)
        {
            foreach (var reportDataId in reportDataIds)
            {
                _treeRepository.DeleteReportData(reportDataId);
            }
        }

        public RegionDto CreateRegion(RegionDto regionDto)
        {
            TreeDao parentDao = null;
            if (regionDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)regionDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otRegion);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var regionDao = new RegionDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = regionDto.Name,
                ShortName = regionDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _regionDtoFetcher.Fetch(new List<RegionDao> { _treeRepository.CreateRegion(regionDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public RegionDto GetRegion(Guid regionId)
        {
            var regionDao = _treeRepository.GetRegion(regionId);

            return regionDao == null ? null : _regionDtoFetcher.Fetch(new List<RegionDao> { regionDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<RegionDto> GetRegions()
        {
            var regionDaos = _treeRepository.GetRegions();

            return regionDaos == null ? null : _regionDtoFetcher.Fetch(regionDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateRegion(RegionDto regionDto)
        {
            var regionDao = _treeRepository.GetRegion(regionDto.Id);

            TreeDao parentDao = null;
            if (regionDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)regionDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(regionDto.TypeId);
            var stateDao = _treeRepository.GetTree(regionDto.StateId);

            regionDao.Parent = parentDao;
            regionDao.Name = regionDto.Name;
            regionDao.ShortName = regionDto.ShortName;
            regionDao.Type = typeDao;
            regionDao.State = stateDao;

            _treeRepository.UpdateRegion(regionDao);
        }

        public DistrictDto CreateDistrict(DistrictDto districtDto)
        {
            TreeDao parentDao = null;
            if (districtDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)districtDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otDistrict);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);
            var regionDao = _treeRepository.GetRegion(districtDto.RegionId);

            var districtDao = new DistrictDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = districtDto.Name,
                ShortName = districtDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                Region = regionDao
            };

            return _districtDtoFetcher.Fetch(new List<DistrictDao> { _treeRepository.CreateDistrict(districtDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public DistrictDto GetDistrict(Guid districtId)
        {
            var districtDao = _treeRepository.GetDistrict(districtId);

            return districtDao == null ? null : _districtDtoFetcher.Fetch(new List<DistrictDao> { districtDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<DistrictDto> GetDistricts()
        {
            var districtDaos = _treeRepository.GetDistricts();

            return districtDaos == null ? null : _districtDtoFetcher.Fetch(districtDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateDistrict(DistrictDto districtDto)
        {
            var districtDao = _treeRepository.GetDistrict(districtDto.Id);

            TreeDao parentDao = null;
            if (districtDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)districtDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(districtDto.TypeId);
            var stateDao = _treeRepository.GetTree(districtDto.StateId);
            var regionDao = _treeRepository.GetRegion(districtDto.RegionId);

            districtDao.Parent = parentDao;
            districtDao.Name = districtDto.Name;
            districtDao.ShortName = districtDto.ShortName;
            districtDao.Type = typeDao;
            districtDao.State = stateDao;
            districtDao.Region = regionDao;

            _treeRepository.UpdateDistrict(districtDao);
        }

        public InstituteDto CreateInstitute(InstituteDto instituteDto)
        {
            TreeDao parentDao = null;
            if (instituteDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)instituteDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otJuridicalPerson);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);
            var districtDao = _treeRepository.GetDistrict(instituteDto.LocationId);
            var educationLevelDao = _treeRepository.GetEducationLevel(instituteDto.EducationLevelId);
            var localityTypeDao = _treeRepository.GetLocalityType(instituteDto.LocalityTypeId);

            var instituteDao = new InstituteDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = instituteDto.Name,
                ShortName = instituteDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now,
                Location = districtDao,
                EducationLevel = educationLevelDao,
                LocalityType = localityTypeDao
            };

            return _instituteDtoFetcher.Fetch(new List<InstituteDao> { _treeRepository.CreateInstitute(instituteDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public InstituteDto GetInstitute(Guid instituteId)
        {
            var instituteDao = _treeRepository.GetInstitute(instituteId);

            return instituteDao == null ? null : _instituteDtoFetcher.Fetch(new List<InstituteDao> { instituteDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<InstituteDto> GetInstitutes()
        {
            var instituteDaos = _treeRepository.GetInstitutes();
            return instituteDaos == null ? null : _instituteDtoFetcher.Fetch(instituteDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();            
        }

        public void UpdateInstitute(InstituteDto instituteDto)
        {
            var instituteDao = _treeRepository.GetInstitute(instituteDto.Id);

            TreeDao parentDao = null;
            if (instituteDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)instituteDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(instituteDto.TypeId);
            var stateDao = _treeRepository.GetTree(instituteDto.StateId);
            var districtDao = _treeRepository.GetRegion(instituteDto.LocationId);
            var educationLevelDao = _treeRepository.GetEducationLevel(instituteDto.EducationLevelId);
            var localityTypeDao = _treeRepository.GetLocalityType(instituteDto.LocalityTypeId);

            instituteDao.Parent = parentDao;
            instituteDao.Name = instituteDto.Name;
            instituteDao.ShortName = instituteDto.ShortName;
            instituteDao.Type = typeDao;
            instituteDao.State = stateDao;
            instituteDao.Location = districtDao;
            instituteDao.EducationLevel = educationLevelDao;
            instituteDao.LocalityType = localityTypeDao;

            _treeRepository.UpdateInstitute(instituteDao);
        }

        public EducationLevelDto CreateEducationLevel(EducationLevelDto educationLevelDto)
        {
            TreeDao parentDao = null;
            if (educationLevelDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)educationLevelDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otEducationLevel);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var educationLevelDao = new EducationLevelDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = educationLevelDto.Name,
                ShortName = educationLevelDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _educationLevelDtoFetcher.Fetch(new List<EducationLevelDao> { _treeRepository.CreateEducationLevel(educationLevelDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public EducationLevelDto GetEducationLevel(Guid educationLevelId)
        {
            var educationLevelDao = _treeRepository.GetEducationLevel(educationLevelId);

            return educationLevelDao == null ? null : _educationLevelDtoFetcher.Fetch(new List<EducationLevelDao> { educationLevelDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<EducationLevelDto> GetEducationLevels()
        {
            var educationLevelDaos = _treeRepository.GetEducationLevels();
            return educationLevelDaos == null ? null : _educationLevelDtoFetcher.Fetch(educationLevelDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateEducationLevel(EducationLevelDto educationLevelDto)
        {
            var educationLevelDao = _treeRepository.GetEducationLevel(educationLevelDto.Id);

            TreeDao parentDao = null;
            if (educationLevelDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)educationLevelDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(educationLevelDto.TypeId);
            var stateDao = _treeRepository.GetTree(educationLevelDto.StateId);

            educationLevelDao.Parent = parentDao;
            educationLevelDao.Name = educationLevelDto.Name;
            educationLevelDao.ShortName = educationLevelDto.ShortName;
            educationLevelDao.Type = typeDao;
            educationLevelDao.State = stateDao;

            _treeRepository.UpdateEducationLevel(educationLevelDao);
        }

        public LocalityTypeDto CreateLocalityType(LocalityTypeDto localityTypeDto)
        {
            TreeDao parentDao = null;
            if (localityTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)localityTypeDto.ParentId);
            }

            var typeDao = _treeRepository.GetTree(ObjectTypes.otEducationLevel);
            var stateDao = _treeRepository.GetTree(ObjectStates.osActive);

            var localityTypeDao = new LocalityTypeDao
            {
                Id = Guid.NewGuid(),
                Parent = parentDao,
                Name = localityTypeDto.Name,
                ShortName = localityTypeDto.ShortName,
                Type = typeDao,
                State = stateDao,
                CreateDateTime = DateTime.Now
            };

            return _localityTypeDtoFetcher.Fetch(new List<LocalityTypeDao> { _treeRepository.CreateLocalityType(localityTypeDao) }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public LocalityTypeDto GetLocalityType(Guid localityTypeId)
        {
            var localityTypeDao = _treeRepository.GetLocalityType(localityTypeId);

            return localityTypeDao == null ? null : _localityTypeDtoFetcher.Fetch(new List<LocalityTypeDao> { localityTypeDao }.AsQueryable(), Page.All, FetchAim.Card).FirstOrDefault();
        }

        public List<LocalityTypeDto> GetLocalityTypes()
        {
            var localityTypeDaos = _treeRepository.GetLocalityTypes();
            return localityTypeDaos == null ? null : _localityTypeDtoFetcher.Fetch(localityTypeDaos.AsQueryable(), Page.All, FetchAim.Card).ToList();
        }

        public void UpdateLocalityType(LocalityTypeDto localityTypeDto)
        {
            var localityTypeDao = _treeRepository.GetLocalityType(localityTypeDto.Id);

            TreeDao parentDao = null;
            if (localityTypeDto.ParentId != null)
            {
                parentDao = _treeRepository.GetTree((Guid)localityTypeDto.ParentId);
            }
            var typeDao = _treeRepository.GetTree(localityTypeDto.TypeId);
            var stateDao = _treeRepository.GetTree(localityTypeDto.StateId);

            localityTypeDao.Parent = parentDao;
            localityTypeDao.Name = localityTypeDto.Name;
            localityTypeDao.ShortName = localityTypeDto.ShortName;
            localityTypeDao.Type = typeDao;
            localityTypeDao.State = stateDao;

            _treeRepository.UpdateLocalityType(localityTypeDao);
        }
    }
}

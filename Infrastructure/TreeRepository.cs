using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using Infrastructure.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure
{
    public class TreeRepository : ITreeRepository
    {
        private readonly ISession _session;

        public TreeRepository()
        {
            _session = SessionManager.CurrentSession;
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }                

        public List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            using (var transaction = _session.BeginTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (includeParent)
                {
                    if (includeDeleted)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .Where(tp => tp.TreeChild.State._Id != ObjectStates.osDeleted.ToString().ToUpper())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();   
                    }                    
                }
                else
                {
                    if (includeDeleted)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .Where(tp => tp.TreeChild.State._Id != ObjectStates.osDeleted.ToString().ToUpper())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    
                }

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent._Id == treeParentDao.TreeChild._Id.ToUpper())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        _Id = treeParentDao.TreeChild._Id,
                        Parent = treeParentDao.TreeChild.Parent,
                        Name = treeParentDao.TreeChild.Name,
                        ShortName = treeParentDao.TreeChild.ShortName,
                        Type = treeParentDao.TreeChild.Type,
                        State = treeParentDao.TreeChild.State,
                        CreateDateTime = treeParentDao.TreeChild.CreateDateTime,
                        HasChildren = _treeParentDaos.Count > 0
                    });
                }                

                return virtualTreeDaos;
            }
        }

        public List<VirtualTreeDao> GetTreeParents(Guid? parent, Guid child, Guid treeParentType,
            bool includeChild = false, bool includeDeleted = false)
        {
            using (var transaction = _session.BeginTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (parent != null)
                {
                    var firstOrDefault = _session.Query<TreeParentDao>()
                        .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper()).FirstOrDefault(tp => tp.TreeChild._Id == child.ToString().ToUpper());
                    if (firstOrDefault != null)
                    {
                        var limitLevel = firstOrDefault.Level;

                        if (includeChild)
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild._Id == child.ToString().ToUpper())
                                .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                                .Where(tp => tp.Level <= limitLevel)
                                .OrderByDescending(tp => tp.Level)
                                .ToList();
                        }
                        else
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild._Id == child.ToString().ToUpper())
                                .Where(tp => tp.TreeParent._Id == child.ToString().ToUpper())
                                .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                                .Where(tp => tp.Level <= limitLevel)
                                .OrderByDescending(tp => tp.Level)
                                .ToList();
                        }
                    }
                    else
                    {
                        treeParentDaos = new List<TreeParentDao>();
                    }
                }
                else
                {
                    if (includeChild)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeChild._Id == child.ToString().ToUpper())
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeChild._Id == child.ToString().ToUpper())
                            .Where(tp => tp.TreeParent._Id == child.ToString().ToUpper())
                            .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                }                

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent._Id == treeParentDao.TreeChild._Id.ToUpper())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        _Id = treeParentDao.TreeChild._Id,
                        Parent = treeParentDao.TreeChild.Parent,
                        Name = treeParentDao.TreeChild.Name,
                        ShortName = treeParentDao.TreeChild.ShortName,
                        Type = treeParentDao.TreeChild.Type,
                        State = treeParentDao.TreeChild.State,
                        CreateDateTime = treeParentDao.TreeChild.CreateDateTime,
                        HasChildren = _treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }

        public TreeDao CreateTree(TreeDao treeDao)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(treeDao);

                transaction.Commit();

                return _session.Query<TreeDao>().FirstOrDefault(t => t._Id == treeDao._Id.ToUpper());
            }            
        }

        public TreeDao GetTree(Guid treeId)
        {
            using (var transaction = _session.BeginTransaction())
            {
                return _session.Query<TreeDao>().FirstOrDefault(t => t._Id == treeId.ToString().ToUpper());   
            }
        }

        public void UpdateTree(TreeDao treeDao)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(treeDao);
                transaction.Commit();
            }
        }

        public UserDao CreateUser(UserDao userDao)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(userDao);

                transaction.Commit();

                return _session.Query<UserDao>().FirstOrDefault(p => p._Id == userDao._Id.ToUpper());
            }
        }

        public UserDao GetUser(Guid userId)
        {
            using (var transaction = _session.BeginTransaction())
            {
                return _session.Query<UserDao>().FirstOrDefault(u => u._Id == userId.ToString().ToUpper());
            }
        }

        public void UpdateUser(UserDao userDao)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(userDao);
                transaction.Commit();
            }
        }

        public UserDao FindUserByLogin(string login)
        {
            var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Login == login);
            return userDao;
        }

        public UserDao FindUserByEmail(string email)
        {
            var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Email == email);
            return userDao;
        }

        public PersonDao CreatePerson(PersonDao personDao)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(personDao);

                transaction.Commit();

                return _session.Query<PersonDao>().FirstOrDefault(p => p._Id == personDao._Id.ToUpper());
            }
        }

        public PersonDao GetPerson(Guid personId)
        {
            using (var transaction = _session.BeginTransaction())
            {
                return _session.Query<PersonDao>().FirstOrDefault(p => p._Id == personId.ToString().ToUpper());
            }
        }
    }
}

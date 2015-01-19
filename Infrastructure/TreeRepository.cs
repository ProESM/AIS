using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using FluentNHibernate.Conventions;
using Infrastructure.Entities;
using Infrastructure.Extensions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Infrastructure
{
    public class TreeRepository : ITreeRepository
    {
        private readonly ISession _session;

        public TreeRepository()
        {
            _session = SessionManager.CurrentSession;
            _session.FlushMode = FlushMode.Always;
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }        

        public List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false)
        {
            using (var transaction = _session.StartTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (includeParent)
                {
                    if (includeDeleted)
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level <= 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .Where(tp => tp.TreeChild.State.Id.ToString() != ObjectStates.osDeleted.ToString())
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
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString())
                            .Where(tp => tp.Level == 1)
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .Where(tp => tp.TreeChild.State.Id.ToString() != ObjectStates.osDeleted.ToString())
                            .OrderBy(tp => tp.Level)
                            .ThenBy(tp => tp.TreeChild.Name)
                            .ToList();
                    }
                    
                }

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeParentDao.TreeChild.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeParentDao.TreeChild.Id,
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
            using (var transaction = _session.StartTransaction())
            {
                List<TreeParentDao> treeParentDaos;

                if (parent != null)
                {
                    var firstOrDefault = _session.Query<TreeParentDao>()
                        .Where(tp => tp.TreeParent.Id.ToString() == parent.ToString()).FirstOrDefault(tp => tp.TreeChild.Id.ToString() == child.ToString());
                    if (firstOrDefault != null)
                    {
                        var limitLevel = firstOrDefault.Level;

                        if (includeChild)
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                                .Where(tp => tp.Level <= limitLevel)
                                .OrderByDescending(tp => tp.Level)
                                .ToList();
                        }
                        else
                        {
                            treeParentDaos = _session.Query<TreeParentDao>()
                                .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParent.Id.ToString() == child.ToString())
                                .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
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
                            .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                    else
                    {
                        treeParentDaos = _session.Query<TreeParentDao>()
                            .Where(tp => tp.TreeChild.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParent.Id.ToString() == child.ToString())
                            .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                            .OrderByDescending(tp => tp.Level)
                            .ToList();
                    }
                }                

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeParentDao in treeParentDaos)
                {
                    var _treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeParentDao.TreeParent.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeParentDao.TreeParent.Id,
                        Parent = treeParentDao.TreeParent.Parent,
                        Name = treeParentDao.TreeParent.Name,
                        ShortName = treeParentDao.TreeParent.ShortName,
                        Type = treeParentDao.TreeParent.Type,
                        State = treeParentDao.TreeParent.State,
                        CreateDateTime = treeParentDao.TreeParent.CreateDateTime,
                        HasChildren = _treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }
        
        public List<VirtualTreeDao> SearchTreesByText(string searchText, Guid treeParentType, List<Guid> typeIds,
            List<Guid> ignoreTypeIds, Guid? parent = null)
        {
            using (var transaction = _session.StartTransaction())
            {
                ICriteria criteria;                

                if (parent == null)
                {                    
                    if (typeIds.IsEmpty())
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }
                        
                    }
                    else
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }                        
                    }

                    

                    //.Where(t => (t.Type.Id.IsIn(typeIds) || !typeIds.Any()))
                    //        //.Where(t => (!t.Type.Id.IsIn(ignoreTypeIds) || !ignoreTypeIds.Any()))     
                    

                    //var subCriteria = DetachedCriteria.For<TreeParentDao>();
                    //subCriteria.SetProjection(Projections.Property("TreeChild"))
                        //.Add(Restrictions.Eq("TreeParentTypeId", treeParentType));
                        ;

                    //var criteria = _session.CreateCriteria(typeof(TreeDao))
                        //.Add(Restrictions.Like("Name", searchText, MatchMode.Anywhere))
                        //.Add(Subqueries.PropertyIn("_Id", subCriteria));

                    //treeDaos = criteria.List<TreeDao>().ToList();

//                    var someComplexQuery = @"select 
//                                                distinct
//                                                lt.* 
//                                             from
//                                                l_tree lt
//                                             join l_tree_parents ltp on ltp.tree_child_id=lt.id and ltp.tree_parent_id=:treeParentType
//                                             where
//                                                lt.name containing :searchText
//                                            ";}

//                    treeDaos = _session.CreateSQLQuery(someComplexQuery)
//                        .SetParameter("treeParentType", treeParentType.ToString(), NHibernateUtil.String)
//                        .SetParameter("searchText", searchText, NHibernateUtil.String)
//                        .SetResultTransformer(Transformers.AliasToBean(typeof(TreeDao)))
//                        .List<TreeDao>().ToList();

                    //treeParentDaos = _session.QueryOver<TreeParentDao>()
                    //    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString())
                    //    .Where(tp => tp.Level == 0)
                    //    .JoinQueryOver(t => t.TreeChild)
                    //        .Where(t => t.Name.IsLike(searchText, MatchMode.Anywhere))
                    //        //.Where(t => (t.Type.Id.IsIn(typeIds) || !typeIds.Any()))
                    //        //.Where(t => (!t.Type.Id.IsIn(ignoreTypeIds) || !ignoreTypeIds.Any()))                        
                    //    .List().ToList();
                }
                else
                {
                    if (typeIds.IsEmpty())
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }

                    }
                    else
                    {
                        if (ignoreTypeIds.IsEmpty())
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds));
                        }
                        else
                        {
                            criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                                .Add(Restrictions.Eq("TreeParent.Id", parent))
                                .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                                .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                                    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere))
                                    .Add(Restrictions.In("lt.Type.Id", typeIds))
                                    .Add(Restrictions.Not(Restrictions.In("lt.Type.Id", ignoreTypeIds)));
                        }
                    }

                    //criteria = _session.CreateCriteria<TreeParentDao>("ltp")
                    //    .Add(Restrictions.Eq("TreeChild.Id", parent))
                    //    .Add(Restrictions.Eq("TreeParentType.Id", treeParentType))
                    //    .CreateCriteria("ltp.TreeChild", "lt", JoinType.InnerJoin)
                    //    .Add(Restrictions.Like("lt.Name", searchText, MatchMode.Anywhere));
                    
                    //treeParentDaos = _session.QueryOver<TreeParentDao>()
                    //    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString())
                    //    .Where(tp => tp.TreeParent._Id == parent.ToString())
                    //    .JoinQueryOver(t => t.TreeChild)
                    //        .Where(t => t.Name.Contains(searchText))
                    //    .List().ToList();
                    //treeParentDaos = _session.QueryOver<TreeParentDao>()
                    //    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString())
                    //    .Where(tp => tp.TreeParent._Id == parent.ToString())
                    //    .JoinQueryOver(t => t.TreeChild)
                    //        .Where(t => t.Name.Contains(searchText))
                    //    .List<TreeDao>().ToList();
                }

                var treeDaos = criteria.List<TreeParentDao>().Select(ltp => ltp.TreeChild).Distinct().ToList();

                var virtualTreeDaos = new List<VirtualTreeDao>();

                foreach (var treeDao in treeDaos)
                {
                    var treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent.Id.ToString() == treeDao.Id.ToString())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType.Id.ToString() == treeParentType.ToString())
                    .ToList();

                    virtualTreeDaos.Add(new VirtualTreeDao
                    {
                        Id = treeDao.Id,
                        Parent = treeDao.Parent,
                        Name = treeDao.Name,
                        ShortName = treeDao.ShortName,
                        Type = treeDao.Type,
                        State = treeDao.State,
                        CreateDateTime = treeDao.CreateDateTime,
                        HasChildren = treeParentDaos.Count > 0
                    });
                }

                return virtualTreeDaos;
            }
        }

        public TreeDao CreateTree(TreeDao treeDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(treeDao);

                transaction.Commit();                

                return _session.Query<TreeDao>().FirstOrDefault(t => t.Id.ToString() == treeDao.Id.ToString());
            }
        }

        public TreeDao GetTree(Guid treeId)
        {
            using (var transaction = _session.StartTransaction())
            {
                //return _session.Query<TreeDao>().FirstOrDefault(t => t._Id == treeId.ToString());   
                return _session.Query<TreeDao>().FirstOrDefault(t => t.Id.ToString() == treeId.ToString());
            }
        }

        public void UpdateTree(TreeDao treeDao)
        {
            using (var transaction = _session.StartTransaction())
            {                
                _session.SaveOrUpdate(treeDao);                
                transaction.Commit();
            }
        }

        public UserDao CreateUser(UserDao userDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(userDao);

                transaction.Commit();

                return _session.Query<UserDao>().FirstOrDefault(p => p.Id.ToString() == userDao.Id.ToString());
            }
        }

        public UserDao GetUser(Guid userId)
        {
            using (var transaction = _session.StartTransaction())
            {                                
                return _session.Query<UserDao>().FirstOrDefault(u => u.Id.ToString() == userId.ToString());                
            }
        }

        public void UpdateUser(UserDao userDao)
        {
            using (var transaction = _session.StartTransaction())
            {                
                _session.SaveOrUpdate(userDao);                
                transaction.Commit();                
            }                        
        }

        public UserDao FindUserByLogin(string login)
        {
            using (var transaction = _session.StartTransaction())
            {                
                var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Login == login);
                return userDao;
            }
        }

        public UserDao FindUserByEmail(string email)
        {
            using (var transaction = _session.StartTransaction())
            {                
                var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Email == email);
                return userDao;
            }
        }

        public PersonDao CreatePerson(PersonDao personDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.Save(personDao);

                transaction.Commit();

                return _session.Query<PersonDao>().FirstOrDefault(p => p.Id.ToString() == personDao.Id.ToString());
            }
        }

        public PersonDao GetPerson(Guid personId)
        {
            using (var transaction = _session.StartTransaction())
            {                
                return _session.Query<PersonDao>().FirstOrDefault(p => p.Id.ToString() == personId.ToString());
            }
        }

        public void UpdatePerson(PersonDao personDao)
        {
            using (var transaction = _session.StartTransaction())
            {
                _session.SaveOrUpdate(personDao);
                transaction.Commit();
            } 
        }
    }
}

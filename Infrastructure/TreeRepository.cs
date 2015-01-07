using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public UserDao FindUserByLogin(string login)
        {            
            var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Login == login);
            return userDao;            
        }

        public List<VirtualTreeDao> GetTreesByParent(Guid? parent, Guid treeParentType, bool includeParent = false)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var treeParentDaos = _session.Query<TreeParentDao>()
                    .Where(tp => tp.TreeParent._Id == parent.ToString().ToUpper())
                    .Where(tp => tp.Level == 1)
                    .Where(tp => tp.TreeParentType._Id == treeParentType.ToString().ToUpper())
                    .ToList();

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
                        HasChildren = (_treeParentDaos != null) && (_treeParentDaos.Count > 0)
                    });
                }                

                return virtualTreeDaos;
            }
        }
    }
}

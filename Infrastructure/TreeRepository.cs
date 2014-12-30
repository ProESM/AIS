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

        public List<TreeDao> GetTrees()
        {
            using (var transaction = _session.BeginTransaction())
            {
                var treeDaos = _session.Query<TreeDao>().Where(t => t._Id != null).OrderBy(t => t.CreateDateTime);                

                return treeDaos.ToList();
            }
        }

        public UserDao FindUserByLogin(string login)
        {            
            var userDao = _session.Query<UserDao>().FirstOrDefault(u => u.Login == login);
            return userDao;            
        }
    }
}

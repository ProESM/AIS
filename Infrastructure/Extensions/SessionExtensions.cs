using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using NHibernate;

namespace Infrastructure.Extensions
{
    public static class SessionExtensions
    {
        public static ITransaction StartTransaction(this ISession session)
        {
            var transaction = session.BeginTransaction();
            session.Clear();
            session.CreateSQLQuery(string.Format("select rdb$set_context('USER_TRANSACTION','USER_ID','{0}') from rdb$database", SystemUser.Id)).List();
            return transaction;
        }
    }
}

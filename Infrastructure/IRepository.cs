using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Возвращает LINQ-запрос.
        /// </summary>
        /// <typeparam name="T">Тип сущности, по которой создается запрос</typeparam>
        /// <returns>LINQ-запрос</returns>
        IQueryable<T> Query<T>();
    }
}

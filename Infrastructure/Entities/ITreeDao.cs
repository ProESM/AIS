using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Базовый интерфейс для доступа к объекту общего дерева в базе данных
    /// </summary>
    public interface ITreeDao : IEntity
    {
        /// <summary>
        /// Идентификатор лицензии
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Id родительского объекта
        /// </summary>
        TreeDao Parent { get; set; }
        /// <summary>
        /// Наименование объекта
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Короткое имя объекта
        /// </summary>
        string ShortName { get; set; }
        /// <summary>
        /// Тип объекта
        /// </summary>
        TreeDao Type { get; set; }
        /// <summary>
        /// Состояние объекта
        /// </summary>
        TreeDao State { get; set; }        
        /// <summary>
        /// Дата и время создание объекта
        /// </summary>
        DateTime CreateDateTime { get; set; }
    }
}

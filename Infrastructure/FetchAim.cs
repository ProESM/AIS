using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Цель извлечения DTO.
    /// </summary>
    public enum FetchAim
    {
        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        None,

        /// <summary>
        /// Карточка
        /// </summary>
        Card,

        /// <summary>
        /// Список
        /// </summary>
        List,

        /// <summary>
        /// Индекс
        /// </summary>
        Index
    }
}

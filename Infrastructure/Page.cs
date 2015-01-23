using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Информация о странице.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Все элементы.
        /// </summary>
        public static readonly Page All = null;

        /// <summary>
        /// Номер первого элемента на странице.
        /// </summary>
        public int FirstItemNum { get; set; }

        /// <summary>
        /// Количество элементов.
        /// </summary>
        public int Count { get; set; }

        public Page(int firstItemNum, int count)
        {
            FirstItemNum = firstItemNum;
            Count = count;
        }
    }
}

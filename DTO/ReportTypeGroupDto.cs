using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// Класс DTO для хранения информации о группе типов отчетов.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportTypeGroupDto : BaseTreeDto
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities.TreeTypeDaos;

namespace Infrastructure.Entities
{
    public class InstituteDao : JuridicalPersonDao
    {
        /// <summary>
        /// Id населенного пункта, района, региона
        /// </summary>
        public virtual Guid LocationId
        {
            get { return Location.Id; }
        }
        /// <summary>
        /// Населенный пункт, район, регион
        /// </summary>
        public virtual TreeDao Location { get; set; }
        /// <summary>
        /// Id уровня профессионального образования
        /// </summary>
        public virtual Guid EducationLevelId
        {
            get { return EducationLevel.Id; }
        }
        /// <summary>
        /// Уровень профессионального образования
        /// </summary>
        public virtual EducationLevelDao EducationLevel { get; set; }
        /// <summary>
        /// Id типа населенного пункта (город, село)
        /// </summary>
        public virtual Guid LocalityTypeId
        {
            get { return LocalityType.Id; }
        }
        /// <summary>
        /// Тип населенного пункта (город, село)
        /// </summary>
        public virtual LocalityTypeDao LocalityType { get; set; }
    }
}

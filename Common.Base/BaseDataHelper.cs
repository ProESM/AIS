using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    public class BaseDataHelper
    {
        public static bool IsSystemObject(Guid objectId)
        {
            if (typeof(SystemObjects).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
            {
                return true;
            }
            if (typeof(ObjectStates).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
            {
                return true;
            }
            if (typeof(ObjectTypes).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
            {
                return true;
            }
            return false;
        }

        public static List<Guid> GetSystemObjects()
        {
            var systemObjects = typeof (SystemObjects).GetFields().Select(fieldInfo => (Guid) fieldInfo.GetValue(fieldInfo)).ToList();

            systemObjects.AddRange(typeof (ObjectStates).GetFields().Select(fieldInfo => (Guid) fieldInfo.GetValue(fieldInfo)));

            systemObjects.AddRange(typeof (ObjectTypes).GetFields().Select(fieldInfo => (Guid) fieldInfo.GetValue(fieldInfo)));

            return systemObjects;
        }
    }

    public static class SystemObjects
    {
        /// <summary>
        /// Корень (родитель) всех объектов
        /// </summary>
        [Display(Name = "Корень (родитель) всех объектов")]
        public static readonly Guid Root = new Guid("C034E889-3B80-42D3-BDAD-5F4E729A905B");                                                     
        /// <summary>
        /// Настройки системы
        /// </summary>
        [Display(Name = "Настройки системы")]
        public static readonly Guid SystemSettings = new Guid("0322648B-488A-40C0-9CEE-B3A4F9B572EF");
        /// <summary>
        /// Служебные состояния дерева объектов
        /// </summary>
        [Display(Name = "Все служебные состояния дерева объектов")]
        public static readonly Guid SystemObjectStates = new Guid("FB782982-8E78-4DD0-B678-DDC24AF694FC");
        /// <summary>
        /// Все типы объектов
        /// </summary>
        [Display(Name = "Все типы объектов")]
        public static readonly Guid SystemObjectTypes = new Guid("F8984CD8-3409-4C19-8FCE-85A5BD2E3161");
        /// <summary>
        /// Служебные состояния дерева объектов
        /// </summary>
        [Display(Name = "Общие состояния дерева объектов")]
        public static readonly Guid CommonObjectStates = new Guid("CFEEADC1-D140-4CA3-AC94-588A4D33C12B");
        /// <summary>
        /// Все пользователи системы
        /// </summary>
        [Display(Name = "Все пользователи системы")]
        public static readonly Guid SystemUsers = new Guid("DB19CDE4-3B97-4422-B9FF-7F3F9E8DC179");
    }

    /// <summary>
    /// Состояния объектов дерева
    /// </summary>    
    public static class ObjectStates
    {
        /// <summary>
        /// Активен
        /// </summary>
        [Display(Name = "Активен")]
        public static readonly Guid osActive = new Guid("63B43B53-DD11-4283-BE14-099A6C33EF49");

        /// <summary>
        /// В разработке
        /// </summary>
        [Display(Name = "В разработке")]
        public static readonly Guid osInDevelopment = new Guid("F37FF71C-D144-4B67-A411-7216E2FBD328");

        /// <summary>
        /// Заблокирован
        /// </summary>
        [Display(Name = "Заблокирован")]
        public static readonly Guid osBlocked = new Guid("A20417F7-7BC2-4DDD-8F8D-A486FDBFB1CF");

        /// <summary>
        /// Удален
        /// </summary>
        [Display(Name = "Удален")]
        public static readonly Guid osDeleted = new Guid("339A6D38-7A40-4F79-BD03-760D470C9258");
    }

    /// <summary>
    /// Типы объектов дерева
    /// </summary>
    public static class ObjectTypes
    {
        /// <summary>
        /// Папка
        /// </summary>
        [Display(Name = "Папка")]
        public static readonly Guid otFolder = new Guid("A15D22B8-B18D-48FD-8964-DC91E4F7652B");

        /// <summary>
        /// Класс
        /// </summary>
        [Display(Name = "Класс")]
        public static readonly Guid otClass = new Guid("77FF88F5-B741-459E-BCE6-DACC83EC1FE3");

        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Display(Name = "Состояние объекта")]
        public static readonly Guid otState = new Guid("99F326AC-A722-4C81-9D00-87CFF1EA5F25");

        /// <summary>
        /// Тип объекта
        /// </summary>
        [Display(Name = "Тип объекта")]
        public static readonly Guid otType = new Guid("B840752E-5C79-49FF-B985-ACD3862E4530");

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public static readonly Guid otUser = new Guid("1FA126C9-1A28-42C2-B3C6-D7629511907F");
        
        /// <summary>
        /// Группа пользователей
        /// </summary>
        [Display(Name = "Группа пользователей")]
        public static readonly Guid otUserGroup = new Guid("E78FA512-E203-435B-BE3F-7B84DE2320C7");
    }   
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    [DataContract]
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
            if (typeof(UserGroups).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
            {
                return true;
            }
            if (typeof(DocumentTypes).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
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

            systemObjects.AddRange(typeof(UserGroups).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)));

            systemObjects.AddRange(typeof(DocumentTypes).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)));

            return systemObjects;
        }
    }

    /// <summary>
    /// Данные о пользователе, вошедшем в систему
    /// </summary>
    public static class SystemUser
    {
        [ThreadStatic]
        public static Guid Id;
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
        public static readonly Guid SystemUsers = new Guid("540BC313-F0A3-412C-857B-0157F23C601B");
        /// <summary>
        /// Все группы пользователей
        /// </summary>
        [Display(Name = "Все группы пользователей")]
        public static readonly Guid UserGroups = new Guid("20F9B9CE-8769-4569-AE71-1ECF18BE90B3");
        /// <summary>
        /// Все люди (физические лица)
        /// </summary>
        [Display(Name = "Все люди (физические лица)")]
        public static readonly Guid AllPeople = new Guid("E756FE32-EB84-4A79-BC3B-B73971F25BB4");
        /// <summary>
        /// Настройки документооборота
        /// </summary>
        [Display(Name = "Настройки документооборота")]
        public static readonly Guid DocumentWorkflowSettings = new Guid("5c9248a2-d473-4a99-a43a-a4240039a6ab");
        /// <summary>
        /// Все группы типов отчетов
        /// </summary>
        [Display(Name = "Все группы типов отчетов")]
        public static readonly Guid AllReportTypeGroups = new Guid("cfe82c85-73b8-4de2-8cdf-a4240048d161");
        /// <summary>
        /// Все типы документов
        /// </summary>
        [Display(Name = "Все типы документов")]
        public static readonly Guid AllDocumentTypes = new Guid("bfb342d1-ea82-4858-8855-a42400405fca");
        /// <summary>
        /// Все типы отчетов
        /// </summary>
        [Display(Name = "Все типы отчетов")]
        public static readonly Guid AllReportTypes = new Guid("8ac93783-ed20-4e80-b1e2-a4240042535c");
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
        public static readonly Guid otUser = new Guid("A92B6B05-D6B7-47F6-880D-78C784D4FAD5");
        
        /// <summary>
        /// Группа пользователей
        /// </summary>
        [Display(Name = "Группа пользователей")]
        public static readonly Guid otUserGroup = new Guid("D43FAAA2-427C-412F-B92C-11B78EA7BE9B");

        /// <summary>
        /// Человек (физическое лицо)
        /// </summary>
        [Display(Name = "Человек (физическое лицо)")]
        public static readonly Guid otIndividualPerson = new Guid("E815F4FF-0DE5-4D39-AFAA-95BC9D72B9C7");

        /// <summary>
        /// Документы, отчеты, изменения документов и т.п.
        /// </summary>
        [Display(Name = "Документы, отчеты, изменения документов и т.п.")]
        public static readonly Guid otDocument = new Guid("4C4B28AE-578B-4358-99EA-2860139ABD85");

        /// <summary>
        /// Тип документа
        /// </summary>
        [Display(Name = "Тип документа")]
        public static readonly Guid otDocumentType = new Guid("91463768-D5E2-4DCD-A125-AC4B60FE7B51");

        /// <summary>
        /// Группа типов отчетов
        /// </summary>
        [Display(Name = "Тип отчетов")]
        public static readonly Guid otReportType = new Guid("0A1087D5-AB77-449D-9677-4B825E80E847");

        /// <summary>
        /// Группа типов отчетов
        /// </summary>
        [Display(Name = "Группа типов отчетов")]
        public static readonly Guid otReportTypeGroup = new Guid("AD4AAA57-A71B-48A9-AD8A-9542A3D8711F");

        /// <summary>
        /// Юридическое лицо
        /// </summary>
        [Display(Name = "Юридическое лицо")]
        public static readonly Guid otJuridicalPerson = new Guid("88AD30FC-92A1-4EC3-B168-C9FED4CA2C9D");
    }

    /// <summary>
    /// Группы пользователей
    /// </summary>    
    public static class UserGroups
    {
        /// <summary>
        /// Системный архитектор
        /// </summary>
        [Display(Name = "Системный архитектор")]
        public static readonly Guid ugArchitect = new Guid("85034FD6-4F23-4F0B-8FE4-56F4EFD17FB1");

        /// <summary>
        /// Общая группа пользователей
        /// </summary>
        [Display(Name = "Общая группа пользователей")]
        public static readonly Guid ugCommonUserGroup = new Guid("E1977546-F3C3-47CD-BD23-BC05EC3F58FC");
    }

    /// <summary>
    /// Типы документов
    /// </summary>
    public static class DocumentTypes
    {
        public static readonly Guid dtReport = new Guid("79cf409a-1897-416d-95cd-f64e342b8312");
    }
}

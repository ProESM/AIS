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
            if (typeof(ReportStates).GetFields().Select(fieldInfo => fieldInfo.GetValue(fieldInfo)).Any(fieldValue => (Guid)fieldValue == objectId))
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

            systemObjects.AddRange(typeof(ReportStates).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)));

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
        public static readonly Guid AllPeople = new Guid("e756fe32-eb84-4a79-bc3b-b73971f25bb4");
        /// <summary>
        /// Общие физические лица
        /// </summary>
        [Display(Name = "Общие физические лица")]
        public static readonly Guid AllCommonPeople = new Guid("9e012798-95c7-4d71-acfc-a42500143ca0");
        /// <summary>
        /// Системные физические лица
        /// </summary>
        [Display(Name = "Системные физические лица")]
        public static readonly Guid AllSystemPeople = new Guid("db8da5e6-b002-4d69-aa70-a42c00e395db");
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
        /// Все типы документов и их изменений
        /// </summary>
        [Display(Name = "Все типы документов и их изменений")]
        public static readonly Guid AllDocumentAndDocumentChangeTypes = new Guid("bfb342d1-ea82-4858-8855-a42400405fca");
        /// <summary>
        /// Все типы документов
        /// </summary>
        [Display(Name = "Все типы документов")]
        public static readonly Guid AllDocumentTypes = new Guid("74fe0fca-0f3a-46c4-bd1e-a42800f1325b");
        /// <summary>
        /// Все типы изменений документов
        /// </summary>
        [Display(Name = "Все типы изменений документов")]
        public static readonly Guid AllDocumentChangeTypes = new Guid("1aef999d-164d-4725-87bf-a42800f7f564");
        /// <summary>
        /// Все типы изменений отчетов
        /// </summary>
        [Display(Name = "Все типы изменений отчетов")]
        public static readonly Guid AllReportChangeTypes = new Guid("c82dd2c4-efee-41c3-8bd8-a42800f82acf");
        /// <summary>
        /// Все типы отчетов
        /// </summary>
        [Display(Name = "Все типы отчетов")]
        public static readonly Guid AllReportTypes = new Guid("8ac93783-ed20-4e80-b1e2-a4240042535c");
        /// <summary>
        /// Все состояния документов
        /// </summary>
        [Display(Name = "Все состояния документов")]
        public static readonly Guid AllDocumentStates = new Guid("8d3c9325-7b95-4d24-9037-a42800cd0a9c");
        /// <summary>
        /// Все состояния отчетов
        /// </summary>
        [Display(Name = "Все состояния отчетов")]
        public static readonly Guid AllReportStates = new Guid("70613b46-d228-431b-996a-a42800dcedee");
        /// <summary>
        /// Все документы
        /// </summary>
        [Display(Name = "Все документы")]
        public static readonly Guid AllDocuments = new Guid("d77ae2b8-25ff-40e6-9163-a4240048fad7");
        /// <summary>
        /// Все изменения документов, отчетов и т.п.
        /// </summary>
        [Display(Name = "Все изменения документов, отчетов и т.п.")]
        public static readonly Guid AllDocumentChanges = new Guid("8f4bc2e3-1739-411e-994c-a424004e4635");
        /// <summary>
        /// Все отчеты
        /// </summary>
        [Display(Name = "Все отчеты")]
        public static readonly Guid AllReports = new Guid("c8f7fb12-938e-4d13-ae2f-a4240049085e");
        /// <summary>
        /// Все юридические лица
        /// </summary>
        [Display(Name = "Все юридические лица")]
        public static readonly Guid AllJuridicalPersons = new Guid("2829d1d6-69bb-4305-aab2-a42900e2676a");
        /// <summary>
        /// Общие юридические лица
        /// </summary>
        [Display(Name = "Общие юридические лица")]
        public static readonly Guid AllCommonJuridicalPersons = new Guid("698cb357-96b0-49f1-a672-a42900e29f14");
        /// <summary>
        /// Служебные юридические лица
        /// </summary>
        [Display(Name = "Служебные юридические лица")]
        public static readonly Guid AllSystemJuridicalPersons = new Guid("e633b705-5132-40d9-9d9e-a42900e2b429");
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
        public static readonly Guid otDocument = new Guid("4c4b28ae-578b-4358-99ea-2860139abd85");
        
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
        public static readonly Guid otJuridicalPerson = new Guid("88ad30fc-92a1-4ec3-b168-c9fed4ca2c9d");
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
        /// <summary>
        /// Отчет
        /// </summary>
        public static readonly Guid dtReport = new Guid("79cf409a-1897-416d-95cd-f64e342b8312");
        /// <summary>
        /// Изменение реквизитов отчета
        /// </summary>
        public static readonly Guid dtReportChangeDetails = new Guid("f7683dc1-5c0b-44d1-91bb-a42800f0776d");
        /// <summary>
        /// Изменение данных в отчете
        /// </summary>
        public static readonly Guid dtReportChangeData = new Guid("e515cdba-3680-4b83-a2fb-a428010fc0c9");
        /// <summary>
        /// Изменение состояния отчета
        /// </summary>
        public static readonly Guid dtReportChangeState = new Guid("25d98903-06da-4283-83e4-a428011197f7");
    }

    /// <summary>
    /// Состояния документов с типом "отчет"
    /// </summary>    
    public static class ReportStates
    {
        /// <summary>
        /// Создан
        /// </summary>
        [Display(Name = "Создан")]
        public static readonly Guid rsCreated = new Guid("8260de6e-58a6-4004-84fa-a42800de4837");

        /// <summary>
        /// Заполнен
        /// </summary>
        [Display(Name = "Заполнен")]
        public static readonly Guid rsFilled = new Guid("37b959eb-f8e5-4ea6-97ed-a4290130d736");        
    }
}

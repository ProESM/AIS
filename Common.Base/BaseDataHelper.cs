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
            var systemObjects = typeof(SystemObjects).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)).ToList();

            systemObjects.AddRange(typeof(ObjectStates).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)));

            systemObjects.AddRange(typeof(ObjectTypes).GetFields().Select(fieldInfo => (Guid)fieldInfo.GetValue(fieldInfo)));

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
        public static readonly Guid Root = new Guid("c034e889-3b80-42d3-bdad-5f4e729a905b");
        /// <summary>
        /// Настройки системы
        /// </summary>
        [Display(Name = "Настройки системы")]
        public static readonly Guid SystemSettings = new Guid("0322648b-488a-40c0-9cee-b3a4f9b572ef");
        /// <summary>
        /// Служебные состояния дерева объектов
        /// </summary>
        [Display(Name = "Все служебные состояния дерева объектов")]
        public static readonly Guid SystemObjectStates = new Guid("fb782982-8e78-4dd0-b678-ddc24af694fc");
        /// <summary>
        /// Все типы объектов
        /// </summary>
        [Display(Name = "Все типы объектов")]
        public static readonly Guid SystemObjectTypes = new Guid("f8984cd8-3409-4c19-8fce-85a5bd2e3161");
        /// <summary>
        /// Служебные состояния дерева объектов
        /// </summary>
        [Display(Name = "Общие состояния дерева объектов")]
        public static readonly Guid CommonObjectStates = new Guid("cfeeadc1-d140-4ca3-ac94-588a4d33c12b");
        /// <summary>
        /// Все пользователи системы
        /// </summary>
        [Display(Name = "Все пользователи системы")]
        public static readonly Guid SystemUsers = new Guid("540bc313-f0a3-412c-857b-0157f23c601b");
        /// <summary>
        /// Все группы пользователей
        /// </summary>
        [Display(Name = "Все группы пользователей")]
        public static readonly Guid UserGroups = new Guid("20f9b9ce-8769-4569-ae71-1ecf18be90b3");
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
        /// Внешние юридические лица
        /// </summary>
        [Display(Name = "Внешние юридические лица")]
        public static readonly Guid AllForeignJuridicalPersons = new Guid("88d8e28b-bf25-4615-9d51-a44500fefc14");
        /// <summary>
        /// Все образовательные учреждения
        /// </summary>
        [Display(Name = "Все образовательные учреждения")]
        public static readonly Guid AllInstituteJuridicalPersons = new Guid("952ed885-29f8-4f39-937c-a445010afe4b");
        /// <summary>
        /// Служебные юридические лица
        /// </summary>
        [Display(Name = "Служебные юридические лица")]
        public static readonly Guid AllSystemJuridicalPersons = new Guid("e633b705-5132-40d9-9d9e-a42900e2b429");

        /// <summary>
        /// Административно-территориальное деление
        /// </summary>
        [Display(Name = "Административно-территориальное деление")]
        public static readonly Guid AdministrativeTerritorialDivision = new Guid("33d8c2e0-3426-42e9-a703-a444016952c5");

        /// <summary>
        /// Все регионы
        /// </summary>
        [Display(Name = "Все регионы")]
        public static readonly Guid AllRegions = new Guid("b07e85a7-fcb4-4a8d-8b9d-a4440169b873");

        /// <summary>
        /// Все районы
        /// </summary>
        [Display(Name = "Все районы")]
        public static readonly Guid AllDistricts = new Guid("44cad9bb-e933-4e16-a2de-a444016a4e9d");

        /// <summary>
        /// Все уровни профессионального образования
        /// </summary>
        [Display(Name = "Все уровни профессионального образования")]
        public static readonly Guid AllEducationLevels = new Guid("3e1b842e-d1d3-45ba-b345-a44500fe346d");

        /// <summary>
        /// Типы населенных пунктов (город, село)
        /// </summary>
        [Display(Name = "Типы населенных пунктов (город, село)")]
        public static readonly Guid AllLocalityTypes = new Guid("f777406d-6ae7-4163-9fc9-a44500fe474b");
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
        public static readonly Guid osActive = new Guid("63b43b53-dd11-4283-be14-099a6c33ef49");

        /// <summary>
        /// В разработке
        /// </summary>
        [Display(Name = "В разработке")]
        public static readonly Guid osInDevelopment = new Guid("f37ff71c-d144-4b67-a411-7216e2fbd328");

        /// <summary>
        /// Заблокирован
        /// </summary>
        [Display(Name = "Заблокирован")]
        public static readonly Guid osBlocked = new Guid("a20417f7-7bc2-4ddd-8f8d-a486fdbfb1cf");

        /// <summary>
        /// Удален
        /// </summary>
        [Display(Name = "Удален")]
        public static readonly Guid osDeleted = new Guid("339a6d38-7a40-4f79-bd03-760d470c9258");
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
        public static readonly Guid otFolder = new Guid("a15d22b8-b18d-48fd-8964-dc91e4f7652b");

        /// <summary>
        /// Класс
        /// </summary>
        [Display(Name = "Класс")]
        public static readonly Guid otClass = new Guid("77ff88f5-b741-459e-bce6-dacc83ec1fe3");

        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Display(Name = "Состояние объекта")]
        public static readonly Guid otState = new Guid("99f326ac-a722-4c81-9d00-87cff1ea5f25");

        /// <summary>
        /// Тип объекта
        /// </summary>
        [Display(Name = "Тип объекта")]
        public static readonly Guid otType = new Guid("b840752e-5c79-49ff-b985-acd3862e4530");

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public static readonly Guid otUser = new Guid("a92b6b05-d6b7-47f6-880d-78c784d4fad5");

        /// <summary>
        /// Группа пользователей
        /// </summary>
        [Display(Name = "Группа пользователей")]
        public static readonly Guid otUserGroup = new Guid("d43faaa2-427c-412f-b92c-11b78ea7be9b");

        /// <summary>
        /// Человек (физическое лицо)
        /// </summary>
        [Display(Name = "Человек (физическое лицо)")]
        public static readonly Guid otIndividualPerson = new Guid("e815f4ff-0de5-4d39-afaa-95bc9d72b9c7");

        /// <summary>
        /// Документы, отчеты, изменения документов и т.п.
        /// </summary>
        [Display(Name = "Документы, отчеты, изменения документов и т.п.")]
        public static readonly Guid otDocument = new Guid("4c4b28ae-578b-4358-99ea-2860139abd85");

        /// <summary>
        /// Тип документа
        /// </summary>
        [Display(Name = "Тип документа")]
        public static readonly Guid otDocumentType = new Guid("91463768-d5e2-4dcd-a125-ac4b60fe7b51");

        /// <summary>
        /// Группа типов отчетов
        /// </summary>
        [Display(Name = "Тип отчетов")]
        public static readonly Guid otReportType = new Guid("0a1087d5-ab77-449d-9677-4b825e80e847");

        /// <summary>
        /// Группа типов отчетов
        /// </summary>
        [Display(Name = "Группа типов отчетов")]
        public static readonly Guid otReportTypeGroup = new Guid("ad4aaa57-a71b-48a9-ad8a-9542a3d8711f");

        /// <summary>
        /// Юридическое лицо
        /// </summary>
        [Display(Name = "Юридическое лицо")]
        public static readonly Guid otJuridicalPerson = new Guid("88ad30fc-92a1-4ec3-b168-c9fed4ca2c9d");

        /// <summary>
        /// Регион
        /// </summary>
        [Display(Name = "Регион")]
        public static readonly Guid otRegion = new Guid("97ba0907-b9fa-4705-a4a9-604f8c5a014f");

        /// <summary>
        /// Район
        /// </summary>
        [Display(Name = "Район")]
        public static readonly Guid otDistrict = new Guid("56057318-c4f2-463e-b05f-781112e62e96");

        /// <summary>
        /// Уровень профессионального образования
        /// </summary>
        [Display(Name = "Уровень профессионального образования")]
        public static readonly Guid otEducationLevel = new Guid("bd3c4813-9d0a-4fa0-bf53-a44500e84914");

        /// <summary>
        /// Тип населенного пункта (город, село)
        /// </summary>
        [Display(Name = "Тип населенного пункта (город, село)")]
        public static readonly Guid otLocalityType = new Guid("0fd2f761-1570-4ef3-9be6-a44500e85760");        
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
        public static readonly Guid ugArchitect = new Guid("85034fd6-4f23-4f0b-8fe4-56f4efd17fb1");

        /// <summary>
        /// Общая группа пользователей
        /// </summary>
        [Display(Name = "Общая группа пользователей")]
        public static readonly Guid ugCommonUserGroup = new Guid("e1977546-f3c3-47cd-bd23-bc05ec3f58fc");
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
        /// Новый
        /// </summary>
        [Display(Name = "Новый")]
        public static readonly Guid rsNew = new Guid("8260de6e-58a6-4004-84fa-a42800de4837");

        /// <summary>
        /// Заполнен
        /// </summary>
        [Display(Name = "Заполнен")]
        public static readonly Guid rsFilled = new Guid("37b959eb-f8e5-4ea6-97ed-a4290130d736");

        /// <summary>
        /// Архив
        /// </summary>
        [Display(Name = "Архив")]
        public static readonly Guid rsArchive = new Guid("c96cb2af-933f-4a98-bce6-618f5d4ca152");

        /// <summary>
        /// Доработать
        /// </summary>
        [Display(Name = "Доработать")]
        public static readonly Guid rsToComplete = new Guid("3fc136d9-a754-4f7e-abfd-3c3fd6b67cb6");
    }
}

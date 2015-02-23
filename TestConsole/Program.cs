using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using TestConsole.TreeServiceReference;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\users.xls;Extended Properties=Excel 8.0");
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from [users$]", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            
            //var passwordMd5 = CryptHelper.GetMd5Hash("123456");
            //var salt = CryptHelper.GenerateSalt("user", passwordMd5, 12);
            //var passwordWithSaltMd5 = CryptHelper.GetMd5Hash(passwordMd5+salt);            

            var treeServiceClient = new TreeServiceReference.TreeServiceClient();

            treeServiceClient.ClientCredentials.UserName.UserName = "user";
            treeServiceClient.ClientCredentials.UserName.Password = "123456";

            /*var r = new RegionDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllRegions,
                Name = "Пермский край",
                ShortName = "",
                TypeId = ObjectTypes.otRegion,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now
            };}

            var regionDto = treeServiceClient.CreateRegion(r); */ 

            var regionDto = treeServiceClient.GetRegion(new Guid("c9d34f0f-3578-4f79-b7d3-a44800283bc0"));

            var d = new DistrictDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllDistricts,
                Name = "МО Юсьвинский район",
                ShortName = "",
                TypeId = ObjectTypes.otDistrict,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now,
                RegionId = regionDto.Id,
                RegionName = regionDto.Name
            };

            var districtDto = treeServiceClient.CreateDistrict(d);

            //var districtDto = treeServiceClient.GetDistrict(new Guid("a9b13dd5-fa17-46ac-9f3a-a448003303e4"));

            var eduLevelDto = treeServiceClient.GetEducationLevel(new Guid("14174718-ccfa-4c98-842b-a4470149bf98"));
            var localityTypeDto = treeServiceClient.GetLocalityType(new Guid("4370ba28-6130-42cc-8545-a447014a1e79")); // город
            var localityTypeDto2 = treeServiceClient.GetLocalityType(new Guid("171d83c9-d03a-4a8e-9c89-a447014a3136")); // село

            var instituteNames = new List<string>()
            {                
                "ГБПОУ \"Юсьвинский агротехнический техникум\""
            };

            foreach (var i in instituteNames.Select(instituteName => new InstituteDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllInstituteJuridicalPersons,
                Name = instituteName,
                ShortName = "",
                TypeId = ObjectTypes.otJuridicalPerson,
                StateId = ObjectStates.osActive,
                CreateDateTime = DateTime.Now,
                LocationId = districtDto.Id,
                LocationName = districtDto.Name,
                EducationLevelId = eduLevelDto.Id,
                EducationLevelName = eduLevelDto.Name,
                LocalityTypeId = localityTypeDto.Id,
                LocalityTypeName = localityTypeDto.Name
            }))
            {
                if (i.Name == "ГБОУ СПО Зюкайский аграрный техникум")
                {
                    i.LocalityTypeId = localityTypeDto2.Id;
                    i.LocalityTypeName = localityTypeDto2.Name;
                }
                treeServiceClient.CreateInstitute(i);
            }

            var ss = 1;

            // r = treeServiceClient.GetReport(new Guid("84f4dd1c-fadb-4add-a355-a42900a252b0"));

            //r.Name = "Тестовый отчет";
            //r.DocumentStateId = ReportStates.rsCreated;

            //treeServiceClient.UpdateReport(r);

            //var so = treeServiceClient.GetSystemObjects();

            //var strees = treeServiceClient.SearchTreesByText("Настрой", SystemObjects.Root, new Guid[] { }, new Guid[] { }, SystemObjects.Root);

            //var trees = treeServiceClient.GetTrees(new Guid("20f9b9ce-8769-4569-ae71-1ecf18be90b3"),
            //    new Guid("C034E889-3B80-42D3-BDAD-5F4E729A905B"), true, false);

            //var tree = treeServiceClient.GetTree(new Guid("A15D22B8-B18D-48FD-8964-DC91E4F7652B"));
            //var tree_user = treeServiceClient.GetTree(new Guid("5F9F9CD3-CF2F-4CC4-B4E5-87D78F5F0090"));
            //var tree_person = treeServiceClient.GetTree(new Guid("47C67417-B8AB-4397-A0E9-8AE717C37942"));

            //var user = treeServiceClient.GetUser(new Guid("2B0FFAA3-913B-4120-982E-736EE0AF0F39"));
            //var person = treeServiceClient.GetPerson(new Guid("3A76B25C-D007-4B18-A125-03863D9C7810"));
            

            //var person2 = treeServiceClient.GetPerson(new Guid("A15D22B8-B18D-48FD-8964-DC91E4F7652B"));

            //var trees = treeServiceClient.SearchTreesByText("сис", SystemObjects.Root, new Guid[]{}, new Guid[]{}, null);

            //var treeList = treeServiceClient.GetTrees();

            //var vt = treeServiceClient.GetTrees(new Guid("20F9B9CE-8769-4569-AE71-1ECF18BE90B3"),
            //    new Guid("C034E889-3B80-42D3-BDAD-5F4E729A905B"), false);

            //var person = treeServiceClient.CreatePerson(new PersonDto
            //{
            //    Id = Guid.NewGuid(),
            //    ParentId = SystemObjects.AllPeople,
            //    Name = "Тестовый пользователь системы",
            //    TypeId = ObjectTypes.otIndividualPerson,
            //    StateId = ObjectStates.osInDevelopment,
            //    CreateDateTime = DateTime.Now,

            //    Surname = "Тестовый",
            //    FirstName = "Пользователь",
            //    Patronymic = "Системы",
            //    BirthDate = new DateTime(2015, 1, 1)
            //});

            

            Console.ReadLine();
        }
    }
}

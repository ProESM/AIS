using System;
using System.Collections.Generic;
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
            //var passwordMd5 = CryptHelper.GetMd5Hash("123456");
            //var salt = CryptHelper.GenerateSalt("user", passwordMd5, 12);
            //var passwordWithSaltMd5 = CryptHelper.GetMd5Hash(passwordMd5+salt);

            var treeServiceClient = new TreeServiceReference.TreeServiceClient();

            //var user = treeServiceClient.FindUserByLogin("architect");

            //var treeList = treeServiceClient.GetTrees();

            //var vt = treeServiceClient.GetTrees(new Guid("20F9B9CE-8769-4569-AE71-1ECF18BE90B3"),
            //    new Guid("C034E889-3B80-42D3-BDAD-5F4E729A905B"), false);

            var person = treeServiceClient.CreatePerson(new PersonDto
            {
                Id = Guid.NewGuid(),
                ParentId = SystemObjects.AllPeople,
                Name = "Тестовый пользователь системы",
                TypeId = ObjectTypes.otPerson,
                StateId = ObjectStates.osInDevelopment,
                CreateDateTime = DateTime.Now,

                Surname = "Тестовый",
                FirstName = "Пользователь",
                Patronymic = "Системы",
                BirthDate = new DateTime(2015, 1, 1)
            });

            var so = treeServiceClient.GetSystemObjects();

            Console.ReadLine();
        }
    }
}

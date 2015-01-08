using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;

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

            var vt = treeServiceClient.GetTrees(new Guid("0322648B-488A-40C0-9CEE-B3A4F9B572EF"),
                new Guid("C034E889-3B80-42D3-BDAD-5F4E729A905B"), false);

            var so = treeServiceClient.GetSystemObjects();

            Console.ReadLine();
        }
    }
}

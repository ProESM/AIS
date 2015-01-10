using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Infrastructure
{
    public interface ITreeRepository : IRepository
    {
        List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false);

        TreeDao CreateTree(TreeDao treeDao);

        TreeDao GetTree(Guid treeId);

        void UpdateTree(TreeDao treeDao);

        UserDao CreateUser(UserDao userDao);

        UserDao GetUser(Guid userId);

        void UpdateUser(UserDao userDao);

        UserDao FindUserByLogin(string login);

        UserDao FindUserByEmail(string email);

        PersonDao CreatePerson(PersonDao personDao);

        PersonDao GetPerson(Guid personId);
    }
}

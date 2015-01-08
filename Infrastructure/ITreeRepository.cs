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
        UserDao FindUserByLogin(string login);

        List<VirtualTreeDao> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false);

        TreeDao CreateTree(TreeDao treeDao);

        TreeDao GetTree(Guid treeId);

        void UpdateTree(TreeDao treeDao);
    }
}

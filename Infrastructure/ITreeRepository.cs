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
        List<TreeDao> GetTrees();

        UserDao FindUserByLogin(string login);
    }
}

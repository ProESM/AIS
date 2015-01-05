using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;

namespace Domain
{    
    public interface IDomainTreeService
    {
        List<TreeDto> GetTrees();

        UserDto FindUserByLogin(string login);

        UserDto AuthenticateUser(string login, string password);

        List<Guid> GetSystemObjects();
    }

}

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
        UserDto FindUserByLogin(string login);

        UserDto AuthenticateUser(string login, string password);

        UserDto RegisterUser(RegistrationUserDto userDto);

        List<Guid> GetSystemObjects();

        List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false);

        TreeDto CreateTree(TreeDto treeDto);
    }

}

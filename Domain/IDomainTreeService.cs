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
        List<Guid> GetSystemObjects();

        List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false, bool includeDeleted = false);

        TreeDto CreateTree(TreeDto treeDto);

        TreeDto GetTree(Guid treeId);

        void UpdateTree(TreeDto treeDto);

        void DeleteTree(TreeDto treeDto);

        UserDto CreateUser(UserDto userDto);

        UserDto GetUser(Guid userId);

        void UpdateUser(UserDto userDto);

        UserDto FindUserByLogin(string login);

        UserDto FindUserByEmail(string email);

        UserDto AuthenticateUser(string login, string password);

        UserDto RegisterUser(RegistrationUserDto userDto);

        PersonDto CreatePerson(PersonDto personDto);

        PersonDto GetPerson(Guid personId);
    }

}

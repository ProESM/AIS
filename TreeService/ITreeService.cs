using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DTO;

namespace TreeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITreeService" in both code and config file together.
    [ServiceContract]
    public interface ITreeService
    {
        [OperationContract]
        UserDto FindUserByLogin(string login);

        [OperationContract]
        UserDto AuthenticateUser(string login, string password);

        [OperationContract]
        List<Guid> GetSystemObjects();

        [OperationContract]
        List<VirtualTreeDto> GetTrees(Guid? parent, Guid treeParentType, bool includeParent = false);

        [OperationContract]
        TreeDto CreateTree(TreeDto treeDto);

        [OperationContract]
        TreeDto GetTree(Guid treeId, bool includeDeleted = false);

        [OperationContract]
        void UpdateTree(TreeDto treeDto);

        [OperationContract]
        void DeleteTree(TreeDto treeDto);
    }
}

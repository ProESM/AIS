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
        List<TreeDto> GetTrees();

        [OperationContract]
        UserDto FindUserByLogin(string login);

        [OperationContract]
        UserDto AuthenticateUser(string login, string password);
    }
}

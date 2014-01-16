using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReversiGame_ServerSide
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    

    [ServiceContract(Namespace = "ReversiGame_ServerSide", CallbackContract = typeof(IGamePlayCallback))]
    public interface IGamePlay
    {
        [OperationContract(IsOneWay = true)]
        void CplaceDisk();

        [OperationContract]
        bool CchooseOpponent(string PlayerID);

        [OperationContract]
        bool CquitGame(string PlayerID);

        [OperationContract]
        bool CsendMessage(string PlayerID, string message);
    
    }

    [DataContract]
    [Serializable]
    public class Disk
    {
        [DataMember]
        public int x
        {
            get;
            set;
        }

        [DataMember]
        public int y
        {
            get;
            set;
        }

        [DataMember]
        public string color
        {
            get;
            set;
        }
    }

    
    public interface IGamePlayCallback
    {
        [OperationContract(IsOneWay = true)]
        void placeDisk(Disk lastPlacedDisk);

        [OperationContract]
        bool quit();

        [OperationContract]
        bool invitationAccepted();

        //[OperationContract(IsOneWay = true)]
        //void testc();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ReversiGame_ServerSide.ContractType".
  
}

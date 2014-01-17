using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame_ServerSidePortal
{
    [ServiceContract(Namespace = "ReversiGame_ServerSidePortal", CallbackContract = typeof(IPortalCallback))]
    public interface IPortal
    {

        [OperationContract(IsOneWay = true)]
        void connected();

        [OperationContract (IsOneWay = true)]
        void connectAsMember(string username, string password);

        [OperationContract]
        string creatAccount(string username, string password);

        [OperationContract]
        bool changePassword(string id, string passw, string newPassW);

        [OperationContract]
        bool chooseOponent(string oponent);

        [OperationContract (IsOneWay = true)]
        void messageWasSend(string message);

        [OperationContract (IsOneWay = true)]
        void IsTyping();
    }

    [DataContract]
    public class PlayerMember
    {
        public PlayerMember() { }

        public IPortalCallback callback;

        public PlayerMember oponent;

        public string userName;

        public string password;

        public bool isLogIn;

        public bool IsLogIN
        {
            set {isLogIn = value;}
            get{return isLogIn;}
        }

        [DataMember]
        public string chatMassage { get; set; }

    }

    public interface IPortalCallback
    {
        [OperationContract (IsOneWay = true)]
        void messageSent(string m);

        [OperationContract(IsOneWay = true)]
        void IsTypingCallback();

        [OperationContract(IsOneWay = true)]
        void ConnectedCAllback(string logInfo);

        [OperationContract(IsOneWay = true)]
         void CconnectPlayer(List<string> list);
    }

}

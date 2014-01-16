using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame_ServerSidePortal
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class CPortal : IPortal
    {
        //public delegate void onConnectHandler(string id);
        //public event onConnectHandler e_Connect;

        List<string> listID = new List<string>();
        List<string> chatMsg = new List<string>();


        List<PlayerMember> listPlayerMbr = new List<PlayerMember>();

        /// <summary>
        ///  return true to prove that the call has been made by a client
        /// </summary>
        /// <returns> true </returns>
        public void connected()
        {
            IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();
            callback.ConnectedCAllback(listID);

        }

        public string connectAsMember(string username, string password)
        {
            string connect = "wrong username or passWord"; ;

            foreach (PlayerMember p in listPlayerMbr)
            {
                if ((p.userName == username) && (p.password == password))
                {
                    if (p.IsLogIN)
                    {
                        connect = "you are alreday online!";
                    }
                    else
                    {
                        p.IsLogIN = true;
                        listID.Add(p.userName);
                        //IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();
                        //callback.CconnectPlayer(listID);
                        //refreshList();
                        connect = "you are connected! you can play now!";
                    }
                }
            }
            return connect;
        }

        public void refreshList()
        {
            
            //List<string> tempList;

            //    for (int i = 0; i < listPlayerMbr.Count; i++)
            //    {
            //        if (listPlayerMbr[i].userName == Convert.ToString(listID.Last()))
            //        {
            //            tempList = listID;
            //           // listID.RemoveAt(listID.Count - 1);
             //           listPlayerMbr[i].callback.CconnectPlayer(listID);
            //            listID = tempList;
            //        }
            //        else if(listPlayerMbr[i].isLogIn)
            //        {
            //            listPlayerMbr[i].callback.CconnectPlayer(listID);
            //        }
            //    }
        }

        /// <summary>
        /// checks if there is no a simular user name, then creat an account
        /// </summary>
        /// <param name="username"> username </param>
        /// <param name="password">and passWord</param>
        /// <returns>message either is successfull, or passWord Short or userName exist </returns>
        public string creatAccount(string username, string password)
        {
            string creat = "";
            bool check = false;

            foreach (PlayerMember p in listPlayerMbr)
            {
                if (p.userName == username)
                {
                    check = true;
                }
            }


            if (check)
            {
                creat = "userName already used! creat another one.";
            }
            else if (5 > password.Count<char>())
            {
                creat = "passWord too short !!";
            }
            else
            {
                PlayerMember player = new PlayerMember();
                player.userName = username;
                player.password = password;
                IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();
                player.callback = callback;

                listPlayerMbr.Add(player);
                //listID.Add(player.userName);
                creat = "account succesful created";
            }

            return creat;
        }

        /// <summary>
        /// changes the password 
        /// </summary>
        /// <param name="passw"> old passWord </param>
        /// <param name="newPassW"> new passWord </param>
        /// <returns></returns>
        public bool changePassword(string id, string passw, string newPassW)
        {
            bool changed = false;
            foreach (PlayerMember p in listPlayerMbr)
            {
                if ((p.userName == id) && (p.password == passw))
                {
                    p.password = newPassW;
                    changed = true;
                }
            }
            return changed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oponent"></param>
        /// <returns></returns>
        public bool chooseOponent(string oponent)
        {
            bool choseOp = false;

            IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();
            foreach (PlayerMember p in listPlayerMbr)
            {
                if (p.callback == callback)
                {

                    foreach (PlayerMember o in listPlayerMbr)
                    {
                        if (o.userName == oponent)
                        {
                            p.oponent = o;
                            o.oponent = p;
                            choseOp = true;
                        }
                    }
                }
            }
            return choseOp;

        }

        /// <summary>
        /// sends chatMassage to the oponent
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="message"></param>
        public void messageWasSend(string message)
        {
            // chatMsg.Add( ":" + message);
            string noOponent = "chose first your Oponent";
            IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();

            foreach (PlayerMember p in listPlayerMbr)
            {
                if ((p.callback == callback) && (p.oponent != null))
                {
                    p.oponent.callback.messageSent(message);

                }
                else p.callback.messageSent(noOponent);
            }
        }

        public void IsTyping()
        {
            IPortalCallback callback = OperationContext.Current.GetCallbackChannel<IPortalCallback>();

            foreach (PlayerMember p in listPlayerMbr)
            {
                if (p.callback == callback)
                {
                    p.oponent.callback.IsTypingCallback();

                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReversiGame_ServerSide
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service : IGamePlay
    {

        public void CplaceDisk()
        {
            throw new NotImplementedException();
        }

        public bool CchooseOpponent(string PlayerID)
        {
            throw new NotImplementedException();
        }

        public bool CquitGame(string PlayerID)
        {
            throw new NotImplementedException();
        }

        public bool CsendMessage(string PlayerID, string message)
        {
            throw new NotImplementedException();
        }
    }
}
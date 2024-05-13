using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Model
{
    /// <summary>
    /// This class represents a message a user can write to another user
    /// </summary>
    public class Message
    {
        #region instance fields
        private int _messageid; 
        private int _senderid;
        private int _recipientid;
        private DateTime _timesent; 
        private string _messagevalue;
        #endregion

        /// <summary>
        /// This constructor uses parameters which are validated using the properties of the class before the object finally is instantiated
        /// </summary>
        /// <param name="messageid">ID for a given message</param>
        /// <param name="senderid">ID of the user sending the message</param>
        /// <param name="recipientid">ID of the user receiving the message</param>
        /// <param name="messagevalue">Content of the message the user sends to the other user</param>
        /// <param name="timesent">Time at which the message was sent. DateTime is expected</param>
        public Message(int messageid, int senderid, int recipientid, string messagevalue, DateTime timesent)
        {
            Messageid = messageid;
            Senderid = senderid;
            Recipientid = recipientid;
            Timesent = timesent;
            Messagevalue = messagevalue;
        }

        #region properties
        public int Messageid { get => _messageid; set => _messageid = value; }
        public int Senderid { get => _senderid; set => _senderid = value; }
        public int Recipientid { get => _recipientid; set => _recipientid = value; }
        public DateTime Timesent { get => _timesent; set => _timesent = value; }
        public string Messagevalue { get => _messagevalue; set => _messagevalue = value; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Messageid)}={Messageid.ToString()}, {nameof(Senderid)}={Senderid.ToString()}, {nameof(Recipientid)}={Recipientid.ToString()}, {nameof(Timesent)}={Timesent.ToString()}, {nameof(Messagevalue)}={Messagevalue}}}";
        }
    }
}

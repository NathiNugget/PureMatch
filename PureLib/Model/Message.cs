using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Model
{
    public class Message
    {
        private int _messageid; 
        private int _senderid;
        private int _recipientid;
        private DateTime _timesent; 
        private string _messagevalue;

        public Message(int messageid, int senderid, int recipientid, DateTime timesent, string messagevalue)
        {
            Messageid = messageid;
            Senderid = senderid;
            Recipientid = recipientid;
            Timesent = timesent;
            Messagevalue = messagevalue;
        }

        public int Messageid { get => _messageid; set => _messageid = value; }
        public int Senderid { get => _senderid; set => _senderid = value; }
        public int Recipientid { get => _recipientid; set => _recipientid = value; }
        public DateTime Timesent { get => _timesent; set => _timesent = value; }
        public string Messagevalue { get => _messagevalue; set => _messagevalue = value; }

        public override string ToString()
        {
            return $"{{{nameof(Messageid)}={Messageid.ToString()}, {nameof(Senderid)}={Senderid.ToString()}, {nameof(Recipientid)}={Recipientid.ToString()}, {nameof(Timesent)}={Timesent.ToString()}, {nameof(Messagevalue)}={Messagevalue}}}";
        }
    }
}

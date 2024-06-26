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
            MessageID = messageid;
            SenderID = senderid;
            RecipientID = recipientid;
            Timesent = timesent;
            Messagevalue = messagevalue;
        }

        public Message() : this(1, 1, 2, "hej", DateTime.Now) { }

        #region properties
        public int MessageID
        {
            get => _messageid;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException($"V�rdien af ID m� ikke v�re under 0. Du angav: {value}");
                }
                _messageid = value;

            }
        }
        public int SenderID
        {
            get => _senderid; set
            {

                if (value < 1)
                {
                    throw new ArgumentException($"V�rdien af AfsenderID m� ikke v�re under 0. Du angav: {value}");
                }
                _senderid = value;
            }
        }

        public int RecipientID
        {
            get => _recipientid;
            set
            {

                if (value < 1)
                {
                    throw new ArgumentException($"V�rdien af ModtagerID m� ikke v�re under 0. Du angav: {value}");
                }
                _recipientid = value;
            }
        }
        public DateTime Timesent
        {
            get => _timesent;
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentNullException($"V�rdien af Afsendertidpunkt m� ikke v�re tom");
                }
                _timesent = value;
            }
        }
        public string Messagevalue
        {
            get => _messagevalue;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($"V�rdien af beskedn m� ikke v�re tom");
                }
                if (value.Length > ValidationRegex.MESSAGELENGTHMAX)
                {
                    throw new ArgumentException($"L�ngen af besked m� max v�re 100 karakterer. Din besked var {value.Length} karakterer lang");
                }
                _messagevalue = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(MessageID)}={MessageID.ToString()}, {nameof(SenderID)}={SenderID.ToString()}, {nameof(RecipientID)}={RecipientID.ToString()}, {nameof(Timesent)}={Timesent.ToString()}, {nameof(Messagevalue)}={Messagevalue}}}";
        }


    }
}

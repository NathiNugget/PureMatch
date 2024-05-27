using PureLib.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLibTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestMessage
    {
        private Message msg;
        private Message msgnormalctor = new Message(1, 1, 2, "Hej", DateTime.Now);
        [TestInitialize]
        public void BeforeEachTest()
        {
            msg = new Message();
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            msg = null;
        }

        //ID 
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void IDProperty(int id)
        {
            msg.MessageID = id;
            Assert.AreEqual(id, msg.MessageID);
        }

        

        //ID property illegal values
        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(int.MinValue)]
        public void IDPropertyIlleagl(int id)
        {
            Assert.ThrowsException<ArgumentException>(() => msg.MessageID = id);
        }

        // SenderID property
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(int.MaxValue)]
        public void SenderIDProperty(int id)
        {
            msg.SenderID = id;
            Assert.AreEqual(id, msg.SenderID);
        }


        //SenderID property illegal values
        [TestMethod]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public void SenderIDIllegal(int id)
        {
            Assert.ThrowsException<ArgumentException>(() => msg.SenderID = id);
        }

        //RecipientID property
        [TestMethod]
        [DataRow(1)]
        [DataRow(int.MaxValue)]
        public void RecipientID(int id)
        {
            msg.RecipientID = id;
            Assert.AreEqual(id, msg.RecipientID);
        }

        //RecipientID property
        [TestMethod]
        [DataRow(0)]
        [DataRow(int.MinValue)]
        public void RecipientIDIllegal(int id)
        {
            Assert.ThrowsException<ArgumentException>(() => msg.RecipientID = id);
        }
   

        //Message property
        [TestMethod]
        [DataRow("H")]
        [DataRow("HalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHallo")]
        public void MessageProperty(string message)
        {
            msg.Messagevalue = message;
            Assert.AreEqual(message, msg.Messagevalue);
        }

        //Message property illegal values
        [TestMethod]
        [DataRow("HalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloHalloH")]
        public void MessagePropertyIllegal(string message)
        {
            Assert.ThrowsException<ArgumentException>(() => msg.Messagevalue = message);
        }

        //Message property null/empty values
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void MessagePropertyNull(string message)
        {
            Assert.ThrowsException<ArgumentNullException>(() => msg.Messagevalue = message); 
        }

        //Message DateTime property
        [TestMethod]
        public void DateTimeProperty()
        {
            DateTime dt = DateTime.Now;
            msg.Timesent = dt;
            Assert.AreEqual(dt, msg.Timesent);

        }

        //MessageTime
        [TestMethod]
        [DataRow(null)]
        public void DateTimePropertyNull(DateTime dt)
        {
            Assert.ThrowsException<ArgumentNullException>(() => msg.Timesent = dt);
        }

        //ToString
        [TestMethod]
        public void TryToString()
        {
            string rep = msg.ToString();
            Assert.AreEqual(rep, msg.ToString());
        }
    }
}

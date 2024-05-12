using PureLib.Model;
using System.Data;

namespace PureLibTest
{
    [TestClass]
    public class TestUserClass
    {
        private User u; 
        [TestInitialize]
        public void BeforeEachTest()
        {
            u = new User();
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            u = null!; 
        }


        //UserID property with legal values
        [TestMethod]
        [DataRow(1)]
        [DataRow(int.MaxValue/2)]
        [DataRow(int.MaxValue)]
        public void UserID(int id)
        {
            u.UserID = id;
            Assert.AreEqual(id, u.UserID);
        }

        //UserID property with illegal values
        [TestMethod]
        [DataRow(null)]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(int.MinValue)]
        public void UserIDIllegal(int id)
        {
            Assert.ThrowsException<ArgumentException>(() => u.UserID = id);
        }

        //Name property
        [TestMethod]
        [DataRow("Nat")]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTeste")]
        [DataRow("TesteTesteTesteTesteTeste")]
        public void Name(string name)
        {
            u.Name = name;
            Assert.AreEqual(name, u.Name);
        }

        //Name property illegal values
        [TestMethod]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTesteT")]
        public void NameIllegal(string name)
        {
            u.Name = name; 
            Assert.ThrowsException<ArgumentException>(() => u.Name = name);
            Assert.ThrowsException<ArgumentNullException>(() => u.Name = null!);
            Assert.ThrowsException<ArgumentNullException>(() => u.Name = "");
        }

        //UserName property
        [TestMethod]
        [DataRow("NathiNathiNathiNathiNathiNathi")]
        []





    }
}
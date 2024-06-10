using PureLib.Model;
using PureLib.Services;
using System.Diagnostics.CodeAnalysis;

namespace PureLibTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestDB
    {
        private int _usersInDB = 0;
        private readonly int _messagesInDB = 0;
        private readonly DataBaseLink db = new DataBaseLink();
        private static List<MuscleGroupEnum> _msgroups = new List<MuscleGroupEnum> { MuscleGroupEnum.Bryst };
        private static List<DaysEnum> _days = new List<DaysEnum>() { DaysEnum.Tirsdag };


        public TestDB()
        {
            _usersInDB = db.ReadAllUsersFromDB().Count;
            _messagesInDB = db.ReadAllMessagesFromDB().Count;
        }

        [TestMethod]
        public void UsersInDB()
        {
            int usercount = db.ReadAllUsersFromDB().Count;
            Assert.AreEqual(usercount, _usersInDB);
        }

        [TestMethod]
        public void MessagesInDB()
        {
            int msgcount = db.ReadAllMessagesFromDB().Count;
            Assert.AreEqual(_messagesInDB, msgcount);
        }

        [TestMethod]
        public void SetUserMatching()
        {
            int rowsaffected = db.SetMatching(2, null, null, 1);
            Assert.AreEqual(3, rowsaffected);
            rowsaffected = db.SetMatching(2, _msgroups, _days, 1);
            Assert.AreEqual(3, rowsaffected);

        }

        [TestMethod]
        //send and delete
        public void SendAndDelete()
        {
            (int rowsaffected, int maxid) = db.SendMessage(1, 2, "Hej");
            Assert.AreEqual(1, rowsaffected);
            rowsaffected = db.DeleteMessage(maxid);
            Assert.AreEqual(1, rowsaffected);
        }

        [TestMethod]
        //read musclegroups and days
        public void ReadMSGroupsAndDays()
        {
            int msgroups = db.ReadMuscleGroups(2).Count;
            int days = db.ReadDays(2).Count;
            Assert.AreEqual(1, msgroups);
            Assert.AreEqual(1, days);

        }

        [TestMethod]
        public void GetChatUsers()
        {
            int users = db.GetChatUsers(2).Count;
            Assert.AreEqual(1, users);
        }

        [TestMethod]
        public void GetMatches()
        {
            int matches = db.GetMatches(2).Count;
            Assert.AreEqual(1, matches);
        }

        [TestMethod]
        public void Update()
        {
            User user = db.ReadUser(2);
            user.Password = "Hej123";
            int rowsaffected = db.UpdateUser(user);
            Assert.AreEqual(1, rowsaffected);
        }

        [TestMethod]
        public void CreateUser()
        {
            int rowsaffected = db.AddUser(new User());
            Assert.AreEqual(1, rowsaffected);
        }

        [TestMethod]
        public void GetMessges()
        {
            int msgcount = db.ReadMessages(2, 1).Count;
            int somemessage = db.GetMessage(2).MessageID;
            Assert.AreEqual(2, msgcount);
            Assert.AreEqual(2, somemessage);
        }

        [TestMethod]
        public void ReadLogin()
        {
            User u = db.ReadLogin("AbdiA", "AbdiA123")!;
            Assert.IsNotNull(u);
        }






    }
}

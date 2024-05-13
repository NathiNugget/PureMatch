using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureLib.Model;
using System.Data;
using System.Linq.Expressions;
using System.Net.Mail;

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

        //Test normal ctor for user
        [TestMethod]
        public void Man()
        {
            User u = new User(1, "test", "test", "test", "12345678", "test@testmail.com", "1111222233334444", "123", "11", "25", SubscriptionEnum.Core, LevelsEnum.Begynder);
            Assert.IsNotNull(u);
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            u = null!;
        }


        //UserID property with legal values
        [TestMethod]
        [DataRow(1)]
        [DataRow(int.MaxValue / 2)]
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
        [DataRow("Te")]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTesteT")]
        public void NameIllegal(string name)
        {
            
            Assert.ThrowsException<ArgumentException>(() => u.Name = name);
            Assert.ThrowsException<ArgumentNullException>(() => u.Name = null!);
            Assert.ThrowsException<ArgumentNullException>(() => u.Name = "");
        }

        //UserName property
        [TestMethod]
        [DataRow("TesteTesteTesteTesteTeste")]
        [DataRow("Tes")]
        [DataRow("TesteTesteTesteTesteTesteTeste")]
        public void UserName(string name)
        {
            u.UserName = name;
            Assert.AreEqual(name, u.UserName);
        }

        //UserName  
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void UserNameNull(string name)
        {
            Assert.ThrowsException<ArgumentNullException>(() => u.UserName = name);
        }

        //UserName property other illegal values
        [TestMethod]
        [DataRow("Te")]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTesteT")]
        public void UserNameIllegal(string name)
        {
            Assert.ThrowsException<ArgumentException>(() => u.UserName = name);
        }

        //Password property
        [TestMethod]
        [DataRow("123")]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTeste")]
        public void Password(string password)
        {
            u.Password = password;
            Assert.AreEqual(password, u.Password);
        }

        //Password property illegal values
        [TestMethod]
        [DataRow("12")]
        [DataRow("TesteTesteTesteTesteTesteTesteTesteTesteTesteTesteT")]
        public void PasswordIllegal(string password)
        {
            Assert.ThrowsException<ArgumentException>(() => u.Password = password);
        }

        //Password proterty null/empty values
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void PasswordNull(string password)
        {
            Assert.ThrowsException<ArgumentNullException>(() => u.Password = password);
        }

        //PhoneNumber property
        [TestMethod]
        [DataRow("12345678")]
        [DataRow("87654321")]
        public void PhoneNumber(string number)
        {
            u.PhoneNumber = number;
            Assert.AreEqual(number, u.PhoneNumber);
        }

        //PhoneNumber property null values
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void PhoneNumberNull(string number)
        {
            Assert.ThrowsException<ArgumentNullException>(() => u.PhoneNumber = number);
        }

        //PhoneNumber other illegal values
        [TestMethod]
        [DataRow("1234567")]
        [DataRow("123456789")]
        [DataRow("1234567a")]
        public void PhoneNumberIllegal(string number)
        {
            Assert.ThrowsException<ArgumentException>(() => u.PhoneNumber = number);
        }

        //Email property
        [TestMethod]
        [DataRow("admin@hotmail.com")]
        [DataRow("a@m.co")]
        [DataRow("admin@adminmail.testmail.simply.com")]
        public void Email(string email)
        {
            u.Email = email;
            Assert.AreEqual(email, u.Email);
        }

        //Email proterty null/empty values
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void EmailNull(string email)
        {
            Assert.ThrowsException<ArgumentNullException>(() => u.Email = email);
        }

        //Email property illegal values
        [TestMethod]
        [DataRow("anders.gmail.com")]
        [DataRow("anders@gmail.c")]
        [DataRow("@gmail.com")]
        [DataRow("anders.andersyeah@")]
        [DataRow("anders@gmail")]
        public void EmailIllegal(string email)
        {
            Assert.ThrowsException<ArgumentException>(() => u.Email = email);
        }

        //CardNumber proterty
        [TestMethod]
        [DataRow("1234567890123456")]
        [DataRow("6543210987654321")]
        [DataRow("1111222233334444")]
        public void CardNumber(string cardnumber)
        {
            u.CardNumber = cardnumber;
            Assert.AreEqual(cardnumber, u.CardNumber);
        }

        //CardNumber property illegal values
        [TestMethod]
        [DataRow("123456789012345")]
        [DataRow("12345678901234567")]
        public void CardNumberIllegal(string cardnumber)
        {
            Assert.ThrowsException<ArgumentException>(() => u.CardNumber = cardnumber);
        }

        //CardNumber proterty null/empty values
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null)]
        [DataRow("")]
        public void CardNumberNull(string cardnumber)
        {
            u.CardNumber = cardnumber;
        }

        //CardCVC property
        [TestMethod]
        [DataRow("321")]
        [DataRow("123")]
        [DataRow("999")]
        public void CardCVC(string cvc)
        {
            u.CardCVC = cvc;
            Assert.AreEqual(cvc, u.CardCVC);
        }

        //CardCVC property illegal values
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("12")]
        [DataRow("1234")]
        [DataRow("12b")]
        public void CardCVCIllegal(string cvc)
        {
            u.CardCVC = cvc;
        }

        //CardCVC proterty null/empty values
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow("")]
        [DataRow(null)]
        public void CardCVCNull(string cvc)
        {
            u.CardCVC = cvc;
        }


        //CardExpMonth property
        [TestMethod]
        [DataRow("01")]
        [DataRow("12")]
        [DataRow("06")]
        public void CardExpMonth(string expmonth)
        {
            u.CardExpMonth = expmonth;
            Assert.AreEqual(expmonth, u.CardExpMonth);
        }

        //CardExpMonth property illegal values
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("00")]
        [DataRow("13")]
        [DataRow("0a")]
        [DataRow("120")]
        public void CardExpMonthIllegal(string expmonth)
        {
            u.CardExpMonth = expmonth;
        }

        //CardExpMonth proterty null/empty values
        [TestMethod][ExpectedException(typeof(ArgumentNullException))]
        [DataRow("")]
        [DataRow(null)]
        public void CardExpMonthNull(string expmonth)
        {
            u.CardExpMonth = expmonth;
        }

        //CardExpYear property
        [TestMethod]
        [DataRow("25")]
        [DataRow("29")]
        public void CardExpYear(string expyear) {
            u.CardExpYear = expyear;
            Assert.AreEqual(expyear, u.CardExpYear);
        }

        //CardExpYear property illegal values
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("24")]
        [DataRow("0")]
        [DataRow("9")]
        [DataRow("0b")]
        [DataRow("30")]
        public void CardExpYearIllegal(string cardexpyear)
        {
            u.CardExpYear = cardexpyear;
        }

        //CearExpYear proterty null/empty values
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null)]
        [DataRow("")]
        public void CearExpYearNull(string expyear)
        {
            u.CardExpYear = expyear;
        }

        //Level property
        [TestMethod]
        [DataRow((int)LevelsEnum.Trænet)]
        [DataRow((int)LevelsEnum.Professionel)]
        [DataRow((int)LevelsEnum.Begynder)]
        [DataRow(3)]
        public void EnumTest(int level)
        {
            u.Level = level;
            Assert.AreEqual(level, u.Level);
        }

        //Level property illegal values
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(4)]
        [DataRow(-1)]
        public void EnumTestIllegal(int level)
        {
            u.Level = level;
        }

        //Level property null/empty values
        [TestMethod]
        [DataRow(null)]
        public void EnumTestNull(object o)
        {
            Assert.ThrowsException<NullReferenceException>(() => u.Level = (int)o);
        }

        //MuscleGroups property
        [TestMethod]
        [DataRow(MuscleGroupEnum.Ben)]
        [DataRow(MuscleGroupEnum.Skulder)]
        [DataRow(4)]
        public void MuscleGroupEnumTest(MuscleGroupEnum msgroup)
        {
            List<MuscleGroupEnum> msgroups = new List<MuscleGroupEnum> { msgroup };
            u.MuscleGroups = msgroups;
            Assert.AreEqual(msgroups, u.MuscleGroups);

        }

        //MuscleGrupsEnum property null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null)]
        public void MusclegroupsEnumNull(object o)
        {
            if (o == null)
            {
                u.MuscleGroups = null;
            }

        }

        //DaysEnum property
        [TestMethod]
        [DataRow(DaysEnum.Søndag)]
        [DataRow(DaysEnum.Lørdag)]
        [DataRow(0)]
        public void DaysEnumTest(DaysEnum day) {
            List<DaysEnum> days = new List<DaysEnum> { day };
            u.Days = days;
            Assert.AreEqual(days, u.Days);
        }

        //DaysEnum null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null)]
        public void DaysEnumNull(object o) {
            u.Days = (List<DaysEnum>)o;
        }

        //Subscription property
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void Subscription(int sub)
        {
            u.Subscription = sub;
            Assert.AreEqual(sub, u.Subscription);
        }

        //Subscription property illegal values
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(-1)]
        [DataRow(3)]
        public void SubscriptionIllegal(int sub)
        {
            u.Subscription = sub;
        }

        //Subscription proterty null/empty values
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        [DataRow(null)]
        public void SubscriptionNull(object o)
        {
            u.Subscription = (int) o;
        }
    }
}
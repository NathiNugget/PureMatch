using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Model
{
    public class User
    {
        private int _userid;
        private string _name;
        private string _username;
        private string _password;
        private string _phonenumber;
        private string _email;
        private string _cardnumber;
        private string _cardcvc;
        private string _cardexpmonth;
        private string _cardexpyear;
        private int _level;
        private List<MuscleGroupEnum> _musclegroups;
        private List<DaysEnum> _days;
        private int _subscription;

        public User(int userid, string name, string username, string password, string phonenumber, string email, string cardnumber, string cardcvc, string cardexpmonth, string expyear, SubscriptionEnum subscription, LevelsEnum level)
        {
            UserID = userid;
            Name = name;
            UserName = username;
            Password = password;
            PhoneNumber = phonenumber;
            Email = email;
            CardNumber = cardnumber;
            CardCVC = cardcvc;
            CardExpMonth = cardexpmonth;
            CardExpYear = expyear;
            Subscription = (int) subscription;
            Level = (int) level;
        }

        

        public int UserID { get => _userid; set => _userid = value; }
        public string Name { get => _name; set => _name = value; }
        public string UserName { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string PhoneNumber { get => _phonenumber; set => _phonenumber = value; }
        public string Email { get => _email; set => _email = value; }
        public string CardNumber { get => _cardnumber; set => _cardnumber = value; }
        public string CardCVC { get => _cardcvc; set => _cardcvc = value; }
        public string CardExpMonth { get => _cardexpmonth; set => _cardexpmonth = value; }
        public string CardExpYear { get => _cardexpyear; set => _cardexpyear = value; }
        public int Level { get => _level; set => _level = value; }
        public List<MuscleGroupEnum> MuscleGroups { get => _musclegroups; set => _musclegroups = value; }
        public List<DaysEnum> Days { get => _days; set => _days = value; }
        public int Subscription { get => _subscription; set => _subscription = value; }

        public override string ToString()
        {
            return $"{{{nameof(UserID)}={UserID.ToString()}, {nameof(Name)}={Name}, {nameof(UserName)}={UserName}, {nameof(Password)}={Password}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(CardNumber)}={CardNumber}, {nameof(CardCVC)}={CardCVC}, {nameof(CardExpMonth)}={CardExpMonth}, {nameof(CardExpYear)}={CardExpYear}, {nameof(Level)}={Level.ToString()}, {nameof(MuscleGroups)}={MuscleGroups}, {nameof(Days)}={Days}, {nameof(Subscription)}={Subscription.ToString()}}}";
        }
    }
}

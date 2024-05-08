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
        private string _expyear;
        private int _level;
        private List<MuscleGroupEnum> _musclegroups;
        private List<DaysEnum> _days;
        private int _subscription;

        public User(int userid, string name, string username, string password, string phonenumber, string email, LevelsEnum level, string cardnumber, string cardcvc, string cardexpmonth, string expyear, SubcriptionEnum subcription)
        {
            Userid = userid;
            Name = name;
            Username = username;
            Password = password;
            Phonenumber = phonenumber;
            Email = email;
            Cardnumber = cardnumber;
            Cardcvc = cardcvc;
            Cardexpmonth = cardexpmonth;
            Expyear = expyear;
            Subscription = (int) subcription;
            Level = (int) level;
        }

        

        public int Userid { get => _userid; set => _userid = value; }
        public string Name { get => _name; set => _name = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Phonenumber { get => _phonenumber; set => _phonenumber = value; }
        public string Email { get => _email; set => _email = value; }
        public string Cardnumber { get => _cardnumber; set => _cardnumber = value; }
        public string Cardcvc { get => _cardcvc; set => _cardcvc = value; }
        public string Cardexpmonth { get => _cardexpmonth; set => _cardexpmonth = value; }
        public string Expyear { get => _expyear; set => _expyear = value; }
        public int Level { get => _level; set => _level = value; }
        public List<MuscleGroupEnum> Musclegroups { get => _musclegroups; set => _musclegroups = value; }
        public List<DaysEnum> Days { get => _days; set => _days = value; }
        public int Subscription { get => _subscription; set => _subscription = value; }

        public override string ToString()
        {
            return $"{{{nameof(Userid)}={Userid.ToString()}, {nameof(Name)}={Name}, {nameof(Username)}={Username}, {nameof(Password)}={Password}, {nameof(Phonenumber)}={Phonenumber}, {nameof(Email)}={Email}, {nameof(Cardnumber)}={Cardnumber}, {nameof(Cardcvc)}={Cardcvc}, {nameof(Cardexpmonth)}={Cardexpmonth}, {nameof(Expyear)}={Expyear}, {nameof(Level)}={Level.ToString()}, {nameof(Musclegroups)}={Musclegroups}, {nameof(Days)}={Days}, {nameof(Subscription)}={Subscription.ToString()}}}";
        }
    }
}

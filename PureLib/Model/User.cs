using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string phonefilter = "^\\d{8}$";
        private string mailfilter = "^[A-Za-z.0-9]+\\@[A-Za-z0-9]+\\.[A-Za-z]+$";
        private string cardnumberfilter = "^\\d{16}$";
        private string cardcvcfilter = "^\\d{3}$";
        private string cardexpmonthfilter = "^\\d{2}$";
        private string cardexpyearfilter = "^\\d{2}$";

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
            Subscription = (int)subscription;
            Level = (int)level;
            _days = new List<DaysEnum>();
            _musclegroups = new List<MuscleGroupEnum>();
        }

        public User()
        {
            UserID = 1;
            Name = "dummy";
            UserName = "dummy";
            Password = "dummy";
            PhoneNumber = "11111111";
            Email = "dummy@dummymail.com";
            CardNumber = "1111222233334444";
            CardCVC = "123";
            CardExpMonth = "05";
            CardExpYear = "25";
            Subscription = (int)SubscriptionEnum.Core;
            Level = (int)LevelsEnum.Begynder;
        }



        public int UserID
        {
            get => _userid; set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Ulovligt UserID: {value}");
                }
                _userid = value;
            }
        }
        public string Name
        {
            get => _name; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal angive et navn");
                }
                else if (value.Length < 3)
                {
                    throw new ArgumentException($"Navn skal være 3 tegn eller mere. Du skrev: {value}");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException($"Navn må ikke være længere end 50 tegn. Du skrev: {value.Length} tegn.");
                }
                _name = value;
            }
        }

        public string UserName
        {
            get => _username; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive et brugernavn");
                }
                else if (value.Length < 3)
                {
                    throw new ArgumentException($"Du skal skrive et navn længere end 2 tegn. Du skrev: {value}");
                }
                else if (value.Length > 30)
                {
                    throw new ArgumentException($"Brugernavn må ikke være længere end 30 tegn. Du skrev: {value.Length} tegn.");
                }
                _username = value;
            }
        }
        public string Password
        {
            get => _password; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive et kodeord");
                }
                if (value.Length < 3)
                {
                    throw new ArgumentException($"Adgangskode skal være mere end 2 tegn. Du skrev: {value.Length} tegn.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException($"Adgangskode må ikke være længere end 50 tegn. Du skrev: {value.Length} tegn");
                }
                _password = value;
            }
        }
        public string PhoneNumber
        {
            get => _phonenumber; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive et 8-cifret telefonnummer");
                }
                if (value.Length != 8)
                {
                    throw new ArgumentException($"Du skal skrive 8 cifre. Du skrev: {value.Length}");
                }
                if (!new Regex(phonefilter).IsMatch(value))
                {
                    throw new ArgumentException($"Specified value is not of format ########. You inputted: {value}");
                }
                _phonenumber = value;

            }
        }
        public string Email
        {
            get => _email; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive en email");
                }
                if (!new Regex(mailfilter).IsMatch(value))
                {
                    throw new ArgumentException($"Mail skal være på formatet teksther@domæne.tld\nDu skrev: {value}.");
                }
                _email = value;
            }
        }
        public string CardNumber
        {
            get => _cardnumber; 
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($"Kort-nummer skal være på formen: 16 cifre eller 16#");
                }
                if (value.Length != 16)
                {
                    throw new ArgumentException($"Kort-nummer skal være på formen: 16 cifre eller 16#\nDu skrev {value}");
                }
                if (!new Regex(cardnumberfilter).IsMatch(value))
                {
                    throw new ArgumentException($"Kort-nummer skal være 16 cifre. Du skrev: {value}");
                }
                _cardnumber = value;
            }
        }

        public string CardCVC
        {
            get => _cardcvc; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive et CVC for kort, som er 3 cifre.");
                }
                if (!new Regex(cardcvcfilter).IsMatch(value))
                {
                    throw new ArgumentException($"CVC skal skrives på formen ### eller 3 cifre. Du skrev: {value}");
                }
            }
        }
        public string CardExpMonth
        {
            get => _cardexpmonth; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive en udløbsmåned på formen ##, f.eks. 05 hvis dit kort løber i maj måned");
                }
                if (value.Length != 2)
                {
                    throw new ArgumentException($"Du skal skrive 2 cifre, f.eks. 02 for februar");
                }
                if (!new Regex(cardexpmonthfilter).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre på mønstret ##");
                }
                _cardexpmonth = value;
            }
        }
        public string CardExpYear
        {
            get => _cardexpyear; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal angive to cifre for udløbsår på formen ##");
                }
                if (value.Length != 2)
                {
                    throw new ArgumentException("Du skal skrive 2 cifre på formen ##");
                }
                if (!new Regex(cardexpyearfilter).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre på formen ## - f.eks. 25 for år 2025");
                }
            }
        }
        public int Level
        {
            get => _level; set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    throw new ArgumentNullException("Du skal angive et niveau for din konto");
                }
                if (value > (int)Enum.GetValues(typeof(LevelsEnum)).Length)
                {
                    throw new ArgumentException("Der er 3 niveauer i PureMatch, du kan max angive 0, 1 eller 2. 3 er reserveret værdi til når brugeren nulstiller level");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Du må ikke angive en værdi for level under 0");
                }
                _level = value;
            }
        }
        public List<MuscleGroupEnum> MuscleGroups
        {
            get => _musclegroups; set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Null angive hvor List<MuscleGroupsEnum> var forventet");
                }
                if (value.GetType() != typeof(List<MuscleGroupEnum>))
                {
                    throw new ArgumentException($"Forventet input: {typeof(List<MuscleGroupEnum>)}. Angivet: {value.GetType()}");
                }
                _musclegroups = value;
            }
        }
        public List<DaysEnum> Days
        {
            get => _days; set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Null angivet hvor List<DaysEnum> var forventet");
                }
                if (value.GetType() != typeof(List<DaysEnum>))
                {
                    throw new ArgumentException($"Forventet input: {typeof(List<DaysEnum>)}. Angivet: {value.GetType()}");
                }
                _days = value;
            }
        }
        public int Subscription
        {
            get => _subscription; set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    throw new NullReferenceException("Null angivet hvor heltalsværdi var forventet");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Værdi for abonnement må ikke være under 0.");
                }
                if (value >= Enum.GetValues(typeof(SubscriptionEnum)).Length)
                {
                    throw new ArgumentException($"Værdi mellem 0 og 2 forventet. Angive: {value}");
                }
                _subscription = value;
            }
        }

        public override string ToString()
        {
            return $"{{{nameof(UserID)}={UserID.ToString()}, {nameof(Name)}={Name}, {nameof(UserName)}={UserName}, {nameof(Password)}={Password}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(CardNumber)}={CardNumber}, {nameof(CardCVC)}={CardCVC}, {nameof(CardExpMonth)}={CardExpMonth}, {nameof(CardExpYear)}={CardExpYear}, {nameof(Level)}={Level.ToString()}, {nameof(MuscleGroups)}={MuscleGroups}, {nameof(Days)}={Days}, {nameof(Subscription)}={Subscription.ToString()}}}";
        }
    }
}

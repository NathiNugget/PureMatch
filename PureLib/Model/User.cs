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

        #region validation related variables
        private static DateTime CURRENTTIME = DateTime.Now;
        private static int YEARDIGITS =  int.Parse(CURRENTTIME.ToString("yy"));
        private const string PHONEFILTER = "^\\d{8}$";
        private const string MAILFILTER = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        private const string CARDNUMBERFILTER = "^\\d{16}$";
        private const string CARDCVCFILTER = "^\\d{3}$";
        private const string CARDEXPMONTHFILTER = "^\\d{2}$";
        private const string CARDEXPYEARFILTER = "^\\d{2}$";
        #endregion

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
                    throw new ArgumentException($"Navn skal v�re 3 tegn eller mere. Du skrev: {value}");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException($"Navn m� ikke v�re l�ngere end 50 tegn. Du skrev: {value.Length} tegn.");
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
                    throw new ArgumentException($"Du skal skrive et navn l�ngere end 2 tegn. Du skrev: {value}");
                }
                else if (value.Length > 30)
                {
                    throw new ArgumentException($"Brugernavn m� ikke v�re l�ngere end 30 tegn. Du skrev: {value.Length} tegn.");
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
                    throw new ArgumentException($"Adgangskode skal v�re mere end 2 tegn. Du skrev: {value.Length} tegn.");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException($"Adgangskode m� ikke v�re l�ngere end 50 tegn. Du skrev: {value.Length} tegn");
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
                if (!new Regex(PHONEFILTER).IsMatch(value))
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
                if (!new Regex(MAILFILTER).IsMatch(value.ToLower()))
                {
                    throw new ArgumentException($"Mail skal v�re p� formatet teksther@dom�ne.tld\nDu skrev: {value}.");
                }
                _email = value.ToLower();
            }
        }
        public string CardNumber
        {
            get => _cardnumber; 
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($"Kort-nummer skal v�re p� formen: 16 cifre eller 16#");
                }
                
                if (!new Regex(CARDNUMBERFILTER).IsMatch(value))
                {
                    throw new ArgumentException($"Kort-nummer skal v�re 16 cifre. Du skrev: {value}");
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
                if (!new Regex(CARDCVCFILTER).IsMatch(value))
                {
                    throw new ArgumentException($"CVC skal skrives p� formen ### eller 3 cifre. Du skrev: {value}");
                }
                _cardcvc = value;
            }
        }
        public string CardExpMonth
        {
            get => _cardexpmonth; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Du skal skrive en udl�bsm�ned p� formen ##, f.eks. 05 hvis dit kort l�ber i maj m�ned");
                }
                if (!new Regex(CARDEXPMONTHFILTER).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre p� m�nstret ##");
                }
                if (int.Parse(value) < 1 || int.Parse(value) > 12)
                {
                    throw new ArgumentException("Du skal skrive en m�ned mellem 01 og 12");
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
                    throw new ArgumentNullException("Du skal angive to cifre for udl�bs�r p� formen ##");
                }
                if (!new Regex(CARDEXPYEARFILTER).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre p� formen ## - f.eks. 25 for �r 2025");
                }
                
                if (int.Parse(value) < YEARDIGITS+1 || int.Parse(value) > YEARDIGITS+5)
                {
                    throw new ArgumentException($"Du skal skrive et �r mellem {YEARDIGITS+1} og {YEARDIGITS+5}");
                }
                _cardexpyear = value;
            }
        }
        public int Level
        {
            get => _level; 
            set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    throw new ArgumentNullException("Du skal angive et niveau for din konto");
                }
                if (value > Enum.GetValues(typeof(LevelsEnum)).Length)
                {
                    throw new ArgumentException("Der er 3 niveauer i PureMatch, du kan max angive 0, 1 eller 2. 3 er reserveret v�rdi til n�r brugeren nulstiller level");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Du m� ikke angive en v�rdi for level under 0");
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
                    throw new ArgumentNullException("Null angive hvor List<MuscleGroupsEnum> var forventet");
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
                    throw new ArgumentNullException("Null angivet hvor List<DaysEnum> var forventet");
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
            get => _subscription; 
            set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    throw new ArgumentNullException("Null angivet hvor heltalsv�rdi var forventet");
                }
                if (value < 0)
                {
                    throw new ArgumentException("V�rdi for abonnement m� ikke v�re under 0.");
                }
                if (value >= Enum.GetValues(typeof(SubscriptionEnum)).Length)
                {
                    throw new ArgumentException($"V�rdi mellem 0 og 2 forventet. Angive: {value}");
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

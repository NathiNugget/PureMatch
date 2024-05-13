using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PureLib.Model
{
    /// <summary>
    /// Class which represents a user in the system. The class has a constructor for when read from database
    /// as well as a default constructor for when one is needed. 
    /// </summary>
    public class User
    {
        #region instance fields
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
        #endregion

        /// <summary>
        /// Constructor using paramters. Parameters are passed to the properties which confirm values are valid
        /// </summary>
        /// <param name="userid">ID for the user. Usually one is provided from the database, but when not instantiating from the database, just use 1</param>
        /// <param name="name">Name for the user. This is the name shown to other users. Full name of the user is expected in this field</param>
        /// <param name="username">Username is used for login and not shown to other users</param>
        /// <param name="password">Password is used for login and not shown to other users</param>
        /// <param name="phonenumber">Phonenumber is only shown on "my profile" page. Expected value is 8 digits</param>
        /// <param name="email">Email is only shown on "my profile" page. Expected form is of usual email-form</param>
        /// <param name="cardnumber">Cardnumber is only used for profile-creation. Expected value is a string with 16 digits</param>
        /// <param name="cardcvc">CVC is only used for profile-creation. Expected value is a string with 3 digits</param>
        /// <param name="cardexpmonth">Expiration date is only used for profile-creation. Expected value is a string with 2 digits</param>
        /// <param name="expyear">Expiration year is only used for profile-creation. Expected value is a string with 2 digits</param>
        /// <param name="subscription">Subscription is either an integer value or an enumeration of type SubscriptionsEnum. Expected values are within the enum</param>
        /// <param name="level">Level is which level the user currently has. The value is expected to be within enum values while 3 is reserved for when the user is not in matching-mode</param>
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

        /// <summary>
        /// Default constructor for when the full constructor is not needed
        /// </summary>
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

        /// <summary>
        /// The properties all follow limitations of the database design. A number of properties make use of a Regex or DateTime shown in ValidationRegex.cs
        /// </summary>
        #region Properties
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
                if (!new Regex(ValidationRegex.PHONEFILTER).IsMatch(value))
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
                if (!new Regex(ValidationRegex.MAILFILTER).IsMatch(value.ToLower()))
                {
                    throw new ArgumentException($"Mail skal være på formatet teksther@domæne.tld\nDu skrev: {value}.");
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
                    throw new ArgumentNullException($"Kort-nummer skal være på formen: 16 cifre eller 16#");
                }
                
                if (!new Regex(ValidationRegex.CARDNUMBERFILTER).IsMatch(value))
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
                if (!new Regex(ValidationRegex.CARDCVCFILTER).IsMatch(value))
                {
                    throw new ArgumentException($"CVC skal skrives på formen ### eller 3 cifre. Du skrev: {value}");
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
                    throw new ArgumentNullException("Du skal skrive en udløbsmåned på formen ##, f.eks. 05 hvis dit kort løber i maj måned");
                }
                if (!new Regex(ValidationRegex.CARDEXPMONTHFILTER).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre på mønstret ##");
                }
                if (int.Parse(value) < 1 || int.Parse(value) > 12)
                {
                    throw new ArgumentException("Du skal skrive en måned mellem 01 og 12");
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
                if (!new Regex(ValidationRegex.CARDEXPYEARFILTER).IsMatch(value))
                {
                    throw new ArgumentException("Du skal skrive 2 cifre på formen ## - f.eks. 25 for år 2025");
                }
                
                if (int.Parse(value) < ValidationRegex.YEARDIGITS+1 || int.Parse(value) > ValidationRegex.YEARDIGITS+5)
                {
                    throw new ArgumentException($"Du skal skrive et år mellem {ValidationRegex.YEARDIGITS+1} og {ValidationRegex.YEARDIGITS+5}");
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
                    throw new ArgumentNullException("Null angivet hvor heltalsværdi var forventet");
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
        #endregion

        /// <summary>
        /// Override of generic ToString from object
        /// </summary>
        /// <returns>string representation of current user-instance</returns>
        public override string ToString()
        {
            return $"{{{nameof(UserID)}={UserID.ToString()}, {nameof(Name)}={Name}, {nameof(UserName)}={UserName}, {nameof(Password)}={Password}, {nameof(PhoneNumber)}={PhoneNumber}, {nameof(Email)}={Email}, {nameof(CardNumber)}={CardNumber}, {nameof(CardCVC)}={CardCVC}, {nameof(CardExpMonth)}={CardExpMonth}, {nameof(CardExpYear)}={CardExpYear}, {nameof(Level)}={Level.ToString()}, {nameof(MuscleGroups)}={MuscleGroups}, {nameof(Days)}={Days}, {nameof(Subscription)}={Subscription.ToString()}}}";
        }
    }
}

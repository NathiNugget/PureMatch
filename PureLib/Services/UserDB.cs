using PureLib.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Services
{
    public class UserDB
    {
        private List<User> _users = new List<User>();
        private string ConnectionString = Secret.ConnectionString;
        public UserDB()
        {

        }

        public User AddUser(User user)
        {

            string query = "insert into PureUser ([Name], UserName, [Password], PhoneNumber, Email, CardNumber, CardCVC, CardExpMonth, CardYyear, Subscription, [Level]) values (@PName, @PUserName, @PPassword, @PPhoneNumber,@PEmail, @PCardNumber, @PCardCVC, @PCardExpMonth, @PCardExpYear, @PSubscription, @PLevel)"; 
            
            return user;
        }

        public User ReadUser(int id)
        {
            return _users.Find(u => u.Userid == id)!; 
        }

        


    }
}

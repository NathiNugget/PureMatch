using PureLib.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureLib.Services
{
    public class DataBaseReader
    {
        private List<Message> _messages = new List<Message>();
        public DataBaseReader() {
            
            List<User> users = GetChatUsers(1);
            foreach (var item in users)
            {
                Console.WriteLine(item);
            }

        }

        private string ConnectionString = Secret.ConnectionString;

        public List<Message> ReadAllMessagesFromDB()
        {
            List<Message> messages = new List<Message>();
            string query = "select * from PureMessage"; 

            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader(); 
                while (reader.Read())
                {
                    messages.Add(ReadMessage(reader)); 
                }
            }
            return messages;
        }

        public List<User> GetChatUsers(int userid)
        {
            List<User> users = new List<User>();
            string query = "select * from PureUser where UserID in (select distinct UserID from (PureUser pu full join PureMessage pm ON pu.UserID = pm.SenderID ) where RecipientID = 2)";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                
                cmd.Parameters.AddWithValue("@PID", userid);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    users.Add(ReadUser(r));
                }
            }
            return users;
        }

        public List<User> GetChatUsers()
        {
            List<User> users = new List<User>();
            string query = "select * from ChatList";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(ReadUser(reader));
                }
            }
            return users;
        }

            private Message ReadMessage(SqlDataReader reader)
        {
            return new Message(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4)); 
        }

        // ******************************************************************
        /* 
         * 
         *                      From here on is primarily user-related stuff from DB
         * 
         * 
         * 
         * 
         * 
         * */

        public User AddUser(User user)
        {


            string query = "insert into PureUser (Name, UserName, Password, PhoneNumber, Email, CardNumber, CardCVC, CardExpMonth, CardExpYear, Subscription, [Level]) values (@PName, @PUserName, @PPassword, @PPhoneNumber,@PEmail, @PCardNumber, @PCardCVC, @PCardExpMonth, @PCardExpYear, @PSubscription, @PLevel)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (user != null)
                {
                    cmd.Parameters.AddWithValue("@PName", user.Name);
                    cmd.Parameters.AddWithValue("@PUserName", user.UserName);
                    cmd.Parameters.AddWithValue("@PPassword", user.Password);
                    cmd.Parameters.AddWithValue("@PPhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@PEmail", user.Email);
                    cmd.Parameters.AddWithValue("@PCardNumber", user.CardNumber);
                    cmd.Parameters.AddWithValue("@PCardCVC", user.CardCVC);
                    cmd.Parameters.AddWithValue("@PCardExpMonth", user.CardExpMonth);
                    cmd.Parameters.AddWithValue("@PCardExpYear", user.CardExpYear);
                    cmd.Parameters.AddWithValue("@PSubscription", user.Subscription);
                    cmd.Parameters.AddWithValue("@PLevel", user.Level);
                }

                int rowsaffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsaffected} rows affected");
            }


            return user;
        }

        public List<User> ReadAllUsersFromDB()
        {
            List<User> users = new List<User>();
            string query = "select * from PureUser";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(ReadUser(reader));
                }

            }
            return users;
        }



        private User ReadUser(SqlDataReader reader)
        {
            return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), (SubscriptionEnum)reader.GetInt32(10), (LevelsEnum)reader.GetInt32(11));
        }
    }
}

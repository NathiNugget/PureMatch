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

            List<User> matches = GetMatches(2);
            foreach (var item in users)
            {
                Console.WriteLine(item);
            }

        }

        private List<User> GetMatches(int userid)
        {
            List<User> users = new List<User>();
            string query = "create view Matches as " +
                "(select pu.UserID, pu.[Name], pu.[Password], PhoneNumber, Email, CardNumber, CardCVC, CardExpMonth, CardExpYear, Subscription, [Level] from PureUser AS pu " +
                "join PureDays pd on pd.UserID = pu.UserID " +
                "join PureMuscleGroups pmg on pu.UserID = pmg.UserID " +
                "where (pd.Monday = (select pd.Monday from PureDays where UserID = @PID) OR " +
                "pd.Tuesday = (select pd.Tuesday from PureDays where UserID = @PID) OR " +
                "pd.Wednesday = (select pd.Wednesday from PureDays where UserID = @PID) OR " +
                "pd.Thursday = (select pd.Thursday from PureDays where UserID = @PID) OR " +
                "pd.Friday = (select pd.Friday from PureDays where UserID = @PID) OR " +
                "pd.Saturday = (select pd.Saturday from PureDays where UserID = @PID) OR " +
                "pd.Sunday = (select pd.Sunday from PureDays where UserID = @PID)) " +
                "and (pmg.Back = (select pmg.Back from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Biceps = (select pmg.Biceps from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Chest = (select pmg.Chest from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Core = (select pmg.Core from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Legs = (select pmg.Legs from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Shoulders = (select pmg.Shoulders from PureMuscleGroups where UserID = @PID) OR " +
                "pmg.Triceps = (select pmg.Triceps from PureMuscleGroups where UserID = @PID)) " +
                "and pu.UserID not in (select UserID from PureUser " +
                "where UserID in (select distinct UserID from (PureUser pu join PureMessage pm ON pu.UserID = pm.SenderID) " +
                "where RecipientID = @PID)) and pu.UserID != @PID)";
                
            using (SqlConnection connection = new SqlConnection(ConnectionString)) { 
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid); 
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(ReadUser(reader));
                }
            }
            return users;
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
            string query = "select * from PureUser where UserID in (select distinct UserID from (PureUser pu full join PureMessage pm ON pu.UserID = pm.SenderID ) where RecipientID = @PID)";
            
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

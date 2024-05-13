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
            foreach (var item in matches)
            {
                Console.WriteLine(item);
            }

        }

        public List<User> GetMatches(int userid)
        {
            List<User> users = new List<User>();
            string query = 
                "(select pu.UserID, pu.[Name], pu.UserName, pu.[Password], PhoneNumber, Email, CardNumber, CardCVC, CardExpMonth, CardExpYear, Subscription, [Level] from PureUser AS pu " +
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
                "where UserID in (select distinct UserID from (PureUser pu join PureMessage pm ON pu.UserID = pm.SenderID or pu.UserID = pm.RecipientID) " +
                "where RecipientID = @PID or SenderID = @PID)) and pu.UserID != @PID)";
                
            using (SqlConnection connection = new SqlConnection(ConnectionString)) { 
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid); 
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(ReadUserUsingReader(reader));
                }
            }
            return users;
        }

        public List<MuscleGroupEnum> ReadMuscleGroups(int userid)
        {
            List <MuscleGroupEnum> msgroups = new List<MuscleGroupEnum>();
            string query = "select * from PureMuscleGroups where UserID = @PID";
            
            using(SqlConnection connection = new SqlConnection(ConnectionString)) {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (MuscleGroupEnum group in ReadMuscleGroups(reader)) {
                        msgroups.Add(group);
                    }
                        
                }
            }
            return msgroups; 

        }

        private List<MuscleGroupEnum> ReadMuscleGroups(SqlDataReader reader)
        {
            List<MuscleGroupEnum> msgroups = new List<MuscleGroupEnum>(); 
            bool chest = reader.GetBoolean(1);
            bool back = reader.GetBoolean(2);
            bool shoulders = reader.GetBoolean(3); 
            bool legs = reader.GetBoolean(4);
            bool biceps = reader.GetBoolean(5);
            bool triceps = reader.GetBoolean(6);
            bool core = reader.GetBoolean(7);
            if (chest)
            {
                msgroups.Add(MuscleGroupEnum.Bryst); 

            }
            if (back)
            {
                msgroups.Add(MuscleGroupEnum.Ryg);
            }
            if (shoulders)
            {
                msgroups.Add(MuscleGroupEnum.Skulder);
            }
            if (legs)
            {
                msgroups.Add(MuscleGroupEnum.Ben);
            }
            if (biceps)
            {
                msgroups.Add(MuscleGroupEnum.Biceps); 
            }
            if (core)
            {
                msgroups.Add(MuscleGroupEnum.Mave); 
            }
            return msgroups;


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
                    users.Add(ReadUserUsingReader(r));
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
                    users.Add(ReadUserUsingReader(reader));
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
                    users.Add(ReadUserUsingReader(reader));
                }

            }
            return users;
        }

        public User ReadUser(int userid)
        {
            User? u = null;
            string query = "select * from PureUser where UserID = @PID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid); 
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    u = ReadUserUsingReader(reader);
                }

            }
            return u!; 

        }

        private User ReadUserUsingReader(SqlDataReader reader)
        {
            return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), (SubscriptionEnum)reader.GetInt32(10), (LevelsEnum)reader.GetInt32(11));
        }

        public List<DaysEnum> ReadDays(int userid)
        {
            List<DaysEnum> days = new List<DaysEnum>();
            string query = "select * from PureDays where UserID = @PID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    days = ReadDaysUsingReader(reader); 
                }

            }
            return days;
        }

        private List<DaysEnum> ReadDaysUsingReader(SqlDataReader reader)
        {
            List<DaysEnum> days = new List<DaysEnum>(); 
            if (reader.GetBoolean(1))
            {
                days.Add(DaysEnum.Mandag); 
            }
            if (reader.GetBoolean(2))
            {
                days.Add(DaysEnum.Tirsdag); 
            }
            if (reader.GetBoolean(3))
            {
                days.Add(DaysEnum.Onsdag); 
            }
            if (reader.GetBoolean(4))
            {
                days.Add(DaysEnum.Torsdag); 
            }
            if (reader.GetBoolean(5))
            {
                days.Add(DaysEnum.Fredag); 
            }
            if (reader.GetBoolean(6))
            {
                days.Add(DaysEnum.Lørdag); 
            } 
            if (reader.GetBoolean(7))
            {
                days.Add(DaysEnum.Søndag); 
            }
            return days; 

        }

        public int SendMessage(int ownid, int matchid, string messagecontent)
        {
            int rowsaffected = 0; 
            string query = "insert into PureMessage (SenderID, RecipientID, MessageValue) values (@PID, @PMID, @PMC)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", ownid);
                cmd.Parameters.AddWithValue ("@PMID", matchid);
                cmd.Parameters.AddWithValue("@PMC", messagecontent); 
                rowsaffected = cmd.ExecuteNonQuery();
            }
            return rowsaffected;

        }
    }
}

using PureLib.Model;
using System.Data.SqlClient;

namespace PureLib.Services
{
    public class DataBaseLink : IDB
    {
        private List<Message> _messages = new List<Message>();
        public DataBaseLink()
        {



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

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
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
            List<MuscleGroupEnum> msgroups = new List<MuscleGroupEnum>();
            string query = "select * from PureMuscleGroups where UserID = @PID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    foreach (MuscleGroupEnum group in ReadMuscleGroupsUsingReader(reader))
                    {
                        msgroups.Add(group);
                    }

                }
            }
            return msgroups;

        }

        private List<MuscleGroupEnum> ReadMuscleGroupsUsingReader(SqlDataReader reader)
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
            if (triceps)
            {
                msgroups.Add(MuscleGroupEnum.Triceps);
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

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            //string query = "select * from PureUser where UserID in (select distinct UserID from (PureUser pu full join PureMessage pm ON pu.UserID = pm.SenderID ) where RecipientID = @PID OR SenderID = @PID)";
            string query = "select * from PureUser where UserID in (select distinct UserID from (PureUser pu join " +
                "PureMessage pm on pu.UserID = pm.SenderID or pu.UserID = pm.RecipientID) where " +
                "pm.RecipientID = @PID or pm.SenderID = @PID) and UserID != @PID";
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




        private Message ReadMessage(SqlDataReader reader)
        {
            return new Message(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4));
        }

        public Message GetMessage(int messageid)
        {
            string query = "select * from PureMessage where MessageID = @MID";
            Message msg = null!;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MID", messageid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    msg = ReadMessageUsingReader(reader);
                }
            }
            return msg;

        }

        public int DeleteMessage(int messageid)
        {

            string query = "delete  from PureMessage where MessageID = @MID";
            int rowsaffected = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MID", messageid);
                rowsaffected = cmd.ExecuteNonQuery();
            }
            return rowsaffected;


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

        public int AddUser(User user)
        {

            int rowsaffected = 0;
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

                rowsaffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsaffected} rows affected");
            }


            return rowsaffected;
        }

        public int UpdateUser(User user)
        {
            string query = "update PureUser" +
                " set Name = @PName, " +
                "UserName = @PUserName, " +
                "Password = @PPassword, " +
                "PhoneNumber = @PPhoneNumber, " +
                "Email = @PEmail, " +
                "CardNumber = @PCardNumber, " +
                "CardCVC = @PCardCVC, " +
                "CardExpMonth = @PCardExpMonth, " +
                "CardExpYear = @PCardExpYear, " +
                "Subscription = @PSubscription, " +
                "[Level] = @PLevel " +
                "where UserID = @PID";
            int rowsaffected;
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
                    cmd.Parameters.AddWithValue("@PID", user.UserID);
                }

                rowsaffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rowsaffected} rows affected");
            }


            return rowsaffected;
        }

        public User? ReadLogin(string username, string password)
        {
            User? user = null;
            string query = "select * from PureUser where UserName = @PName AND Password = @PPassword";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PName", username);
                cmd.Parameters.AddWithValue("@PPassword", password);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = ReadUserUsingReader(reader);
                }
            }
            return user;

        }

        public int SetMatching(int userid, List<MuscleGroupEnum> msgroups, List<DaysEnum> days, int level)
        {
            int rowsaffected = 0;

            rowsaffected += SetMuscleGroups(userid, msgroups);
            rowsaffected += SetDays(userid, days);
            rowsaffected += SetLevel(userid, level);
            return rowsaffected;
        }

        private int SetLevel(int userid, int level)
        {
            int rowsaffected = 0;
            string query = "update PureUser " +
                "set [Level] = @PLevel where UserID = @PID";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userid);
                cmd.Parameters.AddWithValue("@PLevel", level);
                rowsaffected = cmd.ExecuteNonQuery();
            }
            return rowsaffected;

        }

        private int SetDays(int userid, List<DaysEnum> days)
        {
            int rowsaffected = 0;
            if (days != null) // If list is received
            {

                string query = "insert into PureDays (UserID, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday) values" +
                " (@PID, @PMonday, @PTuesday, @PWednesday, @PThursday, @PFriday, @PSaturday, @PSunday)";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PID", userid);
                    cmd.Parameters.AddWithValue("@PMonday", days.Contains(DaysEnum.Mandag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PTuesday", days.Contains(DaysEnum.Tirsdag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PWednesday", days.Contains(DaysEnum.Onsdag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PThursday", days.Contains(DaysEnum.Torsdag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PFriday", days.Contains(DaysEnum.Fredag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PSaturday", days.Contains(DaysEnum.Lørdag) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PSunday", days.Contains(DaysEnum.Søndag) ? 1 : 0);
                    rowsaffected = cmd.ExecuteNonQuery();

                }

            }
            else // If empty;
            {
                string query = "delete from PureDays where UserID = @PID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PID", userid);
                    rowsaffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsaffected;
        }

        private int SetMuscleGroups(int userid, List<MuscleGroupEnum> msgroups)
        {
            int rowsaffected = 0;
            if (msgroups != null) // Insert record into table
            {
                string query = $"insert into PureMuscleGroups (UserID, Chest, Back, Shoulders, Legs, Biceps, Triceps, Core) values" +
                $" (@PID, @PChest, @PBack, @PShoulders, @PLegs, @PBiceps, @PTriceps, @PCore)";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PID", userid);
                    cmd.Parameters.AddWithValue("@PChest", msgroups.Contains(MuscleGroupEnum.Bryst) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PBack", msgroups.Contains(MuscleGroupEnum.Ryg) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PShoulders", msgroups.Contains(MuscleGroupEnum.Skulder) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PLegs", msgroups.Contains(MuscleGroupEnum.Ben) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PBiceps", msgroups.Contains(MuscleGroupEnum.Biceps) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PTriceps", msgroups.Contains(MuscleGroupEnum.Triceps) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@PCore", msgroups.Contains(MuscleGroupEnum.Mave) ? 1 : 0);
                    rowsaffected = cmd.ExecuteNonQuery();
                }
            }
            else // Delete record from table
            {
                string query = "delete from PureMuscleGroups where UserID = @PID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@PID", userid);
                    rowsaffected = cmd.ExecuteNonQuery();
                }
            }


            return rowsaffected;
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

        public bool IsAvailableUserName(string username)
        {
            bool existsnt = true;
            string query = "select * from PureUser where UserName = @PName";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PName", username);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) // If any records exist, set boolean to false 
                {
                    existsnt = false;
                }
            }
            return existsnt;
        }

        private User ReadUserUsingReader(SqlDataReader reader)
        {
            User u = new User();
            u.UserID = reader.GetInt32(0);
            u.Name = reader.GetString(1);
            u.UserName = reader.GetString(2);
            u.Password = reader.GetString(3);
            u.PhoneNumber = reader.GetString(4);
            u.Email = reader.GetString(5);
            u.CardNumber = reader.GetString(6);
            u.CardCVC = reader.GetString(7);
            u.CardExpMonth = reader.GetString(8);
            u.CardExpYear = reader.GetString(9);
            u.Subscription = reader.GetInt32(10);
            u.Level = reader.GetInt32(11);
            return u;
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

        public (int rowsaffected, int maxid) SendMessage(int ownid, int matchid, string messagecontent)
        {
            int rowsaffected = 0;
            int maxid = 0;
            string query = "insert into PureMessage (SenderID, RecipientID, MessageValue) values (@PID, @PMID, @PMC)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", ownid);
                cmd.Parameters.AddWithValue("@PMID", matchid);
                cmd.Parameters.AddWithValue("@PMC", messagecontent);
                rowsaffected = cmd.ExecuteNonQuery();

            }

            maxid = FindMaxMessageID();
            return (rowsaffected, maxid);

        }

        private int FindMaxMessageID()
        {
            string query = "select max(MessageID) from PureMessage";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                int maxid = 0;
                while (reader.Read())
                {
                    maxid = reader.GetInt32(0);
                }
                return maxid;
            }
        }

        public List<Message> ReadMessages(int userID, int chatid)
        {
            List<Message> messages = new List<Message>();
            string query = "select * from PureMessage where (SenderID = @PID and RecipientID = @CID) or (SenderID = @CID and RecipientID = @PID)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PID", userID);
                cmd.Parameters.AddWithValue("@CID", chatid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(ReadMessageUsingReader(reader));
                }
            }
            return messages;
        }

        private Message ReadMessageUsingReader(SqlDataReader reader)
        {
            return new Message(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(4));
        }
    }
}

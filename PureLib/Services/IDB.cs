using PureLib.Model;

namespace PureLib.Services
{
    public interface IDB
    {
        int AddUser(User user);
        int DeleteMessage(int messageid);
        List<User> GetChatUsers(int userid);
        List<User> GetMatches(int userid);
        Message GetMessage(int messageid);
        bool IsAvailableUserName(string username);
        List<Message> ReadAllMessagesFromDB();
        List<User> ReadAllUsersFromDB();
        List<DaysEnum> ReadDays(int userid);
        User? ReadLogin(string username, string password);
        List<Message> ReadMessages(int userID, int chatid);
        List<MuscleGroupEnum> ReadMuscleGroups(int userid);
        User ReadUser(int userid);
        (int rowsaffected, int maxid) SendMessage(int ownid, int matchid, string messagecontent);
        int SetMatching(int userid, List<MuscleGroupEnum> msgroups, List<DaysEnum> days, int level);
        int UpdateUser(User user);
    }
}
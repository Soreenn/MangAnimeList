using Dapper;
using MySql.Data.MySqlClient;
using System.Collections;

namespace MangAnimeList.DBManager
{
    public class DBManager
    {
        private static MySqlConnection _connection = new("Database=MangAnimeList;Server=localhost;user=root;password=root;");

        public static void OpenDBConnection()
        {
            _connection.Open();
        }

        public static void CloseDBConnection()
        {
            _connection.Close();
        }

        public static void Connect(string server, string database, string user, string password)
        {
            _connection = new($"Database={database};Server={server};user={user};password={password};");
        }

        public static int RegisterUserDB(string query)
        {
            int _result = _connection.Execute(query);

            return _result;
        }

        public static bool IsUsernameUnique(string query, string username)
        {
            bool _result = true;

            IEnumerable _queryResult = _connection.Query(query);

            foreach (dynamic _singleResult in _queryResult)
            {
                if (username == _singleResult.username)
                {
                    _result = false;
                }
            }

            return _result;
        }

        //Session {username, userType, userId}
        //userType = 0 = guest
        //userType = 1 = user
        //userType = 2 = admin
        public static string[] Session = new string[3] { null, "0", null };

        public static bool IsLoginCorrect(string query, string password)
        {
            bool _result = false;

            IEnumerable query_result = _connection.Query(query);

            foreach (dynamic single_result in query_result)
            {
                if (single_result.password == password)
                {
                    _result = true;
                }
            }

            return _result;
        }

        public static int GetUserType(string query)
        {
            int userType = 1;

            IEnumerable query_result = _connection.Query(query);

            foreach (dynamic single_result in query_result)
            {
                userType = single_result.userType;
            }
            return userType;
        }

        public static int AddMediaToList(string query)
        {
            int _result = _connection.Execute(query);

            return _result;
        }

        public static int FinishMedia(string query)
        {
            int _result = _connection.Execute(query);

            return _result;
        }

        public static int GetUserId(string query)
        {
            int userId = 0;

            IEnumerable query_result = _connection.Query(query);

            foreach (dynamic single_result in query_result)
            {
                userId = single_result.id;
            }
            return userId;
        }

        public static IEnumerable Select(string query)
        {

            IEnumerable query_result = _connection.Query(query);

            return query_result;
        }
    }
}

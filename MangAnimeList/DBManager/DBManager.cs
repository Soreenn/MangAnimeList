using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections;

namespace MangAnimeList.DBManager
{
    public class DBManager
    {
        //This may not work beacause of the static methods(May need an object to end a connection)
        private static MySqlConnection _connection = new("Database=MangAnimeList;Server=localhost;user=root;password=;");

        /// <summary>
        /// Opens the connection to the database using the static connection instance
        /// </summary>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="MySql.Data.MySqlClient.MySqlException"></exception>
        public static void OpenDBConnection()
        {
            _connection.Open();
        }
        /// <summary>
        /// Closes the connection to the database using the static connection instance
        /// </summary>
        public static void CloseDBConnection()
        {
            _connection.Close();
        }
        /// <summary>
        /// Changes the static connection during runtime and open/closes it once to test it
        /// </summary>
        /// <param name="server">name/ip address of the sql server</param>
        /// <param name="database">database name used for droxid</param>
        /// <param name="user">database user with CRUD access to the database</param>
        /// <param name="password">user's password</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="MySql.Data.MySqlClient.MySqlException">Thrown when the connection failed to open</exception>
        public static void Connect(string server, string database, string user, string password)
        {
            _connection = new($"Database={database};Server={server};user={user};password={password};");
        }

        public static int RegisterUserDB(string query)
        {
            int result = _connection.Execute(query);

            return result;
        }

        public static bool IsUsernameUnique(string query, string username)
        {
            bool result = true;

            IEnumerable queryResult = _connection.Query(query);

            foreach (dynamic singleResult in queryResult)
            {
                if(username == singleResult.username)
                {
                    result = false;
                }
            }

            return result;
        }

        //Session {username, userType}
        //userType = 1 = user
        //userType = 2 = admin
        public static string[] Session = new string[2] { null, "1" };

        public static bool IsLoginCorrect(string query, string password)
        {
            bool result = false;

            IEnumerable queryResult = _connection.Query(query);

            foreach (dynamic singleResult in queryResult)
            {
                if (singleResult.password == password)
                {
                    result = true;
                }
            }

            return result;
        }

        public static int GetUserType (string query)
        {
            int userType = 1;

            IEnumerable queryResult = _connection.Query(query);

            foreach (dynamic singleResult in queryResult)
            {
                userType = singleResult.userType;
            }
            return userType;
        }

    }
}

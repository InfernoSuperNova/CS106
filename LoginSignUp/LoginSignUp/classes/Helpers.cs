using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using LoginSignUp.classes;
namespace LoginSignUp.classes
{
    public static class UserDatabase
    {
        /// <summary>
        /// Enumerates a column in the CSV database at the column index.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="column"></param>
        /// <returns>Every value in that column.</returns>
        private static List<string> EnumerateFileColumn(string[] database, int column)
        {
            return database.Skip(1).Select(row => row.Split(',')[column]).ToList();
        }
        /// <summary>
        /// Enumerates the UserName column in the CSV database.
        /// </summary>
        /// <param name="database"></param>
        /// <returns>Every UserName in the database.</returns>
        public static List<string> EnumerateUserNames(string[] database)
        {
            return EnumerateFileColumn(database, 0);
        }
        /// <summary>
        /// Enumerates the Password column in the CSV database.
        /// </summary>
        /// <param name="database"></param>
        /// <returns>Every Password in the database.</returns>
        public static List<string> EnumeratePasswords(string[] database)
        {
            return EnumerateFileColumn(database, 1);
        }
        /// <summary>
        /// Enumerates the UserType column in the CSV database.
        /// </summary>
        /// <param name="database"></param>
        /// <returns>Every UserType in the database.</returns>
        public static List<string> EnumerateUserType(string[] database)
        {
            return EnumerateFileColumn(database, 2);
        }
        /// <summary>
        /// Breaks the database up into an array of rows.
        /// </summary>
        /// <returns>An array of rows.</returns>
        public static string[] Read()
        {
            return File.ReadAllLines(@".\Database\UserAccountData.csv");
        }
        
        /// <summary>
        /// Checks if the username and password match an entry in the database.
        /// </summary>
        /// <param name="existingUserNameData"></param>
        /// <param name="existingPasswordData"></param>
        /// <param name="inputUserName"></param>
        /// <param name="inputPassword"></param>
        /// <returns>True if they match.</returns>
        public static bool CheckLogin(List<string> existingUserNameData, List<string> existingPasswordData, string inputUserName, string inputPassword)
        {
            for (int i = 0; i < existingUserNameData.Count(); i++)
            {
                if (inputUserName == existingUserNameData.ElementAt(i) && inputPassword == existingPasswordData.ElementAt(i))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets the index of a user by Username.
        /// </summary>
        /// <param name="UserNames"></param>
        /// <param name="userName"></param>
        /// <returns>The index, -1 if invalid.</returns>
        public static int GetUserIndexByUserName(List<string> UserNames, string userName)
        {
            for (int i = 0; i < UserNames.Count(); i++)
            {
                if (userName == UserNames.ElementAt(i))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Gets the user type by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The user type at the specified index.</returns>
        public static string GetUserTypeByIndex(int index)
        {
            string[] DataBase = Read();
            List<string> Users = EnumerateUserType(DataBase);
            return Users.ElementAt(index);
        }
    }
    public static class ProjectList
    {
        public static string[] Read()
        {
            return File.ReadAllLines(@".\Database\Projects.csv");
        }
        public static void Write(string[] lines)
        {
            File.WriteAllLines(@".\Database\Projects.csv", lines);
        }
    }
    public static class UserInput
    {
        /// <summary>
        /// Validates the input string to constrain to limits.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>true if it passes the checks.</returns>
        public static bool ValidateString(string input)
        {

            foreach(char disallowed in Config.DisallowedCharacters)
            {
                if (input.Contains(disallowed))
                {
                    MessageBox.Show("Character (" + disallowed + ") Is not allowed!");
                    return false;
                }
            }
            if (input.Length < Config.MinChars)
            {
                MessageBox.Show($"Input must be at least {Config.MinChars} characters");
                return false;
            }
            if (input.Length > Config.MaxChars)
            {
                MessageBox.Show($"Input must be at most {Config.MaxChars} characters");
            }
            return true;
        }

    }
    public static class DelayedActionHelper
    {
        public static void DelayedAction(DispatcherPriority priority, TimeSpan delay, Action action)
        {
            DispatcherTimer timer = null;
            var dispatcher = Application.Current.Dispatcher;

            dispatcher.BeginInvoke(priority, new Action(() =>
            {
                var frame = new DispatcherFrame();
                timer = new DispatcherTimer(delay, DispatcherPriority.Normal, (s, e) =>
                {
                    timer.Stop();
                    frame.Continue = false;
                    action.Invoke();
                }, dispatcher);
                Dispatcher.PushFrame(frame);
            }));
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LoginSignUp.classes
{
    public static class Helpers
    {
        private static List<string> EnumerateFileColumn(string[] database, int column)
        {
            return database.Skip(1).Select(row => row.Split(',')[column]).ToList();
        }

        public static List<string> EnumerateUserNames(string[] database)
        {
            return EnumerateFileColumn(database, 0);
        }
        public static List<string> EnumeratePasswords(string[] database)
        {
            return EnumerateFileColumn(database, 1);
        }
        public static List<string> EnumerateUserType(string[] database)
        {
            return EnumerateFileColumn(database, 2);
        }
        public static string[] ReadDataBase()
        {
            return File.ReadAllLines(@".\Database\UserAccountData.csv");
        }
    }
}

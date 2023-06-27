using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginSignUp.classes
{
    public static class SafeIO
    {
        public static string[] ReadAllLines(string path)
        {
            string[] result;
            try
            {
                result = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                //Show a messagebox of the function structure that triggered the error

                MessageBox.Show($"An error occured: {ex.Message}");
                result = new string[0];
            }
            return result;
        }
        public static void WriteAllLines(string path, string[] lines)
        {
            try
            {
                File.WriteAllLines(path, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }
        public static void WriteAllText(string path, string toWrite)
        {
            try
            {
                File.WriteAllText(path, toWrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }
        public static void CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }
        public static string ReadAllText(string path)
        {
            string output;
            try
            {
                output = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
                output = "";
            }
            return output;
        }
    }
}

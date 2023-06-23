using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginSignUp.classes
{
    public static class ProjectDataBase
    {
        public static string[] Read()
        {
            return File.ReadAllLines(@".\Database\Projects.csv");
        }
        public static string[] ReadNoHeader()
        {
            return File.ReadAllLines(@".\Database\Projects.csv").Skip(1).ToArray();
        }
        public static void Write(string[] lines)
        {
            File.WriteAllLines(@".\Database\Projects.csv", lines);
        }
        private static List<string> EnumerateFileColumn(string[] database, int column)
        {
            return database.Skip(1).Select(row => row.Split(',')[column]).ToList();
        }
        public static void InitializeDatabase()
        {
            List<string> projects = EnumerateFileColumn(ReadNoHeader(), 0);
            string rootFolder = @".\Database\Projects";
            foreach(string project in projects)
            {
                CreateDirectory(Config.ROOT_FOLDER, project);
            }
        }
        public static void CreateProject(string newName)
        {
            CreateDirectory(Config.ROOT_FOLDER, newName);
        }
        private static void CreateDirectory(string rootFolder, string project)
        {
            string directory = Path.Combine(rootFolder, project);
            string bugsDirectory = Path.Combine(directory, "bugs");
            try
            {
                Directory.CreateDirectory(directory);
                Directory.CreateDirectory(bugsDirectory);
                if (!File.Exists(Path.Combine(directory, "bugManifest.csv")))
                {
                    File.Create(Path.Combine(directory, "bugManifest.csv")).Close();
                }
                if (!File.Exists(Path.Combine(directory, "nextBugIndex.txt")))
                {
                    File.WriteAllText(Path.Combine(directory, "nextBugIndex.txt"), "0");
                }
                   
                //It's probably inefficient to create the file, open it, close it, then open it again to write, then close it again
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured: {ex.Message}");
            }
        }

        public static void DeleteProject(string name)
        {
            string[] existingProjects = Read();
            //Create a new string
            string[] newLines = new string[0];
            //Fill that with all of lines, sans name of project being removed
            foreach (string oldName in existingProjects)
            {
                if (name == oldName) { continue; }
                newLines = newLines.Append(oldName).ToArray();
            }
            newLines = newLines.Prepend("Project name").ToArray();
            //Overwrite the database
            Write(newLines);
            DeleteDirectory(name);
        }
        private static void DeleteDirectory(string project)
        {
            DeepDelete(Path.Combine(@".\Database\Projects", project));
        }
        //recursively called
        private static void DeepDelete(string directory)
        {
            //Get a list of files
            var files = Directory.GetFiles(directory);
            //Delete the files
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}");
                }
            }
            //Get a list of folders
            var directories = Directory.GetDirectories(directory);
            //Else, recursively run DeepDelete on those folders
            foreach (string subdir in directories)
            {
                DeepDelete(subdir);
            }
           public static void CreateBug(string project, Bug bug)
            {
                string[] bugText = new string[0];
                bugText = bugText.Append(bug.name).ToArray();
                bugText = bugText.Append(bug.priority).ToArray();
                bugText = bugText.Append(bug.timeSpent).ToArray();
                bugText = bugText.Append(bug.description).ToArray();
                bugText = bugText.Append(bug.stepsToReproduce).ToArray();
                int bugIndex = GetNextBugIndex(project);
                SetNextBugIndex(project, bugIndex);
                string path = (Path.Combine(Config.ROOT_FOLDER, project));
                UpdateManifest(path, bugIndex);
                path = Path.Combine(path, "bugs", bugIndex.ToString() + ".txt");
                File.WriteAllLines(path, bugText);
            }
        private static class Helpers
        {
            public static void DeepDelete(string directory)
            {
                //Get a list of files
                var files = Directory.GetFiles(directory);
                //Delete the files
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occured: {ex.Message}");
                    }
                }
                //Get a list of folders
                var directories = Directory.GetDirectories(directory);
                //Else, recursively run DeepDelete on those folders
                foreach (string subdir in directories)
                {
                    DeepDelete(subdir);
                }
                //Once empty, delete
                try
                {
                    Directory.Delete(directory);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}");
                }
            }
        }
    }
}

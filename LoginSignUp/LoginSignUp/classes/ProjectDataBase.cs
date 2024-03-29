﻿using System;
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
            return SafeIO.ReadAllLines(@".\Database\Projects.csv");
        }
        public static string[] ReadNoHeader()
        {
            return SafeIO.ReadAllLines(@".\Database\Projects.csv").Skip(1).ToArray();
        }
        public static void Write(string[] lines)
        {
            SafeIO.WriteAllLines(@".\Database\Projects.csv", lines);
        }
        private static List<string> EnumerateFileColumn(string[] database, int column)
        {
            return database.Skip(1).Select(row => row.Split(',')[column]).ToList();
        }
        public static void InitializeDatabase()
        {
            List<string> projects = EnumerateFileColumn(Read(), 0);
            foreach (string project in projects)
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

            SafeIO.CreateDirectory(directory);
            SafeIO.CreateDirectory(bugsDirectory);
            if (!File.Exists(Path.Combine(directory, "bugManifest.csv")))
            {
                File.Create(Path.Combine(directory, "bugManifest.csv")).Close();
            }
            if (!File.Exists(Path.Combine(directory, "nextBugIndex.txt")))
            {
                SafeIO.WriteAllText(Path.Combine(directory, "nextBugIndex.txt"), "0");
            }

        }

        public static void DeleteProject(string name)
        {
            string[] existingProjects = ReadNoHeader();
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
            Helpers.DeepDelete(Path.Combine(@".\Database\Projects", project));
        }
        //recursively called

        public static class Bugs
        {
            public class Bug
            {
                public string name;
                public string priority;
                public string timeSpent;
                public string description;
                public string stepsToReproduce;
                public string reference;
            }
            private static int GetNextBugIndex(string project)
            {

                string path = Path.Combine(Config.ROOT_FOLDER, project, "nextBugIndex.txt");
                return int.Parse(SafeIO.ReadAllText(path));
            }
            private static void SetNextBugIndex(string project, int index)
            {
                string path = Path.Combine(Config.ROOT_FOLDER, project, "nextBugIndex.txt");
                SafeIO.WriteAllText(path, (++index).ToString());
            }
            private static void UpdateManifest(string path, int bugIndex)
            {
                string[] currentManifest = SafeIO.ReadAllLines(Path.Combine(path, "bugManifest.csv"));
                currentManifest = currentManifest.Append(bugIndex.ToString()).ToArray();
                SafeIO.WriteAllLines(Path.Combine(path, "bugManifest.csv"), currentManifest);
            }
            public static string[] GetProjectBugManifest(string project)
            {
                string path = Path.Combine(Config.ROOT_FOLDER, project, "bugManifest.csv");
                return SafeIO.ReadAllLines(path);
            }
            public static string[] GetBug(string project, string reference)
            {
                string path = Path.Combine(Config.ROOT_FOLDER, project, "bugs", reference + ".txt");
                return SafeIO.ReadAllLines(path);
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
                SafeIO.WriteAllLines(path, bugText);
            }
            public static void UpdateBug(string project, string reference, Bug bug)
            {
                string[] bugText = new string[0];
                bugText = bugText.Append(bug.name).ToArray();
                bugText = bugText.Append(bug.priority).ToArray();
                bugText = bugText.Append(bug.timeSpent).ToArray();
                bugText = bugText.Append(bug.description).ToArray();
                bugText = bugText.Append(bug.stepsToReproduce).ToArray();
                string path = Path.Combine(Config.ROOT_FOLDER, project, "bugs", reference.ToString() + ".txt");
                File.WriteAllLines(path, bugText);
            }
            public static void DeleteBug(string project, string index)
            {
                string path = Path.Combine(Config.ROOT_FOLDER, project);
                string[] newManifest = new string[0];
                string[] currentManifest = SafeIO.ReadAllLines(Path.Combine(path, "bugManifest.csv"));
                foreach (string reference in currentManifest)
                {
                    if (reference != index)
                    {
                        newManifest = newManifest.Append(reference).ToArray();
                    }
                }
                SafeIO.WriteAllLines(Path.Combine(path, "bugManifest.csv"), newManifest);
                File.Delete(Path.Combine(path, "bugs", index + ".txt"));
            }
            public static int GetBugCount(string projectName)
            {
                string[] manifest = GetProjectBugManifest(projectName);

                return manifest.Length;
            }
            public static List<Bug> GetBugList(string projectName)
            {
                List<Bug> bugs = new List<Bug>();
                string[] manifest = GetProjectBugManifest(projectName);
                foreach (string reference in manifest)
                {
                    string[] bug = GetBug(projectName, reference);
                    Bug compiledBug = new Bug();
                    compiledBug.name = bug[0];
                    compiledBug.priority = bug[1];
                    compiledBug.timeSpent = bug[2];
                    compiledBug.description = bug[3];
                    compiledBug.stepsToReproduce = bug[4];
                    compiledBug.reference = reference;
                    bugs.Add(compiledBug);
                }
                return bugs;
            }
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

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LAB_8_GetToKnowYourClassmates
{
    /* A.TORRES
     * 
     * C#.NET LAB 8: GET TO KNOW YOUR CLASSMATES!
     * 
     * Recognize invalid user inputs when the user requests information about students in a class.
     * 
     * The application provides information about students in a class.
     * The application prompts the user to ask about a particular student.
     * The application gives proper responses according to user-submitted information.
     * The application asks if the user would like to learn about another student.
     * 
     * Build Specifications:
     * Account for invalid user input with exceptions..
     * Try to incorporate IndexOutOfRangeException and FormatException
     * 
     * Extended Exercises:
     * Include more than two pieces of information about the student.
     * Allow the user to search for a student by name as well as number.
     */

    class Program
    {
        static void Main(string[] args)
        {
            // C# .Net cohort data !!!
            string[] names = { "Amanda", "Andy", "Chris", "Deshawn", "James", "Kendall", "Lucas", "Roy", "Sierra", "Tomas" };
            string[] surnames = { "Bledsoe", "Torres", "Constantino", "Dyson", "Watkins", "Allmand", "Carver", "Miller", "Johnson", "Calvo" };
            string[] favoriteFoods = { "pizza", "pizza", "pizza", "pizza", "pizza", "steak", "steak", "steak", "steak", "steak" };
            string[] hometowns = { "Detroit", "Detroit", "Detroit", "Detroit", "Detroit", "Grand Rapids", "Grand Rapids", "Grand Rapids", "Grand Rapids", "Grand Rapids" };
            string[] songs = { "Wow", "Spring", "Gonna Fly Now", "The Way", "Bury It", "Do ya Thing", "Guile's Theme", "What's the Frequency Kenneth", "When You Believe", "I'm Still Here" };
            string[] githubUrls = { "aaa", @"https://github.com/andres-torres-2020", "ccc", "ddd", "", "eee", "fff", "ggg", "hhh", "iii" };

            Console.WriteLine("Welcome to our C# class.\n");
            
            // ask the user to pick a classmate by number
            int studentIndex = GetStudentIndex(names);

            // gather the selected classmate's info
            string name = names[studentIndex];
            string surname = surnames[studentIndex];
            string favefood = favoriteFoods[studentIndex];
            string hometown = hometowns[studentIndex];
            string song = songs[studentIndex];
            string githubUrl = githubUrls[studentIndex];

            bool canContinue = true;
            while (canContinue)
            {
                // let the user pick what info they would like to see
                Console.WriteLine($"Student {studentIndex + 1} is {name + ' ' + surname}.");

                char whichInfo = PromptMoreInfo(name);
                string infoMessage = "";
                switch (whichInfo)
                {
                    case 'G': infoMessage = $"The github URL for {name} is {githubUrl}"; break;
                    case 'H': infoMessage = $"{name} is from {hometown}"; break;
                    case 'F': infoMessage = $"{name}'s favorite food is {favefood}"; break;
                    case 'M': infoMessage = $"{name}'s motivational song is {song}"; break;
                }
                Console.WriteLine(infoMessage);

                canContinue = GetContinueResponse();
            }
            Console.WriteLine("Thanks!");
        }
        public static char PromptMoreInfo(string name)
        {
            while (true)
            {
                Console.WriteLine($"What would you like to know about {name}?\n(enter \"h=hometown\", \"f=favorite food\", \"m=motivational song\", \"g=github URL\"): ");
                try
                {
                    string input = Console.ReadLine().Trim().ToLower();
                    if (input == "g" || input == "github url")
                    {
                        return 'G';
                    }
                    else if (input == "h" || input == "hometown")
                    {
                        return 'H';
                    }
                    else if (input == "f" || input == "favorite food")
                    {
                        return 'F';
                    }
                    else if (input == "m" || input == "motivational song")
                    {
                        return 'M';
                    }
                    throw new Exception("That data does not exist. Please try again.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static int GetStudentIndex(string[] names)
        {
            List<string> dcNames = new List<string>(names);
            Regex regexNum = new Regex(@"^\d{1,5}$");
            foreach (string s in dcNames)
            {
                Console.WriteLine(s);
            }
            int index = -1;
            while (true)
            {
                Console.WriteLine($"Which C# .Net Daytime student would you like to learn more about? (enter Name or Number from 1 to {names.Length}): ");
                try
                {
                    string input = Console.ReadLine().Trim();
                    if (regexNum.IsMatch(input))
                    {
                        // name not found, let's see if user entered an index
                        index = int.Parse(input); // throw FormatException
                        string selectedName = names[index - 1]; // throw IndexOutOfRangeException
                        return index - 1; // user entered a 1-based index
                    }
                    else
                    {
                        index = dcNames.FindIndex(input.Equals);
                        if (index >= 0)
                        {
                            return index; // zero-based index returned from List<>.FindIndex
                        }
                        else
                        {
                            throw new IndexOutOfRangeException(); // name not found
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Please enter a Name or Number from 1 to {names.Length}.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"That student does not exist. Please try again. (enter a number 1-{names.Length})");
                }
            }
        }
        public static bool GetContinueResponse()
        {
            while (true)
            {
                Console.WriteLine("Would you like to know more? (enter “yes” or “no”): ");
                string response = Console.ReadLine().Trim().ToLower();
                if (response == "y" || response == "yes")
                {
                    return true;
                }
                else if (response == "n" || response == "no")
                {
                    return false;
                }
            }
        }
    }
}

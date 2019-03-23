using System;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace lab03_WordGuessGame
{
    public class Program
    {
        
        public static string path = ("../../../savedWords.txt");
        public static bool gameRunning = true;

        public static void Main(string[] args)
        {
            string[] startingWords = { "Biscuit", "Alpha", "Tarantula", "Rockstar", "Cookies", "Doggo" };
            StartSequence(startingWords);
            
            WriteToFileMethod(startingWords);
        }
        public static void StartSequence(string[] array)
        {
            string[] startingWords = array;
                
            do
            {
               
                
                Console.WriteLine("Welcome to the Word Guess Game!");
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit Program");
                int decision = Convert.ToInt32(Console.ReadLine());
                switch (decision)
                {
                    case 1:
                        string[] textFileWords = ReadFile();

                        Console.WriteLine(RandomWordChooser(textFileWords));

                        break;
                    case 2:
                        AdminMenu();
                        break;

                    case 3:
                        gameRunning = false;
                        break;
                }

            }
            while (gameRunning);

        }
        public static void WriteToFileMethod(string[] startingWords)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (string value in startingWords)
                {
                    sw.WriteLine(value);
                }

            }
        }
        public static string[] ReadFile()
        {
            string[] textFileWords;
            using (StreamReader sr = new StreamReader(path))
            {
               textFileWords = File.ReadAllLines(path);

               
            }
            return textFileWords;
            
          
        }
        public static void ReadFileAdminMode()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string[] textFileWords = File.ReadAllLines(path);

                foreach (var value in textFileWords)
                {
                    Console.WriteLine(value);
                }
            }


        }
        public static void UpdateWordBank(string addedWord)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(addedWord);

            }


        }
        public static void GuessCheck(char guessedChar)
        {

        }
        public static string RandomWordChooser(string[] array)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, array.Length);
            string displayWord = array[randomNumber];
            return displayWord;
        }
        public static void DeleteFile()
        {
            File.Delete(path);
        }
        public static string[] RemoveWord(string[] array)
        {
            string input = Console.ReadLine();
            string[] textFileWords = new string[array.Length ];
            bool flag = false;
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i] == input && !flag)
                {
                    flag = true;
                }else if (array[i] != input)
                {
                    textFileWords[i] = array[i];
                }
                else if (flag) 
                {
                    textFileWords[i - 1] = array[i];
                }
               
            }
            return textFileWords;
        }
      
        public static void AdminMenu()
        {
            Console.WriteLine("1. View Current Word Bank");
            Console.WriteLine("2. Add a word to Word Bank");
            Console.WriteLine("3. Remove a word from Word Bank");
            Console.WriteLine("4. Main Menu");
            int decision = Convert.ToInt32(Console.ReadLine());
            
           

                switch (decision)
                {
                    case 1:
                    Console.WriteLine("These are the current words in the Word Bank: ");
                    ReadFileAdminMode();
                        
                        break;

                    case 2:
                    Console.WriteLine("You've selected to ADD a word to the Word Bank");
                    Console.WriteLine("");
                    Console.WriteLine("Please type the word you would like to ADD to the word bank");
                    UpdateWordBank(Console.ReadLine());

                        break;

                    case 3:
                    Console.WriteLine("You've selected to REMOVE a word to the Word Bank");
                    Console.WriteLine("");
                    Console.WriteLine("Please type the word you would like to REMOVE from the word bank");
                    string[] currentArray = ReadFile();
                    string[] textFileWords = RemoveWord(currentArray);
                    DeleteFile();
                    WriteToFileMethod(textFileWords);
                    ReadFile();
                    
                    
                    break;
                    case 4:
                    textFileWords = ReadFile();
                    StartSequence(textFileWords);
                    

                        break;
                }
            
            
        }
        

    }
}

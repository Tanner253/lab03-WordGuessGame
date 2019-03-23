using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace lab03_WordGuessGame
{
    public class Program
    {

        public static string path = ("../../../savedWords.txt");
        

        public static void Main(string[] args)
        {
            string[] startingWords = { "Biscuit", "Alpha", "Bravo", "Tango", "Roger", "unicorn" };
            WriteToFileMethod(startingWords);
            StartSequence(startingWords);

        }
        public static void StartSequence(string[] array)
        {
            string[] startingWords = array;
            bool menuLoading = true;

                Console.WriteLine("Welcome to the Word Guess Game!");
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit Program");
            try
            {
                int decision = Convert.ToInt32(Console.ReadLine());
                if (decision == 1)
                {

                    string[] textFileWords = ReadFile();
                    PlayGame(RandomWordChooser(textFileWords));



                }

                do
                {
                    switch (decision)
                    {

                        case 2:
                            AdminMenu();

                            break;

                        case 3:
                            menuLoading = false;
                            break;
                    }

                }
                while (menuLoading);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void WriteToFileMethod(string[] wordBank)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                bool check = true;
                foreach (string value in wordBank)
                {
                    if (check != String.IsNullOrEmpty(value)) {
                        sw.WriteLine(value);
                    }
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
        public static void PlayGame(string word)
        {
            int correctGuessCounter = 0;
            int incorrectGuess = 0;
            bool isGuessing = true;
            string mysteryWord = word;
            char[] splitWord = mysteryWord.ToCharArray();
            char[] hiddenLetters = HideLetters(splitWord);
            Console.WriteLine("Guess 1 letter at a time to guess the random word!");
            foreach(char value in hiddenLetters)
            {
                Console.Write($"  {value}  ");
            }
            do
            {
                char guess = Convert.ToChar(Console.ReadLine());

                bool wasCorrect = CorrectGuess(guess, splitWord);
                if (wasCorrect == true)
                {
                    //replace blank with letter
                    Console.WriteLine("you guessed the correct " +
                        "letter!");
                    correctGuessCounter++;
                }
                else if (wasCorrect == false)
                {
                    Console.WriteLine("you guessed the wrong letter!");
                    //output message and guess number
                    incorrectGuess++;
                }
            } while (correctGuessCounter <  26);
            



        }
        public static bool CorrectGuess(char letterGuess, char[] word)
        {
            bool wasCorrect = true;
            for (int i = 0; i < word.Length; i++)
            {
                if(word[i] == letterGuess)
                {
                    wasCorrect = true;
                }
                else if (word[i] != letterGuess)
                {
                    wasCorrect = false;
                }
            }
            return wasCorrect;
            
        }
        public static char[] HideLetters(char[] array)
        {
            char[] newArray = array;

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = '_';
            }
            return newArray;
            
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
            string[] textFileWords = new string[array.Length];
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

            try
            {

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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        

    }
}

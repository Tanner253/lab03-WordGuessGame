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
        
        //"Biscuit", "Alpha", "Bravo", "Tango", "Roger", "unicorn" 
        public static void Main(string[] args)
        {
            string[] startingWords = { "Biscuit", "Alpha", "Bravo", "Tango", "Roger", "unicorn" };
            WriteToFileMethod(startingWords);
            StartSequence(startingWords);

        }
        /// <summary>
        /// starts application from scratch
        /// </summary>
        /// <param name="array">word bank</param>
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
        /// <summary>
        /// writes each array index as a line in new txt file
        /// </summary>
        /// <param name="wordBank">word bank</param>
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
        /// <summary>
        /// reads each line of the text file and stores them in array
        /// </summary>
        /// <returns>word bank array</returns>
        public static string[] ReadFile()
        {
            string[] textFileWords;
            using (StreamReader sr = new StreamReader(path))
            {
                textFileWords = File.ReadAllLines(path);


            }
            return textFileWords;


        }
        /// <summary>
        /// reads admin menu and prints all available options (see my word bank)
        /// </summary>
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
        /// <summary>
        /// append a new element to the end of the txt file - adds word to wordbank
        /// </summary>
        /// <param name="addedWord"> new wordbank</param>
        public static void UpdateWordBank(string addedWord)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(addedWord);

            }


        }
        /// <summary>
        /// initializes the game logic
        /// </summary>
        /// <param name="word">a random word from wordbank for game to be played on</param>
        public static void PlayGame(string word)
        {
            int correctGuessCounter = 0;
            int incorrectGuess = 0;
           
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
        /// <summary>
        /// verifys that the input CHAR is present in the word the user is trying to guess
        /// </summary>
        /// <param name="letterGuess">char</param>
        /// <param name="word">randomly displayed word</param>
        /// <returns>true or false</returns>
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
        /// <summary>
        /// replaces letters in a char array with hidden  values
        /// </summary>
        /// <param name="array">word bank</param>
        /// <returns>hidden array</returns>
        public static char[] HideLetters(char[] array)
        {
            char[] newArray = array;

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = '_';
            }
            return newArray;
            
        }
        /// <summary>
        /// handles choosing a random number == random word from word bank
        /// </summary>
        /// <param name="array">word bank</param>
        /// <returns>1 word</returns>
        public static string RandomWordChooser(string[] array)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, array.Length);
            string displayWord = array[randomNumber];
            return displayWord;
        }
        /// <summary>
        /// deletes txt file
        /// </summary>
        public static void DeleteFile()
        {
            File.Delete(path);
        }
        /// <summary>
        /// finds and removes word from wordbank
        /// </summary>
        /// <param name="array">word bank</param>
        /// <returns>new array with removed element</returns>
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
        /// <summary>
        /// handles logic for admin menu
        /// </summary>
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

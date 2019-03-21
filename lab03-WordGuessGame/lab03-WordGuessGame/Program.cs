using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace lab03_WordGuessGame
{
    public class Program
    {
        public static string path = ("../../../savedWords.txt");
        public static bool gameRunning = true;

        public static void Main(string[] args)
        {
            StartSequence();
        }
        public static void StartSequence()
        {
            do
            {
                string addedWord;
                string[] startingWords = { "Biscuit", "Alpha", "Tarantula", "Rockstar" };
                Console.WriteLine("Welcome to the Word Guess Game!");
                WriteToFileMethod(startingWords);
                Console.WriteLine("1. Play game");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit Program");
                int decision = Convert.ToInt32(Console.ReadLine());
                switch (decision)
                {
                    case 1:


                        break;
                }

                Console.WriteLine("add a word");
                addedWord = Console.ReadLine();
                UpdateWordBank(addedWord);
                ReadFile();
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
        public static void ReadFile()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string[] textFileWords = File.ReadAllLines(path);
                for (int i = 0; i < textFileWords.Length; i++)
                {
                    Console.WriteLine(textFileWords[i]);
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
        public static string RandomWordChooser()
        {

        }

    }
}

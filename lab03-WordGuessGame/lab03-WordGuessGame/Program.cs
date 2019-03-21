using System;
using System.IO;

namespace lab03_WordGuessGame
{
    public class Program
    {
        public static string path = ("../../../savedWords.txt");
        public static void Main(string[] args)
        {

            string[] startingWords = { "Biscuit", "Alpha", "Tarantula", "Rockstar"};
            Console.WriteLine("Welcome to the Word Guess Game!");
            WriteToFileMethod(startingWords);
        }
        public static void WriteToFileMethod(string[] startingWords)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach  (string value in startingWords)
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
                    Console.WriteLine($"These are your options: {textFileWords[i]}");
                }
            }
        }
    }
}

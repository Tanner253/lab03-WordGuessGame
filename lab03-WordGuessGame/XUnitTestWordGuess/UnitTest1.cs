using System;
using Xunit;
using static lab03_WordGuessGame.Program;

namespace XUnitTestWordGuess
{
    public class UnitTest1
    {
        [Fact]
        public void CanUpdateFile()
        {
            string wordBank = "word bank";
            ReadFile();
            UpdateWordBank(wordBank);
            string[] updatedWordBank = ReadFile();

            Assert.Equal("word bank", updatedWordBank[updatedWordBank.Length-1]);

        }
        [Fact]
        public void CanReadFromFile()
        {
            string[] array = { "Biscuit", "Alpha", "Bravo", "Tango", "Roger", "unicorn" };
            WriteToFileMethod(array);
            string[] newArray = ReadFile();
            Assert.Equal(6, newArray.Length);

        }
        //functionality for char check against word is not working right 
    }
}
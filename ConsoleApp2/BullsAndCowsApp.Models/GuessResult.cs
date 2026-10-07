using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.BullsAndCowsApp.Models
{
    public class GuessResult
    {
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public bool IsWinningGuess => Bulls == 4;
    }
}

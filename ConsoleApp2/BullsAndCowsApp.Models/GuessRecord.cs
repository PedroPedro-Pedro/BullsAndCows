using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.BullsAndCowsApp.Models
{
    public class GuessRecord
    {
        public int AttemptNumber { get; set; }
        public string Guess { get; set; } = string.Empty;
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public string Feedback => $"{Bulls} Bulls, {Cows} Cows";
    }
}

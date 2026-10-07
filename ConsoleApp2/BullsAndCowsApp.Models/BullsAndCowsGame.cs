using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace ConsoleApp2.BullsAndCowsApp.Models
{
    public class BullsAndCowsGame
    {
        public const int CodeLength = 4;
        private readonly Random _random = new();
        private string _secretNumber = string.Empty;

        public int AttemptCount { get; private set; }
        public bool IsGameOver { get; private set; }

        public BullsAndCowsGame()
        {
            StartNewGame();
        }


        public void StartNewGame()
        {
            AttemptCount = 0;
            IsGameOver = false;
            _secretNumber = GenerateSecretNumber();
        }


        private string GenerateSecretNumber()
        {
            List<char> digits = new() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string secret;

            do
            {
                for (int i = digits.Count - 1; i > 0; i--)
                {
                    int j = _random.Next(i + 1);
                    (digits[i], digits[j]) = (digits[j], digits[i]);
                }

                secret = new string(digits.Take(CodeLength).ToArray());
            }
            while (secret[0] == '0');

            return secret;
        }


        public static bool IsValidGuess(string guess, out string errorMessage)
        {

            if (string.IsNullOrEmpty(guess) || guess.Length != CodeLength)
            {
                errorMessage = "Please enter exactly 4 digits.";
                return false;
            }


            if (!guess.All(char.IsDigit))
            {
                errorMessage = "Please enter exactly 4 digits.";
                return false;
            }


            if (guess.Distinct().Count() != CodeLength)
            {
                errorMessage = "All digits must be different from each other.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
        public GuessResult SubmitGuess(string guess)
        {
            if (IsGameOver)
            {
                Console.WriteLine("Cannot submit a guess when the game is already over.");
            }

            AttemptCount++;

            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < CodeLength; i++)
            {
                if (guess[i] == _secretNumber[i])
                {
                    bulls++;
                }
                else if (_secretNumber.Contains(guess[i]))
                {
                    cows++;
                }
            }

            if (bulls == CodeLength)
            {
                IsGameOver = true;
            }

            return new GuessResult
            {
                Bulls = bulls,
                Cows = cows
            };
        }
    }
    }

using System.Runtime.Versioning;

namespace ConsoleApp2.BullsAndCowsApp.Models
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BullsAndCowsGame game = new();
            List<GuessRecord> history = new();

            PrintWelcome();

            while (true)
            {
                Console.Write("\nYour guess (4 digits), 'n'=new game, 'h'=history, 'q'=quit: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                string command = input.ToLower();

                if (IsQuit(command))
                {
                    Console.WriteLine("Thanks for playing. Goodbye!");
                    break;
                }

                if (IsNewGame(command))
                {
                    game.StartNewGame();
                    history.Clear();
                    Console.WriteLine("\n--- New Game Started! I've picked a new secret number. ---");
                    continue;
                }

                if (IsHistory(command))
                {
                    PrintHistory(history);
                    continue;
                }

                if (game.IsGameOver)
                {
                    Console.WriteLine("The game is over. Type 'n' for a new game or 'q' to quit.");
                    continue;
                }

                if (!BullsAndCowsGame.IsValidGuess(input, out string errorMessage))
                {
                    Console.WriteLine($"Error: {errorMessage}");
                    continue;
                }

                GuessResult result = game.SubmitGuess(input);

                history.Add(new GuessRecord
                {
                    AttemptNumber = game.AttemptCount,
                    Guess = input,
                    Bulls = result.Bulls,
                    Cows = result.Cows
                });

                Console.WriteLine($"-> {result.Bulls} Bulls, {result.Cows} Cows");

                if (result.IsWinningGuess)
                {
                    string attemptsText = game.AttemptCount == 1 ? "1 attempt" : $"{game.AttemptCount} attempts";
                    Console.WriteLine($"\n*** Correct! You guessed it in {attemptsText}. ***");
                    Console.WriteLine("Type 'n' for a new game or 'q' to quit.");
                }
            }
        }

        private static bool IsQuit(string input)
        {
            return input == "q" || input == "quit" || input == "exit";
        }

        private static bool IsNewGame(string input)
        {
            return input == "n" || input == "new";
        }

        private static bool IsHistory(string input)
        {
            return input == "h" || input == "history";
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("        BULLS AND COWS (CLI)           ");
            Console.WriteLine("=======================================");
            Console.WriteLine("I picked a 4-digit number with no repeated digits.");
            Console.WriteLine("Bulls = right digit, right position.");
            Console.WriteLine("Cows = right digit, wrong position.");
        }

        private static void PrintHistory(List<GuessRecord> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No guesses made yet in this game.");
                return;
            }

            Console.WriteLine("\n--- Guess History ---");
            foreach (var record in history)
            {
                Console.WriteLine($"Attempt #{record.AttemptNumber}: {record.Guess} -> {record.Feedback}");
            }
            Console.WriteLine("---------------------");
        }
    }
}
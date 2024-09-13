using game.Services;
using game.Utilities;
using System;
using System.Security.Cryptography;

namespace game
{
    internal class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "game")
            {
                args = args.Skip(1).ToArray();
            }

            if (args.Length < 3 || args.Length % 2 == 0)
            {
                Console.WriteLine("Error: You must provide an odd number of moves (at least 3). Example: rock paper scissors");
                return;
            }

            string[] moves = args;
            var key = HMACHelper.GenerateKey();
            string keyHex = BitConverter.ToString(key).Replace("-", "").ToLower();
            int computerMove = new Random().Next(0, moves.Length);
            var hmac = HMACHelper.ComputeHmac(moves[computerMove], key);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"HMAC: {hmac}");
                Console.WriteLine();
                Console.WriteLine("Available moves:");
                Console.WriteLine();

                for (int i = 0; i < moves.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {moves[i]}");
                }
                Console.WriteLine();
                Console.WriteLine("0 - Exit");
                Console.WriteLine("? - Help");
                Console.WriteLine();

                Console.Write("Enter your move: ");
                string userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput == "?")
                {
                    Table.ShowHelp(moves);
                    continue;
                }

                if (userInput == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                if (!int.TryParse(userInput, out int userMove) || userMove < 1 || userMove > moves.Length)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                userMove--;

                Console.WriteLine($"Your move: {moves[userMove]}");
                Console.WriteLine($"Computer move: {moves[computerMove]}");

                Console.WriteLine();

                if (userMove == computerMove)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("It's a tie!");
                }
                else
                {
                    int half = moves.Length / 2;
                    if ((userMove > computerMove && userMove - computerMove <= half) ||
                        (computerMove > userMove && computerMove - userMove > half))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You win!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You lose!");
                    }
                }

                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine($"HMAC key: {keyHex}");
                Console.WriteLine($"To ensure the integrity of the game you can check the following page putting in the input field '{moves[computerMove]}': " +
                    $"https://gchq.github.io/CyberChef/#recipe=HMAC(%7B'option':'Hex','string':'{keyHex}'%7D,'SHA256')");

                computerMove = new Random().Next(0, moves.Length);
                hmac = HMACHelper.ComputeHmac(moves[computerMove], key);
            }
        }
    }
}

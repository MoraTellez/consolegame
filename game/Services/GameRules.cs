using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.Services
{
    internal class GameRules
    {
        public static void GetWinner(string[] moves, string userMove, string computerMove)
        {
            int userIndex = Array.IndexOf(moves, userMove);
            int computerIndex = Array.IndexOf(moves, computerMove);

            int half = moves.Length / 2;
            if (userIndex == computerIndex)
            {
                Console.WriteLine("Draw.");
            }
            else if((computerIndex > userIndex && computerIndex - userIndex <= half) || (userIndex > computerIndex && userIndex - computerIndex > half))
        {
                Console.WriteLine("Computer wins.");
            }
            else
            {
                Console.WriteLine("You win.");
            }

        }
    }
}

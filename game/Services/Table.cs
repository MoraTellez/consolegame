using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace game.Services
{
    internal class Table
    {
        public static void ShowHelp(string[] moves)
        {
            // Crear una tabla con las jugadas
            var table = new ConsoleTable(new[] { "" }.Concat(moves).ToArray());

            // Añadir filas con el resultado de cada jugada
            for (int i = 0; i < moves.Length; i++)
            {
                var row = new List<string> { moves[i] };  // Comienza la fila con el movimiento actual
                for (int j = 0; j < moves.Length; j++)
                {
                    if (i == j)
                        row.Add("Tie");  // Si es la misma jugada, es empate
                    else
                    {
                        int half = moves.Length / 2;  // Determina la mitad del total de movimientos
                        if ((j > i && j - i <= half) || (i > j && i - j > half))
                            row.Add("Win");  // Si está dentro del rango de la mitad siguiente, es victoria
                        else
                            row.Add("Lose");  // Si está en la mitad anterior, es derrota
                    }
                }
                table.AddRow(row.ToArray());
            }

            // Imprimir la tabla en la consola
            table.Write();
        }   
    }
}

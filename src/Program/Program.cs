using System;
using System.Text;
using System.Threading;
using PII_Game_Of_Life;

public class Program
{

    static void Main(string[] args)
    {
        int Ancho = 10;
        int Altura = 10;

        Tablero tablero = new Tablero(Ancho, Altura);
        bool[,] b = tablero.GenerarTablero();

        while (true)
        {
            Console.Clear();
            StringBuilder s = new StringBuilder();
            for (int y = 0; y < Altura; y++)
            {
                for (int x = 0; x < Ancho; x++)
                {
                    if (b[x, y])
                    {
                        s.Append("|X|");
                    }
                    else
                    {
                        s.Append("___");
                    }
                }
                s.Append("\n");
            }
            Console.WriteLine(s.ToString());
            Thread.Sleep(300);
        }
    }
}

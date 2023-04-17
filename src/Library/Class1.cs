using System;
using System.IO;

namespace PII_Game_Of_Life
{
/*La clase Tablero no cumple con el principio SRP ya que tiene dos 
responsabilidades las cuales son, generar un tablero y leer un archivo.
Ademas tambien se encarga de aplicar la logica del juego, que se podria 
considerar como una tercera responsabilidad. Para que la clase cumpla con
el principio SRP hay que separar las responsabilidades de leer y escribir
archivos en una clase separada, y la responsabilidad de aplicar la logica 
en otra clase, de esa manera quedaria la clase tablero con solo una 
responsabilidad.

La responsabilidad principal de la clase Tablero es generar un tablero 
como tambien tiene la responsabilidad de aplicar las reglas del juego. Por 
lo tanto, no cumple totalmente con el patrón Expert. Para que se cumpla
totalmente con el patrón Expert, deberia de tener la informacion necesaria 
para llevar a cabo sus responsabilidades incluyendo las reglas del juego.
*/
    public class Tablero
    {
        private string rutaArchivo;
        
        public int Ancho { get; set; }
        public int Altura { get; set;}

        public Tablero(int ancho, int altura)
        {
            Ancho = ancho;
            Altura = altura;
        }
        
        public Tablero(int ancho, int altura, string rutaArchivo)
        {
            Ancho = ancho;
            Altura = altura;
            this.rutaArchivo = rutaArchivo;
        }
        
        public bool[,] GenerarTablero()
        {
            bool[,] board;

            if (rutaArchivo == null)
            {
                board = new bool[Ancho, Altura];

                // inicializar células con un valor random
                Random random = new Random();
                for (int x = 0; x < Ancho; x++)
                {
                    for (int y = 0; y < Altura; y++)
                    {
                        board[x, y] = random.Next(2) == 0;
                    }
                }
            }
            // Si la ruta no es null, se lee el archivo
            else
            {
                MiArchivo miArchivo = new MiArchivo();
                board = miArchivo.LeerArchivo(rutaArchivo);
            }

            // aplicar LogicaJuego 
            LogicaJuego logicaJuego = new LogicaJuego();
            logicaJuego.AplicarReglas(board, Ancho, Altura);

            return board;
        }
    }

/*La clase MiArchivo cumple con el principio SRP ya que tiene una unica 
responsabilidad que es leer un archivo y devolver un array con los valores
booleanos, dependiendo el contenido del archivo.

La clase MiArchivo también cumple con el patrón Expert, ya que tiene la
información necesaria para leer un archivo y devolver un array de valores
bool a partir del contenido del archivo. 
*/
    public class MiArchivo
    {
        public bool[,] LeerArchivo(string url)
        {
            string content = File.ReadAllText(url);
            string[] contentLines = content.Split('\n');
            bool[,] board = new bool[contentLines.Length, contentLines[0].Length];
            for (int y = 0; y < contentLines.Length; y++)
            {
                for (int x = 0; x < contentLines[y].Length; x++)
                {
                    if (contentLines[y][x] == '1')
                    {
                        board[y, x] = true;
                    }
                }
            }
            return board;
        }
    }
/*La clase LogicaJuego cumple con el principio SRP, ya que tiene solo una
unica responsablidad, la cual es aplicar las reglas del juego en un tablero
y ademas actualizar el estado de las células en el tablero.

Ademas, la clase LogicaJuego cumple con el patron Expert, porque es la clase }
mas adecuada para realizar lo que hace. Es responsable de actualizar el 
estado de la celula en el tablero a partir de las reglas del juego, lo cual 
significa que tiene el conocimiento necesario para real8izar lo que hace.
 */
    public class LogicaJuego
    {
        // Método para aplicar las reglas del juego en el tablero
        public void AplicarReglas(bool[,] gameBoard, int ancho, int altura)
        {
            // Recorremos todas las células del tablero
            for (int x = 0; x < ancho; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    // Contamos el número de vecinos vivos
                    int aliveNeighbors = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (i >= 0 && i < ancho && j >= 0 && j < altura && gameBoard[i, j] && !(i == x && j == y))
                            {
                                aliveNeighbors++;
                            }
                        }
                    }

                    
                    if (gameBoard[x, y] && aliveNeighbors < 2) // determinar si una célula viva muere por soledad
                    {
                        gameBoard[x, y] = false; //célula muere
                    }
                    else if (gameBoard[x, y] && aliveNeighbors > 3) // determinar si una célula viva muere por sobrepoblación
                    {
                        gameBoard[x, y] = false; // La célula muere
                    }
                    else if (!gameBoard[x, y] && aliveNeighbors == 3) // determinar si una célula muerta revive por reproducción
                    {
                        gameBoard[x, y] = true; // La célula revive
                    }
                }
            }
        }
    }
}

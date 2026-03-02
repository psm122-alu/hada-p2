using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Game
    {
        private bool finPartida;

        public Game() 
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            List<Barco> barcos = new List<Barco>
            {
                new Barco("THOR", 1, 'h', new Coordenada(0,0)),
                new Barco("LOKI", 2, 'v', new Coordenada(1,2)),
                new Barco("MAYA", 3, 'h', new Coordenada(3,1))
            };

            Tablero tablero = new Tablero(4, barcos);

            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {
                Console.WriteLine(tablero.ToString());
                Console.WriteLine("Introduce la coordenada a disparar (formato: FILA,COLUMNA) ('s' o 'S' para salir):");

                string entrada = Console.ReadLine();

                if (entrada.ToLower() == "s")
                {
                    finPartida = true;
                    break;
                }

                string[] partes = entrada.Split(',');

                if (partes.Length == 2)
                {
                    try
                    {
                        Coordenada coordenadaDisparo = new Coordenada(partes[0], partes[1]);
                        tablero.Disparar(coordenadaDisparo);
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Formato introducido incorrecto (formato: FILA,COLUMNA)");
                    }
                }
                else
                {
                    Console.WriteLine("Formato introducido incorrecto (formato: FILA,COLUMNA)");
                }
            }
        }
        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}

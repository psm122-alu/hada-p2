using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        private int _tamTablero;
        public int TamTablero
        {
            get { return _tamTablero; }
            set
            {
                if (value < 4)
                {
                    _tamTablero = 4;
                }
                else if (value > 9)
                {
                    _tamTablero = 9;
                }
                else _tamTablero = value;
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            TamTablero = tamTablero;
            this.barcos = barcos;

            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            for(int i = 0; i < barcos.Count; i++)
            {
                barcos[i].eventoTocado += cuandoEventoTocado;
                barcos[i].eventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();

        }

        private void inicializaCasillasTablero()
        {
            for(int i = 0; i < TamTablero; i++)
            {
                for(int j = 0; j < TamTablero; j++)
                {
                    Coordenada coordenada_actual = new Coordenada(i, j);
                    string estadoCasilla = "AGUA";

                    for(int b = 0; b < barcos.Count; b++)
                    {
                        if (barcos[b].CoordenadasBarco.ContainsKey(coordenada_actual))
                        {
                            estadoCasilla = barcos[b].Nombre;
                            break;
                        }
                    }
                    casillasTablero.Add(coordenada_actual, estadoCasilla);
                }
            }

        }

        public void Disparar(Coordenada c)
        {
            if(c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine("La coordenada " + c.ToString() + " está fuera de las dimensiones del tablero.");
                return;
            }

            coordenadasDisparadas.Add(c);

            for(int i = 0; i < barcos.Count; i++)
            {
                barcos[i].Disparo(c);
            }
        }

        public string DibujarTablero()
        {
            string imprimirTablero = "CASILLAS TABLERO\n";
            imprimirTablero += "=======\n";

            for(int i = 0; i < TamTablero; i++)
            {
                for(int j = 0; j < TamTablero; j++)
                {
                    Coordenada coordenada_imprimir = new Coordenada(i, j);

                    imprimirTablero += "[" + casillasTablero[coordenada_imprimir] + "]";
                }

                imprimirTablero += "\n";
            }

            return imprimirTablero;
        }

        public override string ToString()
        {
            string imprimir = "";

            for(int i = 0; i < barcos.Count; i++)
            {
                imprimir += barcos[i].ToString() + "\n";
            }

            imprimir += "\n Coordenadas disparadas: ";
            imprimir += string.Join(" ", coordenadasDisparadas) + "\n";

            imprimir += " Coordenadas tocadas: ";
            imprimir += string.Join(" ", coordenadasTocadas) + "\n\n";

            imprimir += DibujarTablero();

            return imprimir;
        }

        public event EventHandler<EventArgs> eventoFinPartida;

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            if (casillasTablero.ContainsKey(e.CoordenadaImpacto))
            {
                casillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";
            }

            if (!coordenadasTocadas.Contains(e.CoordenadaImpacto))
            {
                coordenadasTocadas.Add(e.CoordenadaImpacto);
            }

            Console.WriteLine("TABLERO: Barco ["+ e.Nombre +"] tocado en Coordenada: ["+ e.CoordenadaImpacto.ToString() +"]");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine("TABLERO: Barco [" + e.Nombre + "] hundido!!");

            Barco barcoHundido = null;

            for(int i = 0; i < barcos.Count; i++)
            {
                if (barcos[i].Nombre == e.Nombre)
                {
                    barcoHundido = barcos[i];
         
                }
            }

            if(barcoHundido != null && !barcosEliminados.Contains(barcoHundido))
            {
                barcosEliminados.Add(barcoHundido);
            }

            if(barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this,EventArgs.Empty);
            }
        }



    }
}

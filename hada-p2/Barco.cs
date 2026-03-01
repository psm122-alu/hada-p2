using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }

        public string Nombre {  get; set; }
        public int NumDanyos {  get; set; }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.Nombre = nombre;
            this.NumDanyos = 0;
            this.CoordenadasBarco = new Dictionary<Coordenada, string>();

            for (int i = 0; i < longitud; i++)
            {
                Coordenada c;

                if (orientacion == 'h')
                {
                    c = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                else
                {
                    c = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);

                }

                this.CoordenadasBarco.Add(c, nombre);

            }

        }

        public void Disparo(Coordenada c)
        {
            if (this.CoordenadasBarco.ContainsKey(c))
            {
                if (this.CoordenadasBarco[c] == this.Nombre)
                {
                    this.CoordenadasBarco[c] = this.Nombre + "_T";

                    this.NumDanyos++;

                    eventoTocado?.Invoke(this, new TocadoArgs(this.Nombre, c));

                    if (hundido())
                    {
                        eventoHundido?.Invoke(this, new HundidoArgs(this.Nombre));
                    }

                }
            }
        }


        public bool hundido()
        {
            foreach(var etiqueta in CoordenadasBarco.Values)
            {
                if(etiqueta == this.Nombre)
                {
                    return false;
                }
            }

            return true; 
        }


        public override string ToString()
        {
            string resultado = $"[{this.Nombre}] - DAÑOS: [{this.NumDanyos}] - [{this.hundido()}] - COORDENADAS: ";

            List<string> listaCoordenadas = new List<string>();
            
            foreach(var par in CoordenadasBarco)
            {
                listaCoordenadas.Add($"[{par.Key} :{par.Value}]");
            }

            resultado += string.Join(" ", listaCoordenadas);

            return resultado;
        }

    }
}

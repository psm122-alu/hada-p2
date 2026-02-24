using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int fila;
        private int columna;
        public int Fila
        {
            get { return fila; }
            set { if (value >= 0 && value <= 9){ fila = value;} }

        }

        public int Columna
        {
            get { return columna; }
            set { if (value >= 0 && value <= 9) { columna = value; } }
        }

        public Coordenada() 
        { 
            this.Fila = 0;
            this.Columna = 0;
        }

        public Coordenada(int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;
        }

        public Coordenada(string fila, string columna)
        {
            this.Fila = int.Parse(fila);
            this.Columna = int.Parse(columna);
        }

        public Coordenada( Coordenada otro)
        {
            this.fila = otro.Fila;
            this.columna = otro.Columna;
        }

        public override string ToString()
        {
            return "(" + Fila + "," + Columna + ")";
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public override bool Equals(object obj) 
        { 
            if(obj == null || !(obj is Coordenada)) return false;
            return Equals((Coordenada)obj);
            
        }
        public bool Equals(Coordenada otra)
        {
            if(otra == null) return false;
            return this.Fila == otra.Fila && this.Columna == otra.Columna;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs : EventArgs
    {
        public string Nombre { get; private set; }
        public Coordenada CoordenadaImpacto { get; private set; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            this.Nombre = nombre;
            this.CoordenadaImpacto = coordenadaImpacto;
        }

    }

    public class HundidoArgs : EventArgs { 
        public string Nombre { get; private set; }

        public HundidoArgs(string nombre)
        {
            this.Nombre = nombre;
        }


    }
}

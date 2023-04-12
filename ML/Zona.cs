using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Zona
    {
        public int IdZona { get; set; }
        public string Descripcion { get; set;}
        public decimal ZCentro { get; set; }
        public decimal ZNorte { get; set; }
        public decimal ZPoniente { get; set; }
        public decimal ZSur { get; set; }
        public decimal ZOriente { get; set; }
        public decimal PCentro { get; set; }
        public decimal PNorte { get; set; }
        public decimal PPoniente { get; set; }
        public decimal PSur { get; set; }
        public decimal POriente { get; set; }
        public decimal VentaTotal { get; set; }
        public List<object> Zonas { get; set; }

        public ML.Cine Cine { get; set; }
    }
}

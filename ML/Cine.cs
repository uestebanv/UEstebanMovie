namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Direccion { get; set;}
        public int Venta { get; set; }
        public List<object> Cines { get; set; }
        public ML.Zona Zona { get; set;}
    
    }
}
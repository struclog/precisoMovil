using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisoGps.Responses
{
    public class ResposeHoja
    {
        public int id_hoja { get; set; }
        public string fecha { get; set; }
        public string tipo_dia { get; set; }
        public int? id_conductor { get; set; }
        public int? id_ayudante { get; set; }
        public int? id_ruta { get; set; }
        public int id_unidad { get; set; }
        public string ayudante_nombre { get; set; }
        public Unidad unidad { get; set; }
        public Ruta ruta { get; set; }
        public Conductor conductor { get; set; }
        public List<Gasto> gastos { get; set; }
        public List<Produccione> producciones { get; set; }
    }
}

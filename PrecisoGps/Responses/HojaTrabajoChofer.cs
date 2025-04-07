using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisoGps.Responses
{
    public class HojaTrabajoChofer
    {
        public int id_hoja { get; set; }
        public string fecha { get; set; }
        public string tipo_dia { get; set; }
        public int id_unidad { get; set; }
        public int? id_conductor { get; set; }
        public int? id_ruta { get; set; }
        public string ayudante_nombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisoGps.Responses
{
    public class ResponseHojaConductor
    {
        public int id_hoja { get; set; }
        public string fecha { get; set; }
        public string tipo_dia { get; set; }
        public int? id_conductor { get; set; }
        public object id_ayudante { get; set; }
        public int? id_ruta { get; set; }
        public int id_unidad { get; set; }
        public string ayudante_nombre { get; set; }
        public List<Produccione> producciones { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PrecisoGps.Responses
{
    public class Unidad
    {
        public int id_unidad { get; set; }
        public string numero_habilitacion { get; set; }
        public string placa { get; set; }
        public string propietario { get; set; }
        public int? anio_fabricacion { get; set; }
        public string chasis { get; set; }
        public object carroceria { get; set; }
        public object tipo_especial { get; set; }
        public object capacidad_pasajeros { get; set; }
        public object puertas_ingreso { get; set; }
        public object puertas_izquierdas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PrecisoGps.Models
{
    public class SimCard : INotifyPropertyChanged
    {

        private string _estado;

        public int ID_SIM { get; set; }
        public string RUC { get; set; }
        public string PROPIETARIO { get; set; }
        public string CUENTA { get; set; }
        public string NUMEROTELEFONO { get; set; }
        public string TIPOPLAN { get; set; }
        public string PLAN { get; set; }
        public string ICC { get; set; }
        public string ESTADO {
            get => _estado;
            set
            {
                _estado = value;
                OnPropertyChanged();
            }
        }
        public string GRUPO { get; set; }
        public string ASIGNACION { get; set; }
        public string EQUIPO { get; set; }
        public string VEH_ID { get; set; }
        public string IMEI { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}

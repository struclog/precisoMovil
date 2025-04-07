using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using Xamarin.Forms;

namespace PrecisoGps.Responses
{
    public class Gasto : INotifyPropertyChanged
    {
        public int id_gasto { get; set; }
        public int id_hoja { get; set; }
        public string tipo_gasto { get; set; }
        public string valor { get; set; }
        public string imagen { get; set; }

        [JsonIgnore]
        public bool Editable { get; set; } = true;

        [JsonIgnore]
        public bool MostrarImagen => tipo_gasto == "DIESEL" || tipo_gasto == "OTROS";

        private ImageSource _imagenPreview;

        [JsonIgnore]
        public ImageSource ImagenPreview
        {
            get => _imagenPreview;
            set
            {
                _imagenPreview = value;
                OnPropertyChanged(); // Notifica al binding que cambió
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}

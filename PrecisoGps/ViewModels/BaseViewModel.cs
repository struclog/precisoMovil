using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrecisoGps.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Actualiza el valor de una propiedad y notifica el cambio.
        /// </summary>
        /// <typeparam name="T">El tipo de la propiedad.</typeparam>
        /// <param name="backingStore">El campo que almacena el valor de la propiedad.</param>
        /// <param name="value">El nuevo valor para la propiedad.</param>
        /// <param name="propertyName">El nombre de la propiedad.</param>
        /// <param name="onChanged">Una acción opcional que se ejecuta después de que el valor ha cambiado.</param>
        /// <returns>True si el valor fue cambiado, false si ya era igual.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

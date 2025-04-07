using PrecisoGps.Models;
using PrecisoGps.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{
    public class EditSimCardViewModel : BaseViewModel
    {
        private SimCard _selectedSimCard;
        public SimCard SelectedSimCard
        {
            get => _selectedSimCard;
            set
            {
                _selectedSimCard = value;
                OnPropertyChanged();
                UpdateFieldStates();
            }
        }

        // Propiedades para controlar habilitación de campos
        public bool IsICCEnabled => SelectedSimCard?.ESTADO != "ELIMINADA";
        public bool IsGrupoEnabled => SelectedSimCard?.ESTADO == "ACTIVA";
        public bool IsAsignacionEnabled => SelectedSimCard?.ESTADO == "ACTIVA";
        public bool IsImeiEnabled => SelectedSimCard?.ESTADO == "ACTIVA";
        public bool IsEquipoEnabled => SelectedSimCard?.ESTADO == "ACTIVA";

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public EditSimCardViewModel(SimCard simCard)
        {
            SelectedSimCard = simCard;

            SaveCommand = new Command(async () => await SaveChanges());
            CancelCommand = new Command(async () => await CancelEdit());

            SelectedSimCard.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedSimCard.ESTADO))
                {
                    UpdateFieldStates();
                }
            };
        }

        private void UpdateFieldStates()
        {
            OnPropertyChanged(nameof(IsICCEnabled));
            OnPropertyChanged(nameof(IsGrupoEnabled));
            OnPropertyChanged(nameof(IsAsignacionEnabled));
            OnPropertyChanged(nameof(IsImeiEnabled));
            OnPropertyChanged(nameof(IsEquipoEnabled));
        }

        private async Task SaveChanges()
        {
            if (SelectedSimCard.ESTADO == "LIBRE")
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "En el estado LIBRE se eliminarán los campos: Grupo, Asignación, Equipo, IMEI. ¿Desea continuar?",
                    "Sí",
                    "No"
                );

                if (!confirm) return;

                SelectedSimCard.GRUPO = null;
                SelectedSimCard.ASIGNACION = null;
                SelectedSimCard.EQUIPO = null;
                SelectedSimCard.IMEI = null;
            }
            else if (SelectedSimCard.ESTADO == "ELIMINADA")
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Advertencia",
                    "En el estado ELIMINADA se eliminarán los campos: ICC, Grupo, Asignación, Equipo, IMEI. ¿Desea continuar?",
                    "Sí",
                    "No"
                );

                if (!confirm) return;

                SelectedSimCard.ICC = null;
                SelectedSimCard.GRUPO = null;
                SelectedSimCard.ASIGNACION = null;
                SelectedSimCard.EQUIPO = null;
                SelectedSimCard.IMEI = null;
            }

            if (SelectedSimCard.ESTADO == "ACTIVA" && AreRequiredFieldsEmpty())
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Todos los campos deben estar completos para el estado ACTIVA.",
                    "OK"
                );
                return;
            }

            var simCardService = new SimCardService();
            var response = await simCardService.UpdateSimCardAsync(SelectedSimCard);

            if (response)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "SIM Card actualizada correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar la SIM Card", "OK");
            }
        }

        private bool AreRequiredFieldsEmpty()
        {
            return string.IsNullOrEmpty(SelectedSimCard.CUENTA) ||
                   string.IsNullOrEmpty(SelectedSimCard.NUMEROTELEFONO) ||
                   string.IsNullOrEmpty(SelectedSimCard.TIPOPLAN) ||
                   string.IsNullOrEmpty(SelectedSimCard.PLAN) ||
                   string.IsNullOrEmpty(SelectedSimCard.GRUPO) ||
                   string.IsNullOrEmpty(SelectedSimCard.ASIGNACION) ||
                   string.IsNullOrEmpty(SelectedSimCard.IMEI) ||
                   string.IsNullOrEmpty(SelectedSimCard.ICC);
        }

        private async Task CancelEdit()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

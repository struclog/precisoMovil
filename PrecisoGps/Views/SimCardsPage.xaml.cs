using PrecisoGps.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrecisoGps.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimCardsPage : ContentPage
    {
        private bool _isFirstAppearance = true;

        public SimCardsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!_isFirstAppearance)
            {
                // Llamar al comando de búsqueda automáticamente después de regresar
                var viewModel = BindingContext as SimCardViewModel;
                if (viewModel != null && !string.IsNullOrEmpty(viewModel.SearchId))
                {
                    await viewModel.FetchSimCards(); // Método que realiza la búsqueda
                }
            }

            _isFirstAppearance = false;
        }
    }
}
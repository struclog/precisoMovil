using PrecisoGps.Models;
using PrecisoGps.Services;
using PrecisoGps.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrecisoGps.ViewModels
{

    public class SimCardViewModel : BaseViewModel
    {
        private readonly SimCardService _simCardService;

        public ObservableCollection<SimCard> SimCards { get; set; }
        public string SearchId { get; set; }
        public ICommand SearchCommand { get; }
        public ICommand EditCommand { get; }

        private bool _isBusy;
        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    set
        //    {
        //        _isBusy = value;
        //        OnPropertyChanged();
        //    }
        //}

        public SimCardViewModel()
        {
            _simCardService = new SimCardService();
            SimCards = new ObservableCollection<SimCard>();
            SearchCommand = new Command(async () => await FetchSimCards());
            EditCommand = new Command<SimCard>(async (simCard) => await EditSimCard(simCard));

        }

        public async Task FetchSimCards()
        {
            if (string.IsNullOrEmpty(SearchId))
                return;

            var simCards = await _simCardService.GetSimCardsAsync(SearchId);
            SimCards.Clear();

            foreach (var simCard in simCards)
            {
                SimCards.Add(simCard);
            }
        }


        private async Task EditSimCard(SimCard simCard)
        {
            if (simCard != null)
            {
                // Navegar a la página de edición y esperar el resultado
                await Application.Current.MainPage.Navigation.PushAsync(new EditSimCardPage(simCard));

                // Volver a buscar los datos o actualizar la lista manualmente
                await FetchSimCards();
            }
        }

    }

}

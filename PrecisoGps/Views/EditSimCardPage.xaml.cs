using PrecisoGps.Models;
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
    public partial class EditSimCardPage : ContentPage
    {
        public EditSimCardPage(SimCard simCard)
        {
            InitializeComponent();
            // Asigna el SimCard al BindingContext del ViewModel
            BindingContext = new EditSimCardViewModel(simCard);
        }
    }
}
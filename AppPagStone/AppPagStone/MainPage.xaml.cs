using AppPagStone.ViewModel;
using Xamarin.Forms;

namespace AppPagStone
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CartaoCreditoViewModel();
        }
    }
}

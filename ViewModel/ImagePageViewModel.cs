using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Konyvtar.ViewModel
{
    class ImagePageViewModel : ViewModelBase
    {
        private string imageurl;
        public string ImageUrl
        {
            get { return imageurl; }
            set
            {
                Set(ref imageurl, value);
            }
        }

        //A parameterben kapott imageUrlt beallitja a valtozoba
        //ami ossze van bindolva a xmalben így majd megjelenik az oldalon
        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {        
                ImageUrl = (string)parameter;
                return base.OnNavigatedToAsync(parameter, mode, state);
       
        }
    }
}

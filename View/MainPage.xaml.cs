using Konyvtar.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Konyvtar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       

        public MainPage()
        {
            this.InitializeComponent();
        }

        //Annak fuggvenyeben hogy melyik radioButton van kivalasztva
        //beallitja a searchtypot es meghivja az adott tipusu keresest elvegzo ViewModel fuggvenyt
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            if (radio1.IsChecked == true)
            {
                ViewModel.searchtype = "title";
                ViewModel.searchBy();
            }
            else
            {
                if (radio2.IsChecked == true)
                {
                    ViewModel.searchtype = "author";
                    ViewModel.searchBy();
                }
                else
                {
                    if (radio3.IsChecked == true)
                    {
                        ViewModel.searchBySubject();
                    }
                }
            }
            
        }

        //Meghivja a ViewModel book reszletezeseert felelos fuggvenyet
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectBook((Book)e.ClickedItem);
        }

        //Meghivja a ViewModel Boritokepre navigalo fuggvenyet
        //ami egy uj lapot nyit a boritokeppel ha van a konyvnek
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.NavigateToImage();
        }
    }
}

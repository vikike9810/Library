using Konyvtar.Model;
using Konyvtar.Services;
using Konyvtar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Konyvtar.ViewModel
{
   public class MainPageViewModel : ViewModelBase
    {

        public static ObservableCollection<Book> books { get; set; } = new ObservableCollection<Book>();
        private string searchparam;
        public string SearchParam {
            get { return searchparam; }
            set { Set(ref searchparam, value); }
        }

        public Book selectedBook;
        private static DeatailedBook privdetailedBook;
        public  DeatailedBook detailedBook{
            get { return privdetailedBook; }
            set { Set(ref privdetailedBook, value);
            }
        }
        public string searchtype;

        private string error;
        public string Error
        {
            get { return error; }
            set { Set(ref error, value); }
        }


        public override  Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        { 
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        //A listabol kivalasztott konyv reszleteit keri le,
        //majd azokat a detailedBookba rakja ami ossze van bindolva a felulettel.
        //Ha nincs talalat a konyvre reszletire akkor egy ures DetailedBook lesz a detauledBokkban
        public async void SelectBook(Book bk)
        {
            selectedBook = bk;
            var service = new BookService();
            try
            {
                var dBook = await service.GetBookDetailsAsync(selectedBook.key);
                if (dBook != null)
                {
                    if (selectedBook.coverUrl != null)
                        dBook.coverUrl = selectedBook.coverUrl;
                    dBook.author = selectedBook.author;
                    if (dBook.authors != null)
                    {
                        if (dBook.authors[0].author != null)
                            dBook.authorLink = new Uri("http://www.openlibrary.org" + dBook.authors[0].author.key);
                    }
                    detailedBook = dBook;
                }
            }
            catch(Exception e)
            {
                detailedBook = new DeatailedBook();
                var m = e.Message;
            }
        }

        //Ez a fuggveny intezi a szerzo es a cim alapjan keresest
        //a mar beallitott searchtype es a bindolt searchParam stringekkel lekeri a talalatokat
        //majd a books listaba rakja oket, ha nincs talalat akkor Error uzenet erteket beallitja
        public async void searchBy()
        {
            detailedBook = null;
            books.Clear();
            Error = null;
            if (SearchParam != null)
            {
                var service = new BookService();
                var list = await service.GetBookAsync(searchtype + "=" + SearchParam);
                foreach (var item in list.docs)
                {
                    if (item.cover_i != 0)
                    {
                        item.coverUrl = $"http://covers.openlibrary.org/b/id/{item.cover_i.ToString()}-L.jpg";
                    }
                    if (item.author_name != null)
                    {
                        item.author = item.author_name[0];
                    }
                    books.Add(item);

                }
            }
            if(books.Count == 0)
            {
                Error = "Nincs találat";
            }

        }

        //Ez a fuggveny intezi a tema szerinti keresest
        //a bindolt searchParam stringgel lekeri a talalatokat
        //majd a books listaba rakja oket, ha nincs talalat akkor Error uzenet erteket beallitja
        public async void searchBySubject()
        {
            detailedBook = null;
            books.Clear();
            Error = null;
            if (SearchParam != null)
            {
                var service = new BookService();
                var list = await service.GetBookBySubjectAsync(SearchParam);
                foreach (var item in list.works)
                {
                    var book = createBook(item);
                    books.Add(book);
                }
            }
            if (books.Count == 0)
            {
                Error = "Nincs találat";
            }

        }

        //A tema szerinti keresesnel visszakapott BookBySubject-et alakitja at egy uj Booka
        //Mivel a felulet a books listaval van osszebindolva, ami bookokat tartalmaz
        public Book createBook(BookBySubject book)
        {
            var newbook = new Book();
            if (book.authors!=null && book.authors.Count!=0)
                newbook.author = book.authors[0].name;
            if (book.cover_id != 0)
                newbook.coverUrl = $"http://covers.openlibrary.org/b/id/{book.cover_id.ToString()}-L.jpg";
            newbook.title = book.title;
            if(book.first_publish_year!="null"&& book.first_publish_year != null)
            newbook.first_publish_year = book.first_publish_year;
            newbook.key = book.key;
            return newbook;
        }

        //Atnavigal a kepoldalra ahol az adott konyv boritoja van  nagyobb meretben,
        //amennyiben van a konyvnek boritokepe
        public void NavigateToImage()
        {
            if (detailedBook.coverUrl != null)
            {
                NavigationService.Navigate(typeof(ImagePage), detailedBook.coverUrl);
            }
        }
    }

}

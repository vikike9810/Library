using Konyvtar.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Konyvtar.Services
{
    class BookService
    {
        private String url = "http://openlibrary.org/search.json?";

        //A szerzo es a cim szerinti keresesnel hasznalt urival ter vissza
        // az url es a kapott string osszefuzesevel
        public Uri createUrl(string s)
        {
            return new Uri(url + s);
        }

        // a tema szerinti keresesnel hasznalt urival ter vissza
        // az url es a kapott string osszefuzesevel
        public Uri SubjectUrl(string s)
        {
            return new Uri("http://openlibrary.org/subjects/" + s +".json?");
        }

        //A http kerest indit az adott urival
        //majd az eredmenyt atkonvertalja az adott tipusba
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        //A szerzo es a cim szerinti keresesnel ezzel a fuggvennyel keri le a taltokat
        public async Task<ListofBooks> GetBookAsync(string s)
        {
            return await GetAsync<ListofBooks>(createUrl(s));
        }

        //A tema szerinti keresesnel ezzel a fuggvennyel keri le a talatokat
        public async Task<ListBySubject> GetBookBySubjectAsync(string s)
        {
            return await GetAsync<ListBySubject>(SubjectUrl(s));
        }

        //Az adott konyv reszletes adatait lekero fuggveny hivas
        //parameterkent kapja az adott konyv json kulcsat
        public async Task<DeatailedBook> GetBookDetailsAsync(string s)
        {
            return await GetAsync<DeatailedBook>(new Uri("http://openlibrary.org"+s+".json"));
        }






    }
}

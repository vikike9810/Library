using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konyvtar.Model
{
    public class Book
    {
        public string coverUrl { get; set; }
        public string title { get; set; }
        public string first_publish_year { get; set; }
        public List<string> author_name { get; set; }
        public int cover_i { get; set; }
        public string author { get; set; }
        public string key { get; set; }
    
    }

    public class ListofBooks
    {
        public List<Book> docs { get; set; }

    }

    public class ListBySubject
    {
        public List<BookBySubject> works { get; set; }
    }

    public class BookBySubject
    {
        public int cover_id { get; set; }
        public string coverUrl { get; set; }
        public List<Author> authors { get; set; }
        public string title { get; set; }
        public string first_publish_year { get; set; }
        public string key { get; set; }

    }
    public class Author
    {
        public string name { get; set; }
    }

    public class DeatailedBook
    {
        public string description { get; set; }
        public string title { get; set; }
        public string coverUrl { get; set; }
        public string author { get; set; }
        public string first_publish_date { get; set; }
        public List<string> subjects { get; set; }
        public List<AuthorLink> authors { get; set; }
        public Uri authorLink { get; set; }
    }

    public class AuthorLink
    {
        public AuthorKey author { get; set; }
    }

    public class AuthorKey
    {
        public string key { get; set; }
    }
}

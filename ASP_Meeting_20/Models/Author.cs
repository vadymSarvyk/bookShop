using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Meeting_20.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }


        [NotMapped]
        public string FullName => $"{Firstname} {Surname}";

        public Author()
        {
            Books = new List<Book>();
        }
    }
}

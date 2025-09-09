using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateOnly Year { get; set; }
        public bool Available { get; set; } = true;

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Author: {Author}, Year: {Year}, Available: {Available}";
        }
    }
}

using LibraryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Interfaces
{
    public interface IBookService
    {
        void Add(string title, string author, DateOnly year);
        void Delete(Guid id);
        void ChangeStatus(Guid id);

        IEnumerable<Book> SearchByTitle(string title);
        IEnumerable<Book> SearchByAuthor(string author);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetAllAvaliable();
    }
}

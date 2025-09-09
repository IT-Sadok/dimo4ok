using LibraryManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Interfaces
{
    public interface ILabrary
    {
        void Add(Book book);
        void Delete(Guid id);
        void ChangeStatus(Guid id);

        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetAllAvaliable();
        IEnumerable<Book> SearchByTitle(string title);
        IEnumerable<Book> SearchByAuthor(string author);
    }
}
